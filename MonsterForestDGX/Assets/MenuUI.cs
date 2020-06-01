using UnityEngine;

public abstract class MenuUI : MonoBehaviour
{
    public GameObject ui;
    public Player player;

    public void HideUI()
    {
        player.InMenu = false;
        ui.SetActive(false);
    }
}
