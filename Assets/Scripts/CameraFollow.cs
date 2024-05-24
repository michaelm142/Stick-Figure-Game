using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float ZDistance = 10.0f;
    public float CameraRotationSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Reposition();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 L = ((target.position - target.forward * ZDistance) - transform.position).normalized;
        //float w = 1.0f - Mathf.Abs(Vector3.Dot(L, transform.forward));
        transform.position += L * Time.deltaTime;// * w;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, Time.deltaTime * CameraRotationSpeed);
    }

    public void Reposition()
    {
        transform.position = target.position - target.forward * ZDistance;
        transform.rotation = target.rotation;
    }
}
