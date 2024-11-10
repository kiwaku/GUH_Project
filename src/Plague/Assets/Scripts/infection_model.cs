using UnityEngine;
using System.Collections.Generic;
using System;



public class infection_model : MonoBehaviour
{

    [SerializeField] infection_factors factors;
    [SerializeField] city_data_loader cityData;

    public double N = 1000;           // Total population
    public double infection_rate = 0.001;         // Infection rate
    public double incubation_rate = 1.0 / 5.2;  // Incubation rate (1/incubation period in days)
    public double recovery_rate = 1/2;   // Recovery rate (1/infectious period in days)

    public double mortality_rate = 0; //death rate of infected pop
    
    private double S, E, I, R, D;        // Compartments (Susceptible, Exposed, Infected, Recovered, Deceased)

    private double timeStep = 1;    // Step size 
    //private double maxTime = 365;     // Total simulation time in days
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initial conditions
        S = N - 1;  // Initial susceptible
        E = 0;      // Initial exposed
        I = 1;      // Initial infected
        R = 0;      // Initial recovered
        infection_rate = AdjustInfectionRate(infection_rate, cityData.population, cityData.area, cityData.connectionLevel, cityData.sanitationLevel, cityData.medicalLevel, cityData.economicStabilityLevel);
        recovery_rate = AdjustRecoveryRate(recovery_rate, cityData.medicalLevel, cityData.sanitationLevel);
        SimulateSEIR();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Derivative function for SEIR model
    private (double dSdt, double dEdt, double dIdt, double dRdt, double dDdt) Derivatives(double S, double E, double I, double R, double D)
    {
        double dSdt = (-infection_rate * S * I) / N;
        double dEdt = (infection_rate * S * I / N) - (incubation_rate * E);
        double dIdt = (incubation_rate * E) - ((recovery_rate + mortality_rate) * I);
        double dRdt = recovery_rate * I;
        double dDdt = mortality_rate * I;

        return (dSdt, dEdt, dIdt, dRdt, dDdt);
    }

    // Runge-Kutta 4th Order Method for solving ODEs
    private void RK4Step(ref double S, ref double E, ref double I, ref double R, ref double D, double dt)
    {
        var (dS1, dE1, dI1, dR1, dD1) = Derivatives(S, E, I, R, D);
        var (dS2, dE2, dI2, dR2, dD2) = Derivatives(S + dS1 * dt / 2, E + dE1 * dt / 2, I + dI1 * dt / 2, R + dR1 * dt / 2, D + dD1 * dt / 2);
        var (dS3, dE3, dI3, dR3, dD3) = Derivatives(S + dS2 * dt / 2, E + dE2 * dt / 2, I + dI2 * dt / 2, R + dR2 * dt / 2, D + dD2 * dt / 2);
        var (dS4, dE4, dI4, dR4, dD4) = Derivatives(S + dS3 * dt, E + dE3 * dt, I + dI3 * dt, R + dR3 * dt, D + dD3 * dt);

        // Update each variable using the RK4 formula
        S += dt * (dS1 + 2 * dS2 + 2 * dS3 + dS4) / 6;
        E += dt * (dE1 + 2 * dE2 + 2 * dE3 + dE4) / 6;
        I += dt * (dI1 + 2 * dI2 + 2 * dI3 + dI4) / 6;
        R += dt * (dR1 + 2 * dR2 + 2 * dR3 + dR4) / 6;
        D += dt * (dD1 + 2 * dD2 + 2 * dD3 + dD4) / 6; // Update the deceased population
    }

    // Run the simulation for maxTime with time steps
    private void SimulateSEIR()
    {
        List<double> SList = new List<double>();
        List<double> EList = new List<double>();
        List<double> IList = new List<double>();
        List<double> RList = new List<double>();
        List<double> DList = new List<double>();

        double time = 0;
        while (I/N > 0.0001)
        {
            // Store the current values
            SList.Add(S / N);
            EList.Add(E / N);
            IList.Add(I / N);
            RList.Add(R / N);
            DList.Add(D / N);

            // Step forward in time
            RK4Step(ref S, ref E, ref I, ref R, ref D, timeStep);

            // Increment time
            time += timeStep;
        }

        // Output results (for example, printing to the console)
        for (int i = 0; i < SList.Count; i++)
        {
            Debug.Log($"Day {i * timeStep:0.0} - S: {SList[i]:0.000}, E: {EList[i]:0.000}, I: {IList[i]:0.000}, R: {RList[i]:0.000}, D: {DList[i]:0.000}");
        }
    }

    public double AdjustInfectionRate(double baseInfectionRate, double population, double citySize, double cityConnections, double sanitationLevel, double medicalLevel, double economicLevel)
    {

        double densityFactor = factors.NormalizePopulationDensity(population, citySize);
        double connectionFactor = factors.NormalizeCityConnections(cityConnections);
        double sanitationFactor = factors.NormalizeSanitationLevel(sanitationLevel);
        double medicalFactor = factors.NormalizeMedicalKnowledge(medicalLevel);
        double economicFactor = factors.NormalizeEconomicStability(economicLevel);

        // Combine these factors with respective weights
        double weightedFactor = (densityFactor * 0.2) + (connectionFactor * 0.15) -
                                (medicalFactor * 0.1) - (sanitationFactor * 0.1) + (economicFactor * 0.1);

        return baseInfectionRate * (1 + weightedFactor);
    }

    public double AdjustRecoveryRate(double baseRecoveryRate, double medicalLevel, double sanitationLevel){
        double medicalFactor = factors.NormalizeMedicalKnowledge(medicalLevel);
        double sanitationFactor = factors.NormalizeSanitationLevel(sanitationLevel);

        double weightedFactor = (medicalFactor * 0.2) + (sanitationFactor * 0.1);

        return baseRecoveryRate * (1 + weightedFactor);

    }
    
    
}
