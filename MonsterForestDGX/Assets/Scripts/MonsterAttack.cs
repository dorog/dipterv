using UnityEngine;

public class MonsterAttack : IAttack
{
    public Animator animator;

    public MonsterAttackClass[] normalAttacks;
    public MonsterAttackClass[] hardAttacks;
    public MonsterAttackClass[] ultimateAttacks;

    [Range (0, 100)]
    public float normalAttakChance = 70;
    [Range(0, 100)]
    public float hardAttackChance = 20;
    [Range(0, 100)]
    public float ultimateAttackChance = 10;

    public override float Attack()
    {
        float random = Random.Range(0, 101);

        string animationKey;
        int animation;
        float time;

        if (random <= normalAttakChance)
        {
            animation = GetOneRandomAnimation(normalAttacks);
            animationKey = normalAttacks[animation].animation;
            time = normalAttacks[animation].time;
            foreach(var attackGO in normalAttacks[animation].activate)
            {
                attackGO.SetActive(true);
            }
        }
        else if (random - normalAttakChance <= hardAttackChance)
        {
            animation = GetOneRandomAnimation(hardAttacks);
            animationKey = hardAttacks[animation].animation;
            time = hardAttacks[animation].time;
            foreach (var attackGO in hardAttacks[animation].activate)
            {
                attackGO.SetActive(true);
            }
        }
        else
        {
            animation = GetOneRandomAnimation(ultimateAttacks);
            animationKey = ultimateAttacks[animation].animation;
            time = ultimateAttacks[animation].time;
            foreach (var attackGO in ultimateAttacks[animation].activate)
            {
                attackGO.SetActive(true);
            }
        }

        animator.SetTrigger(animationKey);

        return time;
    }

    private int GetOneRandomAnimation(MonsterAttackClass[] animations)
    {

        int random = Random.Range(0, animations.Length);
        return random;
    }
}
