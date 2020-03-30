using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Player : Fighter
{
    public bool canAttack = false;
    public BattleManager battleManager;
    public Rigidbody rb;
    public bool InBattle = false;
    private bool inCast = false;

    private float rotation = 0;
    public float rotationSpeed = 10;
    private float movement = 0;
    public float movementSpeed = 10;
    private Vector3 mousePosition;
    public GameObject magicCircle;
    public PlayerHealth playerHealth;

    public Color DefenseGridColor;
    public Color AttackGridColor;
    public SpriteRenderer grid;

    public Slider coolDown;
    private RectTransform coolDownRectTransform;
    private readonly float multiplyCD = 100;
    private bool resetedCd = false;

    private GameObject petGO;

    public delegate void CastSpellDelegate();
    public CastSpellDelegate castSpellDelegateEvent;

    private PetManager petManager;
    private SpellTreeManager spellTreeManager;
    private AliveMonstersManager aliveMonstersManager;

    private void Start()
    {
        health.SetUpHealth();
        coolDownRectTransform = coolDown.transform.GetComponent<RectTransform>();

        petManager = PetManager.GetInstance();
        spellTreeManager = SpellTreeManager.GetInstance();
        aliveMonstersManager = AliveMonstersManager.GetInstance();
    }

    public void Battle()
    {
        InBattle = true;

        GameObject playerPet = petManager.GetPet();
        petGO = Instantiate(playerPet, playerPet.transform.position + new Vector3(transform.position.x, 0, transform.position.z) + transform.right * 2, transform.rotation);

        Pet pet = petGO.GetComponent<Pet>();
        pet.AddPlayer(this);
    }

    public void BattleEnd(int id)
    {
        InBattle = false;
        health.inBlock = false;
        canAttack = false;
        inCast = false;
        playerHealth.BlockDown();
        magicCircle.SetActive(false);

        ClearDelegates();
        Destroy(petGO);

        spellTreeManager.Won();
        aliveMonstersManager.Won(id);
    }

    private void ClearDelegates()
    {
        if(castSpellDelegateEvent != null)
        {
            Delegate[] delegates = castSpellDelegateEvent.GetInvocationList();
            foreach (Delegate d in delegates)
            {
                castSpellDelegateEvent -= (CastSpellDelegate)d;
            }
        }
    }

    private void Update()
    {
        if (!InBattle)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rotation -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rotation += 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                movement += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movement -= 1;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad0) && !canAttack)
            {
                health.SetUpBlock();
            }
            if (Input.GetMouseButtonDown(1) && !inCast)
            {
                inCast = true;
                mousePosition = Input.mousePosition;
                magicCircle.SetActive(true);
            }
        }
    }

    public void Def()
    {
        health.SetUpBlock();
        magicCircle.SetActive(false);
        inCast = false;
    }

    public void CastSpell(GameObject gameObject)
    {
        //battleManager.PlayerAttack();
        Vector3 position = Camera.main.ScreenToWorldPoint(mousePosition);
        GameObject spell = Instantiate(gameObject, position + transform.forward, Camera.main.transform.rotation);
        //spell.transform.forward = transform.forward;
        //TODO: Not in children?
        PlayerSpell spellAttack = spell.GetComponentInChildren<PlayerSpell>();

        SetUpCoolDown(spellAttack.cd);

        /*if(spellAttack != null)
        {
            spellAttack.dmg = ;
            spellAttack.attackType = ;
        }
        else
        {
            Debug.LogWarning("There is no SpellAttack!");
        }*/

        if (!canAttack)
        {
            health.SetUpBlock();
        }

        magicCircle.SetActive(false);
    }

    private void SetUpCoolDown(float cd)
    {
        castSpellDelegateEvent?.Invoke();

        if (!resetedCd)
        {
            coolDownRectTransform.sizeDelta = new Vector2(multiplyCD * cd, coolDownRectTransform.sizeDelta.y);
            coolDown.value = 1;
            coolDown.gameObject.SetActive(true);
            StartCoroutine(Countdown(coolDown));
        }
        else
        {
            inCast = false;
            resetedCd = false;
        }
    }

    private IEnumerator Countdown(Slider slider)
    {
        float duration = coolDownRectTransform.sizeDelta.x / 100;
        float normalizedTime = 0;
        while (normalizedTime <= 1f && !resetedCd)
        {
            normalizedTime += Time.deltaTime / duration;
            slider.value -= Time.deltaTime / duration;
            yield return null;
        }

        coolDown.gameObject.SetActive(false);
        inCast = false;
        resetedCd = false;
    }

    public void ResetCooldown()
    {
        resetedCd = true;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * movement * Time.fixedDeltaTime * movementSpeed);
        movement = 0;

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotation, 0) * Time.deltaTime * rotationSpeed);
        rb.MoveRotation(rb.rotation * deltaRotation);
        rotation = 0;
    }

    public override void Die()
    {
        //Necessary?
        magicCircle.SetActive(false);
        battleManager.PlayerDied();
    }

    public override void StartTurn()
    {
        AttackTurn();
    }

    public void DefTurn()
    {
        grid.color = DefenseGridColor;
        canAttack = false;
        magicCircle.SetActive(false);
        inCast = false;
    }

    public void AttackTurn()
    {
        grid.color = AttackGridColor;
        canAttack = true;
        magicCircle.SetActive(false);
        inCast = false;
    }
}
