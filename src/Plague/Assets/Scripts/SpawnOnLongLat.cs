using UnityEngine;

// To spawn a game object at a specific latitude and longitude, call SpawnMarker(latitute, longitude)

public class SpawnOnLongLat : MonoBehaviour
{
    public GameObject marker; // Assign the red marker prefab in the inspector
    public float radius = 51f; // Radius of the sphere
    [SerializeField] GameObject holder;

    public void SpawnMarker(float latitude, float longitude)
    {

        Vector3 markerPosition = CalculateMarkerPosition(latitude, longitude);
        GameObject spawnedMarker = Instantiate(marker, markerPosition, Quaternion.identity);
        spawnedMarker.transform.SetParent(holder.transform); // Assign the holder object in the inspector
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
    void OnDisable()
    {
        Destroy(gameObject);
    }
}