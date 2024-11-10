using UnityEngine;
using System;

public class infection_factors : MonoBehaviour
{
        // Method to normalize population density
    public double NormalizePopulationDensity(double population, double citySize)
    {
        return Mathf.Clamp((float)((population/citySize)/300000), 0f, 1f);//300000 largest recorded pop density
    }

    // Method to normalize distance to neighboring populations
    public double NormalizeDistance(double distance, double minDistance, double maxDistance)
    {
        return Mathf.Clamp((float)(1 - ((distance - minDistance) / (maxDistance - minDistance))), 0f, 1f);
    }

    public double NormalizeCityConnections(double value)
    {
        return Mathf.Clamp((float)(value/10), 0f, 1f);//10 baseline
    }

    
    public double NormalizeSanitationLevel(double sanitationLevel)
    {
        sanitationLevel = sanitationLevel/5;//5 baseline
        return Mathf.Clamp((float)sanitationLevel, 0f, 1f);
    }

    public double NormalizeMedicalKnowledge(double medicalLevel)
    {
        medicalLevel = medicalLevel/5;//5 baseline
        return Mathf.Clamp((float)medicalLevel, 0f, 1f);
    }

    public double NormalizeEconomicStability(double economicLevel)
    {
        economicLevel = economicLevel/5;//5 baseline
        return Mathf.Clamp((float)economicLevel, 0f, 1f);
    }

    public double NormalizeClimateSuitability(double climateSuitability)
    {
        climateSuitability = climateSuitability/10;
        return Mathf.Clamp((float)climateSuitability, 0f, 1f);
    }
}
