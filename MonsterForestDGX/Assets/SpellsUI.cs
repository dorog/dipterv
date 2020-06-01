using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsUI : MenuUI
{
    public Text expText;
    public SpellElementUI spellElementUI;
    public Transform content;
    public SpellElementInfoUI SpellElementInfoUI;

    public void SetupUI(List<ISpellPattern> spellPatterns)
    {
        for(int i = 0; i < spellPatterns.Count; i++)
        {
            SpellElementUI spellElementUiInstance = Instantiate(spellElementUI, content);
            spellElementUiInstance.SetupUI(spellPatterns[i], SpellElementInfoUI);
        }
    }

    public void SetExp(int value)
    {
        expText.text = value + " EXP";

        //Set Update buttons
    }
}
