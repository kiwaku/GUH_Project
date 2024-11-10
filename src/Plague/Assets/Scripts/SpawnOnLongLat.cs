using UnityEngine;

// To spawn a game object at a specific latitude and longitude, call SpawnMarker(latitute, longitude)

public class SpawnOnLongLat : MonoBehaviour
{
    public GameObject marker; // Assign the red marker prefab in the inspector
    public float radius = 51f; // Radius of the sphere

    public void SpawnMarker(float latitude, float longitude, string City, string Year, int Population, double Area, int ConnectionProbability, int SanitationLevel, int PublicHealthLevel ,int EconomicStability)
    {
        Vector3 markerPosition = CalculateMarkerPosition(latitude, longitude);
        GameObject spawnedMarker = Instantiate(marker, markerPosition, Quaternion.identity);
        infection_model model = spawnedMarker.GetComponent<infection_model>();
        print(model.infection_rate);
        model.infection_rate = model.AdjustInfectionRate(model.infection_rate, Population, Area, ConnectionProbability, SanitationLevel, PublicHealthLevel, EconomicStability);
        model.recovery_rate = model.AdjustRecoveryRate(model.recovery_rate, PublicHealthLevel, SanitationLevel);
        model.SimulateSEIR();
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