using TMPro;
using UnityEngine;

public class StartPlague : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] PatientZero input;
    [SerializeField] TextMeshProUGUI button;
    [SerializeField] TMP_InputField city;
    [SerializeField] TMP_InputField year;
    public void ExecuteAnim()
    {
        animator.SetBool("hasInput", true);
        string prompt = city.text + ", " + year.text + " " + button.text;
        StartCoroutine(input.MainCoroutine(prompt));
    }
}
