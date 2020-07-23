using UnityEngine;
using UnityEngine.UI;

public class DefendingTrainingDoll : MonoBehaviour, IEnemy
{
    public BattleManager battleManager;

    public Slider cooldownSlider;

    public CooldownResetPetAbility cooldownReset;

    public GameObject canvas;

    public Text percent;

    public Player player;

    public void StepSlider(float value)
    {
        cooldownSlider.value += value;
    }

    public void SetCooldownChance()
    {
        cooldownReset.resetChance = cooldownSlider.value * 100;
        percent.text = (cooldownSlider.value * 100) + "%";
    }

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
        cooldownReset.Init(battleManager.player);
        canvas.SetActive(true);

        Debug.Log("Doll: Start");
        battleManager.PlayerTurn();
    }

    public void FinishedTraining()
    {
        canvas.SetActive(false);

        cooldownReset.DisableAbility();

        player.FinishedTraining();
        battleManager.FinishedTraining();
    }
}
