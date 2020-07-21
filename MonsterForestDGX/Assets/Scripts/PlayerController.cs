using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player player;

    public Rigidbody rb;

    private float rotation = 0;
    public float rotationSpeed = 30;

    private float movement = 0;
    private float sideMovement = 0;
    public float movementSpeed = 10;

    void Update()
    {
        if (player.CanMove())
        {
            Move();

            Click();
        }
    }

    private void FixedUpdate()
    {
        Vector3 forwardDirection = transform.forward * movement;
        Vector3 sideDirection = transform.right * sideMovement;
        Vector3 direction = (forwardDirection + sideDirection) * Time.fixedDeltaTime * movementSpeed;
        rb.MovePosition(transform.position + direction);
        movement = 0;
        sideMovement = 0;

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotation, 0) * Time.deltaTime * rotationSpeed);
        rb.MoveRotation(rb.rotation * deltaRotation);
        rotation = 0;
    }

    private void Click()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                UiShower teleportUI = hit.collider.gameObject.GetComponent<UiShower>();
                if (teleportUI)
                {
                    player.MenuState(true);
                    teleportUI.ShowUI();
                }
            }
        }*/
    }

    private void Move()
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
        if (Input.GetKey(KeyCode.E))
        {
            sideMovement += 1;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            sideMovement -= 1;
        }
    }
}
