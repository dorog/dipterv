using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public Transform menu;
    public GameObject ui;
    public Player player;

    private EnableShopUI esUI = null;

    public void HideUI()
    {
        if(esUI != null)
        {
            esUI.ClosedUI();
        }

        player.MenuState(false);
        ui.SetActive(false);
    }

    public void ShowUI(Vector3 position, Quaternion rotation, EnableShopUI enableShopUI)
    {
        esUI = enableShopUI;

        player.MenuState(true);

        menu.position = position;
        menu.rotation = rotation;

        ui.SetActive(true);
    }
}
