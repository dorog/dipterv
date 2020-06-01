using UnityEngine;

public class Teleport : MenuUI
{
    public void TeleportPlayer(GameObject port)
    {
        player.transform.position = port.transform.position;
        player.transform.rotation = port.transform.rotation;
        HideUI();
    }
}
