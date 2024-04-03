using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class CombatDirector : MonoBehaviour
{
    static string layoutXml;

    IDecisionMaker playerDescisionMaker;
    // IDecisionMaker aiDecisionMaker;

    public bool isPlayerTurn;

    private int characterIndex;

    private Party playerParty;
    private Party opponentParty;

    // Start is called before the first frame update
    void Start()
    {
        playerDescisionMaker = FindObjectOfType<PlayerDecisionMaker>();

        BeginCombat();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTurn)
        {
            foreach (string decision in playerDescisionMaker.MakeDecision())
            {
                if (string.IsNullOrEmpty(decision))
                    break;

                ProcessPlayerDecision(decision);
            }
            
            ControlCamera();
        }

    }

    void ControlCamera()
    {
        if (characterIndex >= playerParty.members.Count)
            characterIndex = 0;

        GameObject target = playerParty.members[characterIndex];
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, target.transform.position + Vector3.up * 2.0f + Vector3.right * 2.0f + Vector3.back * 2.0f, Time.deltaTime);
    }

    void ProcessPlayerDecision(string decision)
    {
        if (!string.IsNullOrEmpty(decision))
        {
            switch (decision)
            {
                case "attack":
                    Debug.Log("Player attacked");
                    characterIndex++;
                    break;
                case "defend":
                    Debug.Log("Player defended");
                    characterIndex++;
                    break;
            }
        }
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

}
