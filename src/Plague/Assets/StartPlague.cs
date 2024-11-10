using TMPro;
using UnityEngine;

public class StartPlague : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] PatientZero input;
    [SerializeField] TextMeshProUGUI button;
    [SerializeField] TMP_InputField city;
    [SerializeField] TMP_InputField year;
    [SerializeField] TMP_InputField recRate;
    [SerializeField] TMP_InputField infRate;
    [SerializeField] TMP_InputField incRate;
    [SerializeField] TMP_InputField mortRate;
    [SerializeField] SpawnOnLongLat spawnOnLongLat;
    public void ExecuteAnim()
    {
        animator.SetBool("hasInput", true);
        if (int.Parse(year.text) > 1930f)
        {
            Prompt2.plane = $"During the years {year.text}, commercial plane travel become exponentially common, you eventually want to infect all locations around the world. keep this in mind.";
        }
        string prompt = city.text + ", " + year.text + " " + button.text;
        StartCoroutine(input.MainCoroutine(prompt));
        spawnOnLongLat.passedRecRate = float.Parse(recRate.text);
        spawnOnLongLat.passedIncRate = float.Parse(incRate.text);
        spawnOnLongLat.passedInfRate = float.Parse(infRate.text);
        spawnOnLongLat.passedMortRate = float.Parse(mortRate.text);
    }
}
