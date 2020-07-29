using UnityEngine;

public class DefendingTrainingDoll : MonoBehaviour, IEnemy
{
    public BattleManager battleManager;

    public Player player;

    public TrainingCampUI trainingCampUI;

    public CooldownResetPetAbility cooldownReset;

    public void Appear()
    {
        
    }

    public void Disappear()
    {
        
    }

    public Health GetHealth()
    {
        return GetComponent<Health>();
    }

    public void React()
    {
        
    }

    public void ResetMonster()
    {
        
    }

    public void StartTurn()
    {
        trainingCampUI.EnableUI();

        cooldownReset.Init(battleManager.player);

        battleManager.PlayerTurn();
    }

    public void FinishedTraining()
    {
        trainingCampUI.DisableUI();

        cooldownReset.DisableAbility();

        player.FinishedTraining();
        battleManager.FinishedTraining();
    }
}
