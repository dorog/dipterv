using UnityEngine;

public class TimeDamageBlock : DamageBlock
{
    public float durationTime = 2f;

    private bool working = false;
    private float actualDurationTime = 0;
    private float actualBlockValue = 0;

    public GameObject blockUI;

    public void StartBlock()
    {
        actualBlockValue = blockValue;
        actualDurationTime = durationTime;

        working = true;

        blockUI.SetActive(true);
    }

    private void Update()
    {
        if (working)
        {
            actualDurationTime -= Time.deltaTime;

            if (actualDurationTime <= 0)
            {
                working = false;
                blockUI.SetActive(false);
            }
        }
    }

    public override float GetCalculatedDamage(float damage)
    {
        if (working)
        {
            return CalculateDamaga(actualBlockValue, damage);
        }

        return damage;
    }
}
