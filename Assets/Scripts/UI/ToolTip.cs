using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SFG;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected static GameObject container;


    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        container.SetActive(true);
        container.transform.position = Input.mousePosition;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        container.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (container == null)
        {
            GameObject toolTipContainer = Resources.Load<GameObject>("UI/UI_Stat_Container");
            container = Instantiate(toolTipContainer, FindObjectOfType<Canvas>().transform);
            container.transform.position = transform.position;
            container.SetActive(false);
        }
    }
}