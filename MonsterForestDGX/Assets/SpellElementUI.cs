using UnityEngine;
using UnityEngine.UI;

public class SpellElementUI : MonoBehaviour
{
    public Image iconImage;
    public Text spellNameText;

    public Text requiredExpValue;

    private ISpellPattern spellPattern;
    private SpellElementInfoUI spellElementInfoUI;

    public void SetupUI(ISpellPattern spellPattern, SpellElementInfoUI info)
    {
        spellElementInfoUI = info;
        this.spellPattern = spellPattern;

        iconImage.sprite = spellPattern.GetIcon();
        spellNameText.text = spellPattern.GetElementType().ToString();
        requiredExpValue.text = "---";
    }

    public void ShowInfo()
    {
        spellElementInfoUI.ShowUI(spellPattern);
    }
}
