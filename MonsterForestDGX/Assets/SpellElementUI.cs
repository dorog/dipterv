using UnityEngine;
using UnityEngine.UI;

public class SpellElementUI : MonoBehaviour
{
    public Image iconImage;
    public Text spellNameText;
    public Text typeText;
    public Text typeValueText;
    public Text cooldownText;
    public Text levelText;

    public void SetupUI(Sprite icon, string spellName,  string typeName, float typeValue, float cooldown, string lvl)
    {
        iconImage.sprite = icon;
        spellNameText.text = spellName;
        typeText.text = typeName + ":";
        typeValueText.text = typeValue.ToString();
        cooldownText.text = cooldown.ToString();
        levelText.text = lvl;
    }
}
