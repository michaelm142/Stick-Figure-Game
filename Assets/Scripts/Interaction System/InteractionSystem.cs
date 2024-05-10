using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    private GameObject currentInteraction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SendMessage("BeginInteract", SendMessageOptions.DontRequireReceiver);
        currentInteraction = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.SendMessage("EndInteract", SendMessageOptions.DontRequireReceiver);
        EndInteract();
    }

    public void ForceEndInteraction()
    {
        EndInteract();
    }

    void EndInteract()
    {
        if (currentInteraction == null) return;

        currentInteraction.gameObject.SendMessage("EndInteract", SendMessageOptions.DontRequireReceiver);
        currentInteraction = null;
    }
}
