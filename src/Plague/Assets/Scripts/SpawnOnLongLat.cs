using UnityEngine;

// To spawn a game object at a specific latitude and longitude, call SpawnMarker(latitute, longitude)

public class MarkPoint : MonoBehaviour
{
    public GameObject marker; // Assign the red marker prefab in the inspector
    public float radius = 51f; // Radius of the sphere

    // Start is called before the first frame update
    void Start()
    {
        float latitude = 40.6670f; // Example latitude
        float longitude = 16.6063f; // Example longitude

        SpawnMarker(latitude, longitude);
    }

    public void SpawnMarker(float latitude, float longitude)
    {

        Vector3 markerPosition = CalculateMarkerPosition(latitude, longitude);
        GameObject spawnedMarker = Instantiate(marker, markerPosition, Quaternion.identity);
        // Configure colour and size.
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