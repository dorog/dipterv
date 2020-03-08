using UnityEngine;
using System;

[Serializable]
public class CooldownResetPetAbility : PetAbility
{
    private Player player;
    [Range(0, 100)]
    public float resetChance = 50;

    public override void Init(Player _player)
    {
        player = _player;
        player.castSpellDelegateEvent += ResetCooldown;
    }

    private void ResetCooldown()
    {
        float random = UnityEngine.Random.Range(0, 100);
        Debug.Log(random + ", " + resetChance);
        if (random <= resetChance)
        {
            player.ResetCooldown();
        }
    }
}
