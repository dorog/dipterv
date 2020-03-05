using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Text hp;
    public GameObject block;

    public override void SetUpHealth()
    {
        base.SetUpHealth();
        
        hp.text = Mathf.Ceil(currentHp).ToString() + "/" + maxHp.ToString();
    }

    public override void SetUpBlock()
    {
        base.SetUpBlock();
        block.SetActive(true);
    }

    public override void BlockSet()
    {
        base.BlockSet();
        block.SetActive(false);
    }

    public void BlockDown()
    {
        inBlock = false;
        block.SetActive(true);
    }
}
