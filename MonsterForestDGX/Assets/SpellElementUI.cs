using UnityEngine;
using UnityEngine.UI;

public class SpellElementUI : MonoBehaviour
{
    public Image iconImage;
    public Text spellNameText;
    public Text requiredExpValue;
    public Text buttonText;
    public Button button;

    private int id;
    private ISpellPattern spellPattern;
    private SpellElementInfoUI spellElementInfoUI;

    public void SetupUI(int id, ISpellPattern spellPattern, SpellElementInfoUI info, int exp)
    {
        this.id = id;
        this.spellPattern = spellPattern;
        spellElementInfoUI = info;

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
        else
        {
            buttonText.text = "Update";
        }

        AvailableExp(exp);
    }

    public void Refresh(int exp)
    {
        spellPattern.LevelUp();
        SetupUI(id, spellPattern, spellElementInfoUI, exp);
    }

    public void AvailableExp(int exp)
    {
        bool enable = (spellPattern.GetRequiredExpValue() <= exp);
        button.interactable = enable;
        requiredExpValue.color = enable ? Color.white : Color.red;
    }

    public void ShowInfo()
    {
        spellElementInfoUI.ShowUI(id, spellPattern);
    }

    public void BuyOrUpdate()
    {
        DataManager dataManager = DataManager.GetInstance();
        dataManager.LevelUpSpell(id,spellPattern.GetRequiredExpValue());
    }
}
