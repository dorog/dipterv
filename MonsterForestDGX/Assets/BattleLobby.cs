﻿using UnityEngine;

public class BattleLobby : MonoBehaviour
{
    public BattleManager battleManager = null;
    public ResistantValue water;
    public ResistantValue earth;
    public ResistantValue fire;
    public ResistantValue air;

    public void StartBattle()
    {
        battleManager.BattleStart();
    }

    public void SetResistantValues(Resistant resistant)
    {
        water.SetValue(ElementType.Water.ToString(), resistant.water);
        earth.SetValue(ElementType.Earth.ToString(), resistant.earth);
        fire.SetValue(ElementType.Fire.ToString(), resistant.fire);
        air.SetValue(ElementType.Air.ToString(), resistant.air);
    }

    public void Run()
    {
        battleManager.Run();

        gameObject.SetActive(false);
    }
}
