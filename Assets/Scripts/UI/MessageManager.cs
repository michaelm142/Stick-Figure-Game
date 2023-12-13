using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    private static MessageManager Instance;

    public GameObject messagePrefab;

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ShowMessage(string message, float lifetime = 3.0f)
    {
        GameObject m = Instantiate(Instance.messagePrefab, Instance.transform);
        m.GetComponentInChildren<TextMeshProUGUI>().text = message;
        m.GetComponent<Lifetime>().Life = lifetime;
    }
}
