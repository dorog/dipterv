using UnityEngine;

public class Teleport : MenuUI
{
    public GameObject lastLocation;
    private DataManager dataManager;

    public GameObject teleportsParent;

    private bool[] ports;

    private int lastTeleportUI = -1;

    public GameObject defaultLocation;

    public void Start()
    {
        dataManager = DataManager.GetInstance();
        GameObject last = dataManager.GetLastLocation();
        ports = dataManager.GetTeleportsState();

        //TODO: Refactor, id instead of gameobject
        /*if (last != null)
        {
            TeleportPlayer(last);
        }*/

        SetupTeleportUI();
    }

    public void TeleportPlayer(GameObject port)
    {
        player.transform.position = port.transform.position;
        player.transform.rotation = port.transform.rotation;

        HideUI();
    }

    private void SetLastLocation(GameObject port)
    {
        lastLocation = port;
        dataManager.SavePortLocation(port);
    }

    public void ReachedTerritory(int id, GameObject port)
    {
        SetLocation(id, port);

        SetPortUI(id);
    }

    private void SetLocation(int id, GameObject port)
    {
        if (!ports[id])
        {
            ports[id] = true;
            dataManager.SaveTeleportUnlock(id);

            teleportsParent.transform.GetChild(id).gameObject.SetActive(true);
        }

        SetLastLocation(port);
    }

    private void SetPortUI(int id)
    {
        if (lastTeleportUI != -1 && ports[lastTeleportUI])
        {
            teleportsParent.transform.GetChild(lastTeleportUI).gameObject.SetActive(true);
        }
        teleportsParent.transform.GetChild(id).gameObject.SetActive(false);
        lastTeleportUI = id;
    }

    public void TeleportToLastPosition()
    {
        TeleportPlayer(lastLocation);
    }

    private void SetupTeleportUI()
    {
        for (int i = 0; i < ports.Length; i++)
        {
            teleportsParent.transform.GetChild(i).gameObject.SetActive(ports[i]);
        }
    }

    public void TeleportToDefaultLocation()
    {
        TeleportPlayer(defaultLocation);
    }
}
