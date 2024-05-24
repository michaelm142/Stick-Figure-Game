using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DoorTeleport : MonoBehaviour
{
    public float length = 1.0f;

    public Transform location;

    private void OnDrawGizmos()
    {
        if (location == null)
            Gizmos.DrawWireCube(transform.position + transform.forward * length, Vector3.up * 2.0f + Vector3.forward + Vector3.right);
    }

    public void TeleportPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterController>().enabled = false;
        if (location == null)
            player.transform.position = transform.position + transform.forward * length;
        else
        {
            player.transform.position = location.position;
            player.transform.rotation = location.rotation;
        }
        Camera.main.GetComponent<CameraFollow>().Reposition();
        player.GetComponent<InteractionSystem>().ForceEndInteraction();
        player.GetComponent<CharacterController>().enabled = true;
        Debug.Log("Door " + name + " teleported player to position " + (transform.position + transform.forward * length).ToString());
    }
}
