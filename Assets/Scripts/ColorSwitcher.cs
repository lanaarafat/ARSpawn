using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ColorSwitcher : MonoBehaviour
{
    private GameObject spawnedObject;

    public void ChangeColor(string color)
    {
        if (spawnedObject == null) spawnedObject = FindObjectOfType<SpawnObject>()?.spawnedObject;
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
                    material.color = newColor;
                }
            }
        }
    }
}
