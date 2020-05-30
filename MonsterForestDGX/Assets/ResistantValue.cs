using UnityEngine;
using UnityEngine.UI;

public class ResistantValue : MonoBehaviour
{
    public Text typeText;
    public Text valueText;

    public void SetValue(string type, int value)
    {
        typeText.text = type + ":";
        valueText.text = value.ToString() + "%";
    }
}
