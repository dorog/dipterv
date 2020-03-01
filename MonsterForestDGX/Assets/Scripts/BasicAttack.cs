using UnityEngine;

[System.Serializable]
public class BasicAttack : IAttack
{
    public GameObject attack;
    public Vector3 position;

    public override float Attack()
    {
        Instantiate(attack, position, Quaternion.identity);

        return 0;
    }
}
