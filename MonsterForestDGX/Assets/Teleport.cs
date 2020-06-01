using UnityEngine;

public class Teleport : MenuUI
{
    public GameObject lastLocation;

    public void TeleportPlayer(GameObject port)
    {
        player.transform.position = port.transform.position;
        player.transform.rotation = port.transform.rotation;
        lastLocation = port;
        HideUI();
    }

    public void TeleportToLastPosition()
    {
        TeleportPlayer(lastLocation);
    }
}
