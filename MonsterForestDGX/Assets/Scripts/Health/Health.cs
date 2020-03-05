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
    public Resistant resistant;

    public void Awake()
    {
        currentHp = maxHp;
    }

    public virtual void SetUpHealth()
    {
        hpSlider.value = currentHp / maxHp;
        hpImage.color = Color.Lerp(lowColor, fullColor, currentHp / maxHp);
    }

    public virtual void TakeDamage(float dmg, AttackType magicType)
    {
        if (inBlock)
        {
            BlockSet();
            return;
        }

        float realDmg = resistant.CalculateDmg(dmg, magicType);

        currentHp -= realDmg;

        SetUpHealth();

        if (currentHp <= 0)
        {
            fighter.Die();
        }
    }

    public virtual void SetUpBlock()
    {
        inBlock = true;
    }

    public virtual void BlockSet()
    {
        inBlock = false;
    }
}
