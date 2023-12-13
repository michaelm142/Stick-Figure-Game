using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MapIconAnimationControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string targetScene;

    private Animator anim;

    public void OnPointerClick(PointerEventData eventData)
    {
        var map = FindObjectOfType<UIMapManager>();

        if (map.currentLocation != this)
        {
            map.currentLocation = this;
            FindObjectOfType<SceneDirector>().LoadScene(targetScene);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("Mouse Over", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("Mouse Over", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
}
