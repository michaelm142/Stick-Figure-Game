using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float Life = 1.0f;

    // Update is called once per frame
    void Update()
    {
        Life -= Time.deltaTime;
        if (Life <= 0.0f)
            Destroy(gameObject);
    }
}
