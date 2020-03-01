using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Text hp;

    public override void SetUpHealth()
    {
        base.SetUpHealth();
        
        hp.text = Mathf.Ceil(currentHp).ToString() + "/" + maxHp.ToString();
    }
}
