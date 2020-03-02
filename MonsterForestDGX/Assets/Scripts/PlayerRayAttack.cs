using UnityEngine;

public class PlayerRayAttack : IAttack
{
    public float dmg = 10;
    public Vector3 mousePosition = Vector3.zero;

    public override float Attack()
    {
        Debug.Log("Attack");
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            MonsterHit monsterHit = hit.collider.gameObject.GetComponent<MonsterHit>();
            if(monsterHit != null)
            {
                monsterHit.TakeDamage(dmg);
            }
        }
        else
        {
            Debug.Log("No hit!");
        }

        return 0;
    }
}
