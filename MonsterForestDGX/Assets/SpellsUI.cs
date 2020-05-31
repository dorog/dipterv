using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsUI : MonoBehaviour
{
    public Text xpText;
    public SpellElementUI spellElementUI;
    public Transform content;

    public GameObject menu;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            menu.SetActive(!menu.activeSelf);
        }
    }

    public void SetupUI(List<ISpellPattern> spellPatterns)
    {
        for(int i = 0; i < spellPatterns.Count; i++)
        {
            SpellElementUI spellElementUiInstance = Instantiate(spellElementUI, content);
            spellElementUiInstance.SetupUI(spellPatterns[i].GetIcon(), spellPatterns[i].GetElementType().ToString(), spellPatterns[i].GetSpellType(), spellPatterns[i].GetTypeValue(),
                spellPatterns[i].GetCooldown(), spellPatterns[i].GetLevel());
        }
    }

    public void SetXp(int value)
    {
        xpText.text = value + " XP";

        //Set Update buttons
    }
}
