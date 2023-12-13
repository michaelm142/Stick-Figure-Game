using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FlavorTextBox))]
public class ActiveInteraction : MonoBehaviour
{
    public UnityEvent OnInteract;

    private bool active;

    private float interactPrev;

    // Start is called before the first frame update
    void Start()
    {
        interactPrev = Input.GetAxis("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        if (active && Input.GetAxis("Interact") == 0.0f && interactPrev > 0.0f)
        {
            BroadcastMessage("ActiveInteract", SendMessageOptions.DontRequireReceiver);
            if (OnInteract != null)
                OnInteract.Invoke();
        }

        interactPrev = Input.GetAxis("Interact");
    }

    void BeginInteract()
    {
        active = true;
    }

    void EndInteract()
    {
        active = false;
    }
}
