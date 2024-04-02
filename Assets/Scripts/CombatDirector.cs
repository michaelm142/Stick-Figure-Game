using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CombatDirector : MonoBehaviour
{
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
        for (int y = 1, partyIndex = 0; y < 3 && partyIndex < party.members.Count; y++)
        {
            for (int x = 1; x < 4; x++)
            {
                if (partyIndex >= party.members.Count)
                    break;

                Transform space = combatSquare.transform.Find(y.ToString() + "x" + x.ToString());
                GameObject character = Instantiate(party.members[partyIndex]);
                character.transform.position = space.position;
                newMembers.Add(character);
                partyIndex++;
            }
        }
        party.members = newMembers;
    }


}
