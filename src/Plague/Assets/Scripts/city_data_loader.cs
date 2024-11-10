using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



public class city_data_loader : MonoBehaviour
{
    public string filePath = "Assets/CityData.csv"; // Specify the path to your CSV file
    public string cityName;
    public string year;
    public int population;
    public int area;
    public Vector2 coordinates; // (Longitude, Latitude)
    public float connectionLevel;
    public int sanitationLevel;
    public int medicalLevel;
    public int economicStabilityLevel;

    void Start()
    {
        ReadCSV(filePath);
        // After reading, you can access the cityDataList or use it for your logic
    }

    void ReadCSV(string path)
    {
        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                bool firstLine = true; // Skip header
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (firstLine)
                    {
                        firstLine = false; // Skip header
                        continue;
                    }

                    string[] values = line.Split(',');

                    cityName = values[0].Trim();
                    year = values[1].Trim();
                    population = int.Parse(values[2].Trim());
                    area = int.Parse(values[3].Trim());
                    float longitude = float.Parse(values[4].Trim());
                    float latitude = float.Parse(values[5].Trim());
                    Vector2 coordinates = new Vector2(longitude, latitude);
                    connectionLevel = float.Parse(values[6].Trim());
                    sanitationLevel = int.Parse(values[7].Trim());
                    medicalLevel = int.Parse(values[8].Trim());
                    economicStabilityLevel = int.Parse(values[9].Trim());


                }
            }
            Debug.Log("CSV Read Successfully");
        }
        catch (Exception e)
        {
            Debug.LogError("Error reading CSV file: " + e.Message);
        }
    }
}

