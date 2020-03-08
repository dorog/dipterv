using System;

[Serializable]
public class HealPetAbility : PetNextAction
{
    private PlayerHealth health;

    public override void Init(Player player)
    {
        health = player.GetComponent<PlayerHealth>();
        base.Init(player);
    }

    public override void UpdateEffect()
    {
        if (!inWait && (health.maxHp > health.currentHp))
        {
            health.Heal(effectAmount);
            SetUpNextEffect();
        }
    }
}
