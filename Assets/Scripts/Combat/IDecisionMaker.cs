using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDecisionMaker
{
    IEnumerable<string> MakeDecision();

    GameObject target { get; }
}
