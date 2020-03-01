using UnityEngine;

public class PlayerRayAttack : IAttack
{
    public float dmg = 10;

    public override float Attack()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            MonsterHit monsterHit = hit.collider.gameObject.GetComponent<MonsterHit>();
            if(monsterHit != null)
            {
                monsterHit.TakeDamage(dmg);
            }
        }

        return 0;
    }
}
