using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class MagicCircleHandler : MonoBehaviour
{
    public bool canAttack = false;

    public GameObject magicCircle;

    private bool inCast = false;

    public Text coolDown;
    private readonly float multiplyCD = 100;
    private bool resetedCd = false;

    public Player player;

    public delegate void CastSpellDelegate();
    public CastSpellDelegate castSpellDelegateEvent;

    public GameObject hand;

    public XRNode input;

    public Text feedback;

    public float magicCircleExtraDistance = 2;

    public void ResetCooldown()
    {
        resetedCd = true;
    }

    private void Update()
    {
        if (player.InBattle && canAttack)
        {
            InputDevice device = InputDevices.GetDeviceAtXRNode(input);
            device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryBtn);
            device.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryBtn);

            if (secondaryBtn && !inCast)
            {
                inCast = true;

                magicCircle.transform.position = hand.transform.position + hand.transform.forward * magicCircleExtraDistance;
                magicCircle.transform.rotation = hand.transform.rotation;
                magicCircle.SetActive(true);
            }
            if (primaryBtn && inCast)
            {
                inCast = false;
                magicCircle.SetActive(false);
            }
        }
    }

    public void Def()
    {
        magicCircle.SetActive(false);
        inCast = false;
    }

    public void BattleEnd()
    {
        Debug.Log("BattleEnd");
        inCast = false;
        canAttack = false;
        magicCircle.SetActive(false);

        ClearDelegates();
    }

    public void CastSpell(SpellResult spellResult, BattleManager battleManager)
    {
        GameObject spell = Instantiate(spellResult.spell, magicCircle.transform.position, magicCircle.transform.rotation);
        SpellAttack spellAttack = spell.GetComponent<SpellAttack>();
        spellAttack.coverage = spellResult.coverage;

        spellAttack.SetBattleManager(battleManager);

        SetUpCoolDown(spellResult.cooldown);

        magicCircle.SetActive(false);
    }

    private IEnumerator Countdown(float cd)
    {
        float duration = cd;
        while (duration > 0 && !resetedCd)
        {
            coolDown.text = duration.ToString();
            duration -= Time.deltaTime;

            yield return null;
        }

        //TODO: Reset text after win
        coolDown.text = "Ready";

        inCast = false;
        resetedCd = false;
    }

    private void SetUpCoolDown(float cd)
    {
        castSpellDelegateEvent?.Invoke();

        if (!resetedCd)
        {
            coolDown.text = cd.ToString();
            StartCoroutine(Countdown(cd));
        }
        else
        {
            inCast = false;
            resetedCd = false;
        }
    }

    //TODO: Rename
    public void Die()
    {
        //Necessary?
        magicCircle.SetActive(false);
    }

    public void DefTurn()
    {
        canAttack = false;
        magicCircle.SetActive(false);
        inCast = false;
    }

    public void AttackTurn()
    {
        canAttack = true;
        magicCircle.SetActive(false);
        inCast = false;
    }

    private void ClearDelegates()
    {
        if (castSpellDelegateEvent != null)
        {
            Delegate[] delegates = castSpellDelegateEvent.GetInvocationList();
            foreach (Delegate d in delegates)
            {
                castSpellDelegateEvent -= (CastSpellDelegate)d;
            }
        }
    }

    public Vector3 GetPosition()
    {
        return hand.transform.position;
    }

    public Transform GetTransform()
    {
        return magicCircle.transform;
    }
}
