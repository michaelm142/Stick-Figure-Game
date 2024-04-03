using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDecisionMaker : MonoBehaviour, IDecisionMaker
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
