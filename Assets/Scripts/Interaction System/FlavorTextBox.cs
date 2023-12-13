using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlavorTextBox : MonoBehaviour
{
    private GameObject textBox;

    public Transform bone;

    public string Title;
    [TextArea]
    public string Text;

    public void Start()
    {
    }

    private void Update()
    {
        if (textBox != null)
        {
            Vector3 scrPos = Vector3.zero;
            if (bone == null)
                scrPos = Camera.main.WorldToScreenPoint(transform.position);
            else
                scrPos = Camera.main.WorldToScreenPoint(bone.position);

            textBox.transform.position = scrPos;
        }
    }

    public void BeginInteract()
    {
        if (textBox != null)
            return;

        textBox = Instantiate(Resources.Load<GameObject>("UI/TextBox"));
        textBox.transform.parent = FindObjectOfType<Canvas>().transform;
        textBox.transform.position = transform.position;
        var body = textBox.transform.Find("Body");
        body.GetComponent<TextMeshProUGUI>().text = Text;
        var title = textBox.transform.Find("Title");
        title.GetComponent<TextMeshProUGUI>().text = Title;
        
    }

    public void EndInteract()
    {
        if (textBox == null)
            return;

        Destroy(textBox);
        textBox = null;
    }
}
