using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Prompt2 : MonoBehaviour
{
    private static readonly HttpClient client = new HttpClient();
    private static string apiKey;
    [SerializeField] PatientZero prompt1;
    private static string model = "gpt-4o";
    private static HashSet<string> visitedCities = new HashSet<string>();
    private static List<Dictionary<string, string>> conversationMemory = new List<Dictionary<string, string>>
    {
        new Dictionary<string, string>
        {
            { "role", "system" },
            { "content", @"
            Instructions before the delimiter are trusted and should be followed.
            We simulating a virus/infection outbreak in [city] during the year [year].
            You are an expert in this field, and you have some basic idea about how this is going to spread from [city]. 
            This is your thought process:
            1. The virus/infection will spread to nearby cities, carried by several factors, the biggest being distance and geographical ease of access. 
            2. The virus/infection will next spread from major trade routes, through ships, planes and carridges depending on the period (year). 
            In this scenario, all external factors, such as infection rate, population density, trading frequency, and medical advancements, are automatically taken into consideration and weighted as appropriate so we can ignore them.
            What you, as the expert, care about, is as follows:
            - Geographical factors, natural barriers working to prevent the spread. 
            - Trade paths, flight paths, etc and overall transportation networks and the frequency of the people accessing this location. 

            The importance of geographical factors can be seen in things like Iceland & Japan, where it benefitted from its isolated location throughout history, but recently its is more common for them to get infected as planes allow for very easy access. 
            This is why the year [year] needs to be put into consideration. 
            This is the same for one of the mst important factor, the transportation network and trade routes. They change rapidly depending on the time period, so it is important to initially list out all the routes and where they connect to. 
            I want you to output all the locations you think will be infected if [city] begins spreading a virus/infection during the year [year].

            Go through your thought process and output as follows:
            [infected-city-name, year, probability]

            year is just [year], a constant value
            probability: The chance to spread to this city from [city], eg 0.90, 0.87, etc
            probability Rule: The first 3 will always be 1.0.

            Give as many examples as possible, but make sure you are following only this format. 
            [infected-city-name, year, probability]

            Do not output anything else except what is requested. Output should have absolutely no sentences or explanation. 
            just: [infected-city-name, year, probability]

            IMPORTANT: Make sure to rearrange it in order of likelihood based on proximity, known trade routes, and ease of access, and any additional consideration.

            It is already in order of likelihood, so the chance of spreading (probability) will always be highest at the top and lowest at the bottom.
            Make sure you are considering limited travel methods and slower spread depending on periods. Lower probabilities for distant cities reflect the transportation constraints of the period.
            
            [Delimiter] #################################################G#U#H#################################################
            " }
        }
    };


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        apiKey = Environment.GetEnvironmentVariable("MY_API_KEY");


        if (string.IsNullOrEmpty(apiKey))
        {
            throw new Exception("API key not found. Please set it in the environment variables.");
        }

        // StartCoroutine(MainCoroutine("London, 2000"));
    }

    public IEnumerator MainCoroutine(string prompt)
    {
        // while (true)
        // {
        //     Debug.Log("Your question (type 'exit' to end): ");
        //     string prompt = Console.ReadLine();
        //     if (prompt.ToLower() == "exit")
        //     {
        //         break;
        //     }
        //     yield return ConverseWithMemory(prompt);
        // }

        // Get this prompt as patient zero from the UI
        yield return ConverseWithMemory(prompt);
    }

    private async Task ConverseWithMemory(string prompt)
    {
        
        if (visitedCities.Count != 0)
        {
            Debug.Log("City already visited, using prompt 3");
            string visitedCitiesString = string.Join("; ", visitedCities);
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + visitedCitiesString);
            conversationMemory = new List<Dictionary<string, string>>
    {
        new Dictionary<string, string>
        {
            { "role", "system" },
            { "content", @$"
            We are simulating a virus/infection outbreak in [city] during the year [year].
                    You are an expert in this field, and you have some basic idea about how this is going to spread from [city]. 
                    This is your thought process:
                    1. The virus/infection will spread to nearby cities, carried by several factors, the biggest being distance and geographical ease of access. 
                    2. The virus/infection will next spread from major trade routes, through ships, planes and carriages depending on the period (year). 
                    3. The virus/infection will eventually spread to all cities.
                    In this scenario, all external factors, such as infection rate, population density, trading frequency, and medical advancements, are automatically taken into consideration and weighted as appropriate so we can ignore them.
                    What you, as the expert, care about, is as follows:
                    - Geographical factors, natural barriers working to prevent the spread. 
                    - Trade paths, flight paths, etc and overall transportation networks and the frequency of the people accessing this location. 

                    The importance of geographical factors can be seen in things like Iceland & Japan, where it benefitted from its isolated location throughout history, but recently it is more common for them to get infected as planes allow for very easy access. 
                    This is why the year [year] needs to be put into consideration. 
                    This is the same for one of the most important factors, the transportation network and trade routes. They change rapidly depending on the time period, so it is important to initially list out all the routes and where they connect to. 
                    I want you to output all the locations you think will be infected if [city] begins spreading a virus/infection during the year [year].

                    HOWEVER, KEEP IN MIND, YOU CANNOT USE ANY OF THE CITY, YEAR COMBOS IN THE SET BELOW:

                    {visitedCitiesString}

                    ALL CITIES MUST BE DIFFERENT FROM WHAT IS IN THE LIST

                    If you cannot produce a response as per the instructions, output a random city that is not in the set.

                    Go through your thought process and output as follows:
                    [infected-city-name, year, probability]

                    year is just [year], a constant value
                    probability: The chance to spread to this city from [city], eg 0.90, 0.87, etc
                    probability Rule: start at 0.6 and go down from there

                    Give as many examples as possible, but make sure you are following only this format. 
                    [infected-city-name, year, probability]

                    Do not output anything else except what is requested. 
                    If you cannot come up with an answer, output a random city that is not in the set. 

                    Output should have absolutely no sentences or explanation. 
                    just: [infected-city-name, year, probability]

                    IMPORTANT: Make sure to rearrange it in order of likelihood based on proximity, known trade routes, and ease of access, and any additional consideration.

                    It is already in order of likelihood, so the chance of spreading (probability) will always be highest at the top and lowest at the bottom.
                    Make sure you are considering limited travel methods and slower spread depending on periods. Lower probabilities for distant cities reflect the transportation constraints of the period.
            [Delimiter] #################################################G#U#H#################################################
            " }
        }
    };


            


        }
        conversationMemory.Add(new Dictionary<string, string> { { "role", "user" }, { "content", prompt } });

        var requestBody = new
        {
            model = model,
            messages = conversationMemory,
            temperature = 0.7
        };

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

        var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
        var responseString = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<ResponseObject>(responseString);

        string assistantMessage = responseObject.choices[0].message.content;
        Debug.Log("Chatbot 2/3 response: " + assistantMessage);

        // Remove duplicates from the response

        List<(string, float)> parsedResponse = ParseResponse(assistantMessage);
        // foreach (var item in parsedResponse)
        // {
        //     Debug.Log($"City, Year: {item.Item1}, Probability: {item.Item2}");
        // }

        conversationMemory.Add(new Dictionary<string, string> { { "role", "assistant" }, { "content", assistantMessage } });

        if (conversationMemory.Count > 10)
        {
            conversationMemory.RemoveAt(1);
        }
        StartCoroutine(waitforresponse(parsedResponse));
        // foreach (var item in parsedResponse)
        // {
        //     visitedCities.Add(item.Item1);
        //     if (UnityEngine.Random.value <= item.Item2)
        //     {
        //         Debug.Log($"City, Year: {item.Item1}, Probability: {item.Item2} Triggered!");
        //         StartCoroutine(prompt1.MainCoroutine(item.Item1));
        //     }
        //     else {
        //         Debug.Log($"City, Year: {item.Item1}, Probability: {item.Item2} Not Triggered!");
        //     }
        //     // Debug.Log($"City: {item.Item1}, Probability: {item.Item2}");
        // }
    }

    IEnumerator waitforresponse(List<(string, float)> parsedResponse)
    {
        foreach (var item in parsedResponse)
        {
        yield return new WaitForSeconds(5);
            visitedCities.Add(item.Item1);
            if (UnityEngine.Random.value <= item.Item2)
            {
                Debug.Log($"City, Year: {item.Item1}, Probability: {item.Item2} Triggered!");
                StartCoroutine(prompt1.MainCoroutine(item.Item1));
            }
            else {
                Debug.Log($"City, Year: {item.Item1}, Probability: {item.Item2} Not Triggered!");
            }
            // Debug.Log($"City: {item.Item1}, Probability: {item.Item2}");
        }
    }

    private List<(string, float)> ParseResponse(string response)
    {
        List<(string, float)> result = new List<(string, float)>();
        string[] lines = response.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            string[] parts = line.Trim(new char[] { '[', ']', ' ' }).Split(new[] { ", " }, StringSplitOptions.None);
            if (parts.Length == 3)
            {
                string cityYear = $"{parts[0]}, {parts[1]}";
                if (float.TryParse(parts[2], out float probability))
                {
                    result.Add((cityYear, probability));
                }
            }
        }

        return result;
    }
    public class ParsedResponse
    {
        public string City { get; set; }
        public string Year { get; set; }
        public string Population { get; set; }
        public string Area { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public int ConnectionProbability { get; set; }
        public int SanitationLevel { get; set; }
        public int PublicHealthLevel { get; set; }
        public int EconomicStability { get; set; }
    }

    // Define the ResponseObject class to match the structure of the JSON response
    public class ResponseObject
    {
        public Choice[] choices { get; set; }
    }

    public class Choice
    {
        public Message message { get; set; }
    }

    public class Message
    {
        public string content { get; set; }
    }

    // Update is called once per frame

}
