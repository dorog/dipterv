using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player player;

    void Update()
    {
        if (player.CanMove())
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    TeleportUI teleportUI = hit.collider.gameObject.GetComponent<TeleportUI>();
                    if (teleportUI)
                    {
                        player.InMenu = true;
                        teleportUI.ShowUI();
                    }
                }
            }
        }
    }
}
