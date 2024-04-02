using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFG;

public class CombatCharacter : MonoBehaviour
{
    public Character character { get; private set; }

    public float Health;

    public CombatCharacter()
    {
        character = new Character("Obi-Wan Kenobi");
        Health = character.Health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
