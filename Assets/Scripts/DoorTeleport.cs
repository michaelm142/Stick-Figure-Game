using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class DoorTeleport : MonoBehaviour
{
    public float length = 1.0f;

    public Transform location;

    private void OnDrawGizmos()
    {
        var box = GetComponent<BoxCollider>();
        Color c = Color.white;
        if (Selection.objects.Contains(gameObject))
            c = Color.green;
        Vector3 location = transform.localToWorldMatrix.MultiplyPoint(box.center);
        Vector3 size = transform.localToWorldMatrix.MultiplyVector(box.size);
        Gizmos.color = c;
        Gizmos.DrawWireCube(location, size);

        if (this.location != null)
        {
            Gizmos.DrawSphere(this.location.position, 0.1f);
            Gizmos.DrawLine(location, this.location.position);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.location.position, this.location.position + this.location.right * 0.2f);
        }
        Gizmos.color = Color.white;
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
