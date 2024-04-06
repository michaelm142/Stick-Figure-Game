using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDecisionMaker : MonoBehaviour, IDecisionMaker
{
    public GameObject target
    {
        get 
        {
            if (opponentParty.members.Count == 0)
                return null;

            return opponentParty.members[targetIndex]; 
        }
    }


    private Party opponentParty;

    private int targetIndex;

    public IEnumerable<string> MakeDecision()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            yield return "attack";
        if (Input.GetKeyDown(KeyCode.DownArrow))
            yield return "defend";

        yield return string.Empty;
    }

    // Start is called before the first frame update
    void Start()
    {
        opponentParty = FindObjectsOfType<Party>().ToList().Find(p => !p.isPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            targetIndex--;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            targetIndex++;

        if (targetIndex < 0)
            targetIndex = opponentParty.members.Count - 1;
        if (targetIndex >= opponentParty.members.Count)
            targetIndex = 0;
    }
}
