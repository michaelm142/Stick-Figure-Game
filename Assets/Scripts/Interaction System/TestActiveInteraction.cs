using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActiveInteraction))]
public class TestActiveInteraction : MonoBehaviour
{
    void ActiveInteract()
    {
        Debug.Log("Active interaction in game object " + gameObject.name);
    }
}
