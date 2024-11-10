using UnityEngine;
using System.Collections.Generic; // Add this line

// To spawn a game object at a specific latitude and longitude, call SpawnMarker(latitute, longitude)

public class SpawnOnLongLat : MonoBehaviour
{
    public GameObject marker; // Assign the red marker prefab in the inspector
    public float radius = 51f; // Radius of the sphere
    public float passedIncRate;
    public float passedRecRate;
    public float passedInfRate;
    public float passedMortRate;
    [SerializeField] GameObject holder;
    HashSet<Vector3> markerPositions = new HashSet<Vector3>();

    public void SpawnMarker(float latitude, float longitude, string City, string Year, double Population, double Area, double ConnectionProbability, int SanitationLevel, int PublicHealthLevel, int EconomicStability)
    {
        Vector3 markerPosition = CalculateMarkerPosition(latitude, longitude);

        if (!markerPositions.Contains(markerPosition))
        {
            markerPositions.Add(markerPosition);
        }
        else
        {
            return;
        }
        //Vector3 markerPosition = CalculateMarkerPosition(latitude, longitude);

        GameObject spawnedMarker = Instantiate(marker, markerPosition, Quaternion.identity);
        spawnedMarker.transform.SetParent(holder.transform);
        infection_model model = spawnedMarker.GetComponent<infection_model>();
        model.N = Population;
        model.incubation_rate = passedIncRate;
        model.infection_rate = passedInfRate;
        model.recovery_rate = passedRecRate;
        model.mortality_rate = passedMortRate;
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
    void OnDisable()
    {
        Destroy(gameObject);
    }
}