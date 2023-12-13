using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class LineRendererAnimator : MonoBehaviour
{
    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RecursiveSetLinePosition(transform.GetChild(0), 0);
    }

    void RecursiveSetLinePosition(Transform leaf, int index)
    {
        line.SetPosition(index, leaf.position);
        if (leaf.childCount != 0)
            RecursiveSetLinePosition(leaf.GetChild(0), ++index);
    }
}
