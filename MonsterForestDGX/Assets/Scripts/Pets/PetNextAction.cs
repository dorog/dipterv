using System;

[Serializable]
public abstract class PetNextAction : UpdatePetAbility
{
    public float effectAmount = 10;
    public float minWait = 5;
    public float maxWait = 10;
    protected bool inWait = true;

    public override void Init(Player player)
    {
        inWait = false;
    }

    protected void SetUpNextEffect()
    {
        inWait = true;
        float waitTime = UnityEngine.Random.Range(minWait, maxWait);
        Invoke(nameof(WaitEnd), waitTime);
    }

    private void WaitEnd()
    {
        inWait = false;
    }
}
