using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatDirector : MonoBehaviour
{
    static string layoutXml;

    IDecisionMaker playerDescisionMaker;
    IDecisionMaker aiDecisionMaker;

    public bool isPlayerTurn;

    private int characterIndex;

    private Party playerParty;
    private Party opponentParty;

    public float CameraMoveSpeed = 1.0f;
    public float CameraRotatSpeed = 5.0f;

    public Transform victoryScreen;

    public GameObject targetHealthComponent;
    public float targetHealthVerticalOffset = 200.0f;

    #region Initialize

    // Start is called before the first frame update
    void Start()
    {
        playerDescisionMaker = FindObjectOfType<PlayerDecisionMaker>();
        aiDecisionMaker = FindObjectOfType<AiDecisionMaker>();

        targetHealthComponent = Instantiate(targetHealthComponent, transform);

        BeginCombat();
    }

    void BeginCombat()
    {
        List<Party> parties = FindObjectsOfType<Party>().ToList();

        playerParty = parties.Find(p => p.isPlayer);
        opponentParty = parties.Find(p => p != playerParty);

        List<GameObject> combatSquares = GameObject.FindGameObjectsWithTag("CombatSquare").ToList();

        Transform playerCombatSquare = combatSquares.Find(s => s.name == "Player").transform;
        Transform opponentCombatSquare = combatSquares.Find(s => s.name == "Opponent").transform;

        SpawnCharacters(playerParty, playerCombatSquare);
        SpawnCharacters(opponentParty, opponentCombatSquare);
    }

    void SpawnCharacters(Party party, Transform combatSquare)
    {
        List<GameObject> newMembers = new List<GameObject>();
        string[] layout = LoadLayout(party.members.Count);
        for (int i = 0; i < party.members.Count; i++)
        {
            Transform space = combatSquare.transform.Find(layout[i]);
            GameObject character = Instantiate(party.members[i]);
            character.transform.position = space.position;
            newMembers.Add(character);
        }
        party.members = newMembers;
    }

    private string[] LoadLayout(int n)
    {
        if (string.IsNullOrEmpty(layoutXml))
        {
            TextAsset xml = Resources.Load<TextAsset>("Xml\\GridLayouts");
            layoutXml = xml.text;
        }
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(layoutXml);

        XmlElement root = doc.FirstChild as XmlElement;
        List<XmlNode> layouts = root.SelectNodes("Layout").ConvertTo<List<XmlNode>>();
        XmlNode layoutNode = layouts.Find(l => l.Attributes["n"].Value == n.ToString());
        string[] outval = new string[layoutNode.ChildNodes.Count];

        for (int i = 0; i < outval.Length; i++)
        {
            XmlNode actor = layoutNode.ChildNodes[i];
            string x = actor.Attributes["x"].Value;
            string y = actor.Attributes["y"].Value;

            outval[i] = y + "x" + x;
        }

        return outval;
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        ShowTargetHealth();
        if (isPlayerTurn)
        {
            foreach (string decision in playerDescisionMaker.MakeDecision())
            {
                if (string.IsNullOrEmpty(decision))
                    break;

                ProcessPlayerDecision(decision);
            }
        }
        else
        {
            foreach (string decision in aiDecisionMaker.MakeDecision())
            {
                if (string.IsNullOrEmpty(decision))
                    break;

                ProcessAiDecision(decision);
            }
        }

        if (isPlayerTurn && characterIndex >= playerParty.members.Count)
        {
            characterIndex = 0;
            isPlayerTurn = false;
        }

        if (!isPlayerTurn && characterIndex >= opponentParty.members.Count)
        {
            characterIndex = 0;
            isPlayerTurn = true;
        }

        DetectEndCondition();
    }

    private void DetectEndCondition()
    {
        // remove dead members from each party
        for (int i = 0; i < opponentParty.members.Count; i++)
        {
            if (opponentParty.members[i].GetComponent<CombatCharacter>().Health < 0.0f)
            {
                opponentParty.members.RemoveAt(i);
                i--;
            }
        }
        for (int i = 0; i < playerParty.members.Count; i++)
        {
            if (playerParty.members[i].GetComponent<CombatCharacter>().Health < 0.0f)
            {
                playerParty.members.RemoveAt(i);
                i--;
            }
        }

        if (opponentParty.members.Count == 0)
            victoryScreen.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (isPlayerTurn)
            ControlCamera();

    }

    /// <summary>
    /// Displays the health of the current target
    /// </summary>
    void ShowTargetHealth()
    {
        if (playerDescisionMaker.target == null)
        {
            targetHealthComponent.SetActive(false);
            return;
        }

        Vector3 targetPosition = Camera.main.WorldToScreenPoint(playerDescisionMaker.target.transform.position);
        targetHealthComponent.transform.position = targetPosition + Vector3.up * targetHealthVerticalOffset;
        targetHealthComponent.transform.Find("health").GetComponent<Image>().fillAmount = playerDescisionMaker.target.GetComponent<CombatCharacter>().Health /
            playerDescisionMaker.target.GetComponent<CombatCharacter>().character.statTable.MaxHealth;
    }


    void ControlCamera()
    {

        GameObject target = playerDescisionMaker.target;
        if (target != null)
        {
            //Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, target.transform.position + Vector3.up * 2.0f + Vector3.right * 2.0f + Vector3.back * 2.0f, CameraMoveSpeed);
            Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, Quaternion.LookRotation(playerDescisionMaker.target.transform.position - Camera.main.transform.position), CameraRotatSpeed);
        }
    }


    void ProcessAiDecision(string decision)
    {
        if (!string.IsNullOrEmpty(decision))
        {
            switch (decision)
            {
                case "attack":
                    ProcessAiAttackDecision(aiDecisionMaker.target.GetComponent<CombatCharacter>().character);
                    break;
                case "defend":
                    Debug.Log("Ai defended");
                    characterIndex++;
                    break;
            }
        }
    }

    void ProcessAiAttackDecision(SFG.Character defender)
    {
        opponentParty.members[characterIndex].GetComponent<Animator>().SetTrigger("Attack_Melee_Slice");
        Debug.Log("Ai attacked");
        opponentParty.members[characterIndex].GetComponent<CombatCharacter>().character.Attack(defender);
        characterIndex++;
    }

    void ProcessPlayerDecision(string decision)
    {
        if (!string.IsNullOrEmpty(decision))
        {
            switch (decision)
            {
                case "attack":
                    ProcessPlayerAttackDecision(playerDescisionMaker.target.GetComponent<CombatCharacter>().character);
                    break;
                case "defend":
                    Debug.Log("Player defended");
                    characterIndex++;
                    break;
            }
        }
    }

    void ProcessPlayerAttackDecision(SFG.Character defender)
    {
        playerParty.members[characterIndex].GetComponent<Animator>().SetTrigger("Attack_Melee_Slice");
        Debug.Log("Player attacked");

        playerParty.members[characterIndex].GetComponent<CombatCharacter>().character.Attack(defender);
    }

    /// <summary>
    /// Advances combat to the next character in the sequence | called from animation
    /// </summary>
    public void EndTurn()
    {
        characterIndex++;
    }

    /// <summary>
    /// Returns the player to gameplay | Called from UI
    /// </summary>
    public void ReturnToGamePlay()
    {
        SceneManager.LoadScene("TestExteriorEnvironment");
    }
}
