﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Player : Fighter
{
    public MagicCircleHandler magicCircleHandler;

    public BattleManager battleManager;

    public Teleport teleport;

    public bool InLobby = false;
    public bool InBattle = false;
    public bool InMenu = false;

    public PlayerHealth playerHealth;

    public Color DefenseGridColor;
    public Color AttackGridColor;
    public SpriteRenderer grid;

    private GameObject petGO;

    private PetManager petManager;
    private SpellTreeManager spellTreeManager;
    private SpellManager spellManager;
    private AliveMonstersManager aliveMonstersManager;

    public BattleLobby battleLobbyUI;

    private void Start()
    {
        health.SetUpHealth();

        petManager = PetManager.GetInstance();
        spellTreeManager = SpellTreeManager.GetInstance();
        aliveMonstersManager = AliveMonstersManager.GetInstance();
        spellManager = SpellManager.GetInstance();
    }

    public void BattleStarted()
    {
        battleLobbyUI.gameObject.SetActive(false);
        InLobby = false;

        InBattle = true;

        GameObject playerPet = petManager.GetPet();
        if(playerPet != null)
        {
            petGO = Instantiate(playerPet, playerPet.transform.position + new Vector3(transform.position.x, 0, transform.position.z) + transform.right * 2, transform.rotation);

            Pet pet = petGO.GetComponent<Pet>();
            pet.AddPlayer(this);
        }
    }

    public void BattleEnd(int id)
    {
        InBattle = false;
        health.inBlock = false;
        playerHealth.BlockDown();


        magicCircleHandler.BattleEnd();

        if(petGO != null)
        {
            Destroy(petGO);
        }

        spellManager.Won();
        aliveMonstersManager.Won(id);
    }

    public override void Die()
    {
        magicCircleHandler.Die();
        battleManager.PlayerDied();
    }

    public override void StartTurn()
    {
        AttackTurn();
    }

    public void DefTurn()
    {
        grid.color = DefenseGridColor;
        magicCircleHandler.DefTurn();
    }

    public void AttackTurn()
    {
        grid.color = AttackGridColor;
        magicCircleHandler.AttackTurn();
    }

    public void Battle(BattleManager battleManager, Resistant monsterResistant)
    {
        InLobby = true;
        this.battleManager = battleManager;
        battleLobbyUI.battleManager = battleManager;
        battleLobbyUI.SetResistantValues(monsterResistant);

        battleLobbyUI.gameObject.SetActive(true);
    }

    public bool CanMove()
    {
        return !(InLobby || InBattle || InMenu);
    }

    public void MenuState(bool state)
    {
        InMenu = state;
    }

    public void Run()
    {
        InLobby = false;
        teleport.TeleportToLastPosition();
    }

    public bool CanAttack()
    {
        return magicCircleHandler.canAttack;
    }

    public void CastSpell(SpellResult spellResult)
    {
        magicCircleHandler.CastSpell(spellResult);
    }

    public MagicCircleHandler GetMagicCircleHandler()
    {
        return magicCircleHandler;
    }
}
