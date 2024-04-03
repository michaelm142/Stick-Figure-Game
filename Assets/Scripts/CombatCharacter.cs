using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFG;

public class CombatCharacter : MonoBehaviour
{
    public Character character { get; private set; }

    public float Health
    {
        get { return character.Health; }

        set { character.Health = value; }
    }

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
