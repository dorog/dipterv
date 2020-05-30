using UnityEngine;

public class TeleportUI : MonoBehaviour
{
    public Player player;
    public GameObject portUI;

    public void ShowUI()
    {
        portUI.SetActive(true);
    }
}
