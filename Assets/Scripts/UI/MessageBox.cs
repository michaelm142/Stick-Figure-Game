using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationEnd()
    {
        Destroy(gameObject);
    }

    public static void Show(string message)
    {
        var existing = GameObject.FindObjectOfType<MessageBox>();
        if (existing != null)
            return;

        var messageObject = GameObject.Instantiate(Resources.Load<GameObject>("UI/MessageBox"));
        messageObject.GetComponent<MessageBox>().messageText.text = message;
    }
}
