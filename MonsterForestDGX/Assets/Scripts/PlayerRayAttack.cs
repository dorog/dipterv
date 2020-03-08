using UnityEngine;
using UnityEngine.UI;

public class PlayerRayAttack : IAttack
{
    public float dmg = 10;
    public Vector3 mousePosition = Vector3.zero;
    public ElementType magicType =  ElementType.TrueDamage;

    //TODO: Delete
    public void Start()
    {
        magicType = ElementType.TrueDamage;
    }

    //TODO: Delete
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            magicType = ElementType.Fire;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            magicType = ElementType.Water;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            magicType = ElementType.TrueDamage;
        }
    }

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
                monsterHit.TakeDamage(dmg, magicType);
            }
        }
        else
        {
            Debug.Log("No hit!");
        }

        return 0;
    }
}
