using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLobby : MonoBehaviour
{
    public BattleManager battleManager = null;

    public void StartBattle()
    {
        battleManager.BattleStart();
    }
}
