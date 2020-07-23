using System;

[Serializable]
public class AttackPetAbility : PetNextAction
{
    private Player player;
    private Health monsterHealth;

    public override void Init(Player _player)
    {
        player = _player;
        monsterHealth = player.battleManager.monster.GetHealth();
        base.Init(player);
    }

    public override void UpdateEffect()
    {
        if (player.CanAttack() && !inWait)
        {
            monsterHealth.TakeDamage(effectAmount, ElementType.TrueDamage);
            SetUpNextEffect();
        }
    }
}
