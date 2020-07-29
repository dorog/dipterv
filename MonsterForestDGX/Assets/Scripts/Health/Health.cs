using UnityEngine;
using UnityEngine.UI;

public abstract class Health : MonoBehaviour
{
    public float maxHp = 100f;
    public float currentHp;
    public Fighter fighter;
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
        //hpSlider.value = currentHp / maxHp;
        //hpImage.color = Color.Lerp(lowColor, fullColor, currentHp / maxHp);
    }

    public virtual void TakeDamage(float dmg, ElementType magicType)
    {
        float realDmg = resistant.CalculateDmg(dmg, magicType);

        //TODO: Add resistant
        realDmg = GetBlockedDamage(realDmg);

        currentHp -= realDmg;

        SetUpHealth();

        if (currentHp <= 0)
        {
            fighter.Die();
        }
    }

    protected abstract float GetBlockedDamage(float dmg);

    public abstract void SetDamageBlock();

    public void ResetHealth()
    {
        currentHp = maxHp;
        SetUpHealth();
    }
}
