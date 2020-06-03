using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsUI : MenuUI
{
    public Text expText;
    public SpellElementUI spellElementUI;
    public Transform content;
    public SpellElementInfoUI SpellElementInfoUI;

    private List<SpellElementUI> spellElementUIs = new List<SpellElementUI>();

    private int exp = 0;

    private void Start()
    {
        DataManager dataManager = DataManager.GetInstance();
        dataManager.spellLevelChangedEvent += LevelChanged;
    }

    public void SetupUI(List<ISpellPattern> spellPatterns)
    {
        for(int i = 0; i < spellPatterns.Count; i++)
        {
            SpellElementUI spellElementUiInstance = Instantiate(spellElementUI, content);
            spellElementUiInstance.SetupUI(i, spellPatterns[i], SpellElementInfoUI, exp);

            spellElementUIs.Add(spellElementUiInstance);
        }

        SetExp(exp);
    }

    public void SetExp(int value)
    {
        expText.text = value + " EXP";
        exp = value;

        //Set Update buttons
        for(int i = 0; i < spellElementUIs.Count; i++)
        {
            spellElementUIs[i].AvailableExp(value);
        }
    }

    public void LevelChanged(int id)
    {
        spellElementUIs[id].Refresh(exp);
        SpellElementInfoUI.RefreshInfo(id);
    }
}
