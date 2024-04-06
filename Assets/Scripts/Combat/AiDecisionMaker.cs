using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AiDecisionMaker : MonoBehaviour, IDecisionMaker
{
    public GameObject target
    {
        get
        {
            if (playerParty.members.Count == 0)
                return null;

            return playerParty.members[targetIndex];
        }
    }

    private int targetIndex;

    private Party playerParty;

    public IEnumerable<string> MakeDecision()
    {
        yield return "attack";
    }

    // Start is called before the first frame update
    void Start()
    {
        playerParty = FindObjectsOfType<Party>().ToList().Find(p => p.isPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
