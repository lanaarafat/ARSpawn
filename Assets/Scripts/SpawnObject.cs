using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class SpawnObject : MonoBehaviour
{
    public GameObject objectPrefab;
    private ARRaycastManager arRaycastManager;
    [HideInInspector]
    public GameObject spawnedObject;

    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (spawnedObject == null) // Ensures only one object is spawned
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (arRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    spawnedObject = Instantiate(objectPrefab, hitPose.position, hitPose.rotation);
                }
            }
        }
    }

    public void ChangeColor(string color)
    {
        if (spawnedObject == null) return;

        var objRenderers = spawnedObject.GetComponentsInChildren<Renderer>();

        Color newColor = color.ToLower() switch
        {
            "red" => Color.red,
            "green" => Color.green,
            "blue" => Color.blue,
            _ => default
        };

        if (newColor != default)
        {
            foreach (var renderer in objRenderers)
            {
                foreach (var material in renderer.materials)
                {
                    material.SetColor("_BaseColor", newColor);
                }
            }
        }
    }
}
