using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHp = 100f;
    protected float currentHp;
    public Fighter fighter;
    public bool inBlock = false;
    public Slider hpSlider;
    public Image hpImage;
    public Color lowColor;
    public Color fullColor;

    public void Awake()
    {
        currentHp = maxHp;
    }

    public virtual void SetUpHealth()
    {
        hpSlider.value = currentHp / maxHp;
        hpImage.color = Color.Lerp(lowColor, fullColor, currentHp / maxHp);
    }

    public virtual void TakeDamage(float dmg)
    {
        if (inBlock)
        {
            inBlock = false;
            return;
        }
        //TODO: Add resistant calculation
        //TODO: Monster: If take dmg, chance for attack back -> start turn
        currentHp -= dmg;

        SetUpHealth();

        if (currentHp <= 0)
        {
            fighter.Die();
        }
    }

    public void SetUpBlock()
    {
        inBlock = true;
    }
}
