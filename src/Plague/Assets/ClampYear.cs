using TMPro;
using UnityEngine;

public class ClampYear : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TMP_InputField text;
    public void SetYear()
    {
        if (int.Parse(text.text) > 2020)
        {
            text.text = "2020";
        }
        if (int.Parse(text.text) < 0)
        {
            text.text = "0";
        }
    }
}
