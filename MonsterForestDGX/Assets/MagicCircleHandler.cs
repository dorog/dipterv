using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MagicCircleHandler : MonoBehaviour
{
    public bool canAttack = false;

    private Vector3 mousePosition;

    private Vector3 handPosition;
    private Quaternion handRotaion;

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

    private void Start()
    {
        //coolDownRectTransform = coolDown.transform.GetComponent<RectTransform>();
    }

    public void ResetCooldown()
    {
        resetedCd = true;
    }

    private void Update()
    {
        if (player.InBattle && player.CanAttack())
        {
            //Remove later, add def spells
            /*if (Input.GetKeyDown(KeyCode.Keypad0) && !canAttack)
            {
                health.SetUpBlock();
            }*/

            //feedback.text = "Fix: " + handPosition.ToString() + "\nHand: " + hand.transform.position;

            InputDevice device = InputDevices.GetDeviceAtXRNode(input);
            device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryBtn);
            device.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryBtn);

            if (secondaryBtn && !inCast)
            {
                inCast = true;
                mousePosition = Input.mousePosition;
                handPosition = hand.transform.position;
                handRotaion = hand.transform.rotation;

                magicCircle.transform.position = hand.transform.position + hand.transform.forward * magicCircleExtraDistance;
                magicCircle.transform.rotation = hand.transform.rotation;
                magicCircle.SetActive(true);
            }
            if (primaryBtn && inCast)
            {
                inCast = false;
                magicCircle.SetActive(false);

                //feedback.text = hand.transform.position.ToString();
                //Debug.Log(hand.transform.position.ToString());
            }
        }
    }

    public void Def()
    {
        //health.SetUpBlock();
        magicCircle.SetActive(false);
        inCast = false;
    }

    public void BattleEnd()
    {
        inCast = false;
        canAttack = false;
        magicCircle.SetActive(false);

        ClearDelegates();
    }

    public void CastSpell(SpellResult spellResult)
    {
        //Vector3 position = Camera.main.ScreenToWorldPoint(mousePosition);
        //GameObject spell = Instantiate(spellResult.spell, position + transform.forward, Camera.main.transform.rotation);
        Vector3 position = handPosition;
        GameObject spell = Instantiate(spellResult.spell, magicCircle.transform.position, magicCircle.transform.rotation);
        PlayerSpell spellAttack = spell.GetComponent<PlayerSpell>();
        spellAttack.coverage = spellResult.coverage;

        SetUpCoolDown(spellResult.cooldown);

        if (!canAttack)
        {
            //health.SetUpBlock();
        }

        magicCircle.SetActive(false);
    }

    private IEnumerator Countdown(float cd)
    {
        float duration = cd;
        while (duration > 0 && !resetedCd)
        {
            //normalizedTime += Time.deltaTime / duration;
            coolDown.text = duration.ToString();
            duration -= Time.deltaTime;
            //slider.value -= Time.deltaTime / duration;
            yield return null;
        }

        coolDown.text = "Ready";
        //coolDown.gameObject.SetActive(false);
        inCast = false;
        resetedCd = false;
    }

    private void SetUpCoolDown(float cd)
    {
        castSpellDelegateEvent?.Invoke();

        if (!resetedCd)
        {
            coolDown.text = cd.ToString();
            /*coolDownRectTransform.sizeDelta = new Vector2(multiplyCD * cd, coolDownRectTransform.sizeDelta.y);
            coolDown.value = 1;
            coolDown.gameObject.SetActive(true);*/
            StartCoroutine(Countdown(cd));
        }
        else
        {
            inCast = false;
            resetedCd = false;
        }
    }

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
        var matrix = hand.transform.worldToLocalMatrix;

        //Debug.Log("Hand fix: " + handPosition);
        //Debug.Log("Hand trans: " + hand.transform.position);
        //return hand.transform.position - handPosition;
        return hand.transform.position;
    }

    public Vector3 GetNormal()
    {
        Debug.Log("Normal: " + magicCircle.transform.forward);
        return magicCircle.transform.forward;
    }

    public Transform GetTransform()
    {
        return magicCircle.transform;
    }
}
