using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollHealth : Health
{
    public override void SetUpHealth()
    {

    }

    public override void SetDamageBlock()
    {
        
    }

    protected override float GetBlockedDamage(float dmg)
    {
        return 0;
    }
}
