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
        if (Health < 0.0f)
            gameObject.SetActive(false);
    }

    /// <summary>
    /// Ends the turn for the current character and advances combat to the next
    /// </summary>
    public void EndTurn()
    {
        FindObjectOfType<CombatDirector>().EndTurn();
    }
}
