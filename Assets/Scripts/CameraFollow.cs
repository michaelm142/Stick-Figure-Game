using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float ZDistance = 10.0f;
    public float CameraRotationSpeed = 10.0f;
    public float dampening = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        Reposition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = target.position - target.forward * ZDistance;
        Vector3 L = (targetPosition - transform.position);
        float damp = (1.0f * Mathf.Clamp01(dampening));
        L = Vector3.ClampMagnitude(L, Vector3.Distance(target.position, transform.position));
        transform.position += L * damp;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, Time.deltaTime * CameraRotationSpeed);
    }

    public void Reposition()
    {
        transform.position = target.position - target.forward * ZDistance;
        transform.rotation = target.rotation;
    }
}
