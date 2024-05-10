using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    public float length = 1.0f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + transform.forward * length, Vector3.up * 2.0f + Vector3.forward + Vector3.right);
    }

    public void TeleportPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = transform.position + transform.forward * length;
    }
}
