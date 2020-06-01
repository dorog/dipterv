using UnityEngine;
using UnityEngine.UI;

public class SpellElementInfoUI : MonoBehaviour
{
    public GameObject ui;

    public Text spellNameText;
    public Text difficultyValueText;
    public Text valueTitleText;
    public Text valueValueText;
    public Text cooldownValueText;
    public Text levelText;

    public void ShowUI(ISpellPattern spellPattern)
    {
        spellNameText.text = spellPattern.GetElementType().ToString();
        difficultyValueText.text = "" + spellPattern.GetDifficulty();
        difficultyValueText.color = spellPattern.GetDifficultyColor();
        valueTitleText.text = "" + spellPattern.GetSpellTypeUI() + ":";
        valueValueText.text = "" + spellPattern.GetTypeValueUI();
        cooldownValueText.text = spellPattern.GetCooldownUI();
        levelText.text = "" + spellPattern.GetLevelUI();

        ui.SetActive(true);
    }

    public void Exit()
    {
        ui.SetActive(false);
    }
}
