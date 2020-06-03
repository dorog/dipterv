using UnityEngine;
using UnityEngine.UI;

public class SpellElementInfoUI : MonoBehaviour
{
    [Header("Update UI Settings")]
    public GameObject updateUI;

    public Text spellNameTextUpdate;

    public Text difficultyValueTextActual;
    public Text difficultyValueTextNext;

    public Text valueTitleTextUpdate;

    public Text valueValueTextActual;
    public Text valueValueTextNext;

    public Text cooldownValueTextActual;
    public Text cooldownValueTextNext;

    public Text levelTextActual;
    public Text levelTextNext;

    [Header("Maxed UI Settings")]
    public GameObject buyOrMaxedUI;
    public Text spellNameText;
    public Text difficultyValueText;
    public Text valueTitleText;
    public Text valueValueText;
    public Text cooldownValueText;
    public Text levelText;

    private int spellId = -1;
    private ISpellPattern spellPattern = null;

    public void ShowUI(int id, ISpellPattern spellPattern, bool refresh = false)
    {
        if (!refresh && id == spellId)
        {
            spellId = -1;
            buyOrMaxedUI.SetActive(false);
            updateUI.SetActive(false);

            return;
        }

        spellId = id;
        this.spellPattern = spellPattern;

        if (spellPattern.IsMaxed() || spellPattern.GetLevelValue() == 0)
        {
            spellNameText.text = spellPattern.GetElementType().ToString();
            difficultyValueText.text = spellPattern.GetDifficulty()[0];
            difficultyValueText.color = spellPattern.GetDifficultyColor()[0];
            valueTitleText.text = spellPattern.GetSpellTypeUI() + ":";
            valueValueText.text = spellPattern.GetTypeValueUI()[0];
            cooldownValueText.text = spellPattern.GetCooldownUI()[0];
            levelText.text = spellPattern.GetLevelUI()[0];

            buyOrMaxedUI.SetActive(true);
            updateUI.SetActive(false);
        }
        else
        {
            spellNameTextUpdate.text = spellPattern.GetElementType().ToString();

            string[] difficulties = spellPattern.GetDifficulty();
            difficultyValueTextActual.text = difficulties[0];
            difficultyValueTextNext.text = difficulties[1];


            Color[] difficultyColors = spellPattern.GetDifficultyColor();
            difficultyValueTextActual.color = difficultyColors[0];
            difficultyValueTextNext.color = difficultyColors[1];

            //Attack -> def?
            valueTitleTextUpdate.text = spellPattern.GetSpellTypeUI() + ":";

            string[] values = spellPattern.GetTypeValueUI();
            valueValueTextActual.text = values[0]; ;
            valueValueTextNext.text = values[1];

            string[] cooldowns = spellPattern.GetCooldownUI();
            cooldownValueTextActual.text = cooldowns[0];
            cooldownValueTextNext.text = cooldowns[1];

            string[] levels = spellPattern.GetLevelUI();
            levelTextActual.text = levels[0];
            levelTextNext.text = levels[1];

            buyOrMaxedUI.SetActive(false);
            updateUI.SetActive(true);
        }
    }

    public void RefreshInfo(int id)
    {
        if(spellId == id)
        {
            ShowUI(spellId, spellPattern, true);
        }
    }

    public void Exit()
    {
        buyOrMaxedUI.SetActive(false);
        updateUI.SetActive(false);
    }
}
