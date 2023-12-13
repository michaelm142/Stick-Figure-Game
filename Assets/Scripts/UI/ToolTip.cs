using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SFG;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject toolTipContainer;

    private GameObject container;


    public void OnPointerEnter(PointerEventData eventData)
    {
        container.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        container.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        container = Instantiate(toolTipContainer, transform.parent);
        container.transform.position = transform.position;
        container.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
