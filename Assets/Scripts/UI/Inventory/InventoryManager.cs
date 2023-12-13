using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFG;
using Unity.VisualScripting;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    private List<GameItem> items = new List<GameItem>();
    // list of icons for items on the screen
    private List<GameObject> uiItems = new List<GameObject>();

    public RectTransform itemSpace;

    public GameObject UIItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        AddItem(SFG.ArmorFactory.LeatherArmor());
        OnEnableCallback callback = itemSpace.AddComponent<OnEnableCallback>();
        callback.caller = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        if (!itemSpace.gameObject.activeSelf)
            return;

        float width = UIItemPrefab.GetComponent<RectTransform>().rect.width;
        float height = UIItemPrefab.GetComponent<RectTransform>().rect.height;

        float x = width / 2.0f, y = height / 2.0f;
        foreach (GameItem item in items)
        {
            GameObject i = Instantiate(UIItemPrefab, itemSpace);
            i.transform.localPosition = Vector3.right * x - Vector3.up * y;
            i.GetComponentInChildren<TextMeshProUGUI>().text = item.Name;  
            RectTransform rect = i.GetComponent<RectTransform>();
            x += width;
        }
    }

    public void AddItem(GameItem item)
    {
        items.Add(item);
        MessageManager.ShowMessage(string.Format("{0} added", item.Name));
    }
}

internal class OnEnableCallback : MonoBehaviour
{
    public GameObject caller;

    public void OnEnable()
    {
        caller.SendMessage("OnEnable");
    }
}
