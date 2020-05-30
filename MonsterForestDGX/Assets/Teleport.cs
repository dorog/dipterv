using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject ui;
    public Player player;

    public void HideUI()
    {
        player.InMenu = false;
        ui.SetActive(false);
    }

    public void TeleportPlayer(GameObject port)
    {
        player.transform.position = port.transform.position;
        player.transform.rotation = port.transform.rotation;
        HideUI();
    }
}
