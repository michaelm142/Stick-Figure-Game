using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    public float length = 1.0f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + transform.forward * length, Vector3.up * 2.0f + Vector3.forward + Vector3.right);
    }

    public void TeleportPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = transform.position + transform.forward * length;
        player.GetComponent<InteractionSystem>().ForceEndInteraction();
        player.GetComponent<CharacterController>().enabled = true;
        Debug.Log("Door " + name + " teleported player to position " + (transform.position + transform.forward * length).ToString());
    }
}
