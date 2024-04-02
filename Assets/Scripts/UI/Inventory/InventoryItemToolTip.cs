using SFG;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemToolTip : ToolTip
{
    public GameItem item { get; set; }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        string itemText = item.Name;
        if (item is Armor)
            itemText += "\n Defense: " + ((Armor)item).Defense;
        if (item is Weapon)
            itemText += "\n Attack: " + ((Weapon)item).Attack;
        container.GetComponentInChildren<TextMeshProUGUI>().text = itemText;
    }
}
