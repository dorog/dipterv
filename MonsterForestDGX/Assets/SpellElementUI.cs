using UnityEngine;
using UnityEngine.UI;

public class SpellElementUI : MonoBehaviour
{
    public Image iconImage;
    public Text spellNameText;
    public Text requiredExpValue;
    public Text buttonText;
    public Button button;

    private ISpellPattern spellPattern;
    private SpellElementInfoUI spellElementInfoUI;

    public void SetupUI(ISpellPattern spellPattern, SpellElementInfoUI info)
    {
        spellElementInfoUI = info;
        this.spellPattern = spellPattern;

        iconImage.sprite = spellPattern.GetIcon();
        spellNameText.text = spellPattern.GetElementType().ToString();
        requiredExpValue.text = spellPattern.GetRequiredExp();

        if (spellPattern.GetLevelValue() == 0)
        {
            buttonText.text = "Buy";
        }
        else if (spellPattern.IsMaxed())
        {
            button.gameObject.SetActive(false);
            //Exp and value set false too?
        }
    }

    public void AvailableExp(int exp)
    {
        bool enable = (spellPattern.GetRequiredExpValue() <= exp);
        button.interactable = enable;
        requiredExpValue.color = enable ? Color.white : Color.red;
    }

    public void ShowInfo()
    {
        spellElementInfoUI.ShowUI(spellPattern);
    }
}
