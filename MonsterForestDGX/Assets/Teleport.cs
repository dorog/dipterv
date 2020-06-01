using UnityEngine;

public class Teleport : MenuUI
{
    public GameObject lastLocation;
    private DataManager dataManager;

    public void Start()
    {
        dataManager = DataManager.GetInstance();
        GameObject last = dataManager.GetLastLocation();
        if(last != null)
        {
            TeleportPlayer(last);
        }
    }

    public void TeleportPlayer(GameObject port)
    {
        player.transform.position = port.transform.position;
        player.transform.rotation = port.transform.rotation;
        lastLocation = port;

        dataManager.SavePortLocation(lastLocation);

        HideUI();
    }

    public void TeleportToLastPosition()
    {
        TeleportPlayer(lastLocation);
    }
}
