using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class CombatDirector : MonoBehaviour
{
    static string layoutXml;

    // Start is called before the first frame update
    void Start()
    {
        BeginCombat();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void BeginCombat()
    {
        List<Party> parties = FindObjectsOfType<Party>().ToList();

        Party playerParty = parties.Find(p => p.isPlayer);
        Party opponentParty = parties.Find(p => p != playerParty);

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
            TextAsset xml = Resources.Load<TextAsset>("GridLayouts");
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
