using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyHealthUIControl : MonoBehaviour
{
    public GameObject[] healthPlates;

    private Party playerParty;

    public Gradient healthGradient;

    // Start is called before the first frame update
    void Start()
    {
        // find player party
        playerParty = FindObjectsOfType<Party>().ToList().Find(p => p.isPlayer);
        // initialize hud elements
        for (int i = 0; i < healthPlates.Length; i++)
        {
            if (i >= playerParty.members.Count)
            {
                healthPlates[i].SetActive(false);
                continue;
            }

            SFG.Character character = playerParty.members[i].GetComponent<CombatCharacter>().character;

            healthPlates[i].GetComponentInChildren<TextMeshProUGUI>().text = character.Name;
            healthPlates[i].GetComponentInChildren<Slider>().value = character.Health;
            healthPlates[i].GetComponentInChildren<Slider>().maxValue = character.statTable.MaxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // update health plates
        for (int i = 0; i < playerParty.members.Count; i++)
        {
            float health = playerParty.members[i].GetComponent<CombatCharacter>().Health;
            float maxHealth = playerParty.members[i].GetComponent<CombatCharacter>().character.statTable.MaxHealth;
            float x = health / maxHealth;
            Color c = healthGradient.Evaluate(x);
            healthPlates[i].GetComponentInChildren<Slider>().transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = c;
            healthPlates[i].GetComponentInChildren<Slider>().value = health;
        }
    }
}
