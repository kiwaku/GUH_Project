using TMPro;
using UnityEngine;

public class BCE : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public void ToggleYears()
    {
        if (text.text == "BCE")
        {
            text.text = "AD";
        }
        else
        {
            text.text = "BCE";
        }
    }
}
