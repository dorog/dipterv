using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class TrainingCampUI : MonoBehaviour
{

    public Slider cooldownSlider;

    public CooldownResetPetAbility cooldownReset;

    public GameObject root;

    public Text percent;

    public MagicCircleHandler magicCircleHandler;
    private SpellManager spellManager;
    public Text coverage;

    public Dropdown dropdown;

    public SpellGuideDrawer spellGuideDrawer;

    private void Start()
    {
        spellManager = SpellManager.GetInstance();
        SetCooldownChance();
    }

    public void EnableUI()
    {
        root.SetActive(true);
        magicCircleHandler.castSpellDelegateEvent += GetSpellCoverage;
    }

    public void DisableUI()
    {
        root.SetActive(false);
        magicCircleHandler.castSpellDelegateEvent -= GetSpellCoverage;
    }

    public void StepSlider(float value)
    {
        cooldownSlider.value += value;
    }

    public void SetCooldownChance()
    {
        cooldownReset.resetChance = cooldownSlider.value * 100;
        percent.text = (cooldownSlider.value * 100) + "%";
    }

    private void GetSpellCoverage()
    {
        coverage.text = spellManager.GetCastedCoverage() * 100 + "%";
    }

    public void AskForGuide()
    {
        //dropdown.value
        OptionData option = dropdown.options[dropdown.value];
        BasePatternSpell basePatternSpell = spellManager.GetSpellPoints(option.text);

        if(basePatternSpell == null)
        {
            spellGuideDrawer.ClearGuide();
        }
        else
        {
            spellGuideDrawer.DrawGuide(basePatternSpell.SpellPatternPoints.GetPoints());
        }
    }
}
