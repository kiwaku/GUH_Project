using UnityEngine;

// To spawn a game object at a specific latitude and longitude, call SpawnMarker(latitute, longitude)

public class MarkPoint : MonoBehaviour
{
    public GameObject marker; // Assign the red marker prefab in the inspector
    public float radius = 50f; // Radius of the sphere

    // Start is called before the first frame update
    void Start()
    {
        // float latitude = 35f; // Example latitude
        // float longitude = 100f; // Example longitude

        // SpawnMarker(latitude, longitude);
    }

    // Update is called once per frame
    void Update()
    {
        // Frame update code here
    }

    public void SpawnMarker(float latitude, float longitude)
    {
        // Example coordinates
        // float latitude = 335.8617f; // Example latitude
        // float longitude = 104.1954f; // Example longitude

        Vector3 markerPosition = CalculateMarkerPosition(latitude, longitude);
        Instantiate(marker, markerPosition, Quaternion.identity);
    }

    Vector3 CalculateMarkerPosition(float lat, float lon)
    {
        // Adjust latitude and longitude to match Unity's coordinate system
        float adjustedLat = 90 - lat;
        float adjustedLon = lon;

        float phi = adjustedLat * Mathf.Deg2Rad;
        float theta = adjustedLon * Mathf.Deg2Rad;

        float x = radius * Mathf.Sin(phi) * Mathf.Cos(theta);
        float z = radius * Mathf.Sin(phi) * Mathf.Sin(theta);
        float y = radius * Mathf.Cos(phi);

        // Initial position before rotation
        Vector3 initialPosition = new Vector3(x, y, z);
        return initialPosition;
    }
}