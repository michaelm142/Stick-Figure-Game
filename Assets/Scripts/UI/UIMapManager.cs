using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapManager : MonoBehaviour
{
    public MapIconAnimationControl currentLocation;

    public Sprite Marker;

    private GameObject locationMarker;

    public Transform footstepMarker;

    private const float markerMoveSpeed = .1f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject g = new GameObject("Location Marker");
        locationMarker = Instantiate(g, transform);
        Image image = locationMarker.gameObject.AddComponent<Image>();
        image.sprite = Marker;

        locationMarker.transform.position = currentLocation.transform.position;
        footstepMarker.transform.position = locationMarker.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        locationMarker.transform.position = currentLocation.transform.position;
        if (Vector3.Distance(locationMarker.transform.position, footstepMarker.position) > 0.01f)
        {
            footstepMarker.gameObject.SetActive(true);
            Vector3 L = (locationMarker.transform.position - footstepMarker.position).normalized;
            footstepMarker.position += L * markerMoveSpeed * Time.deltaTime;
        }
        else if (footstepMarker.GetComponent<ParticleSystem>().emission.enabled)
            footstepMarker.gameObject.SetActive(false);
    }
}
