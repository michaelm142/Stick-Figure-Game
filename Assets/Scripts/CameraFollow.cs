using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 L = (target.position - transform.position).normalized;
        float w = 1.0f - Mathf.Abs(Vector3.Dot(L, transform.forward));
        transform.position += new Vector3(L.x, L.y, 0.0f) * w;
    }
}
