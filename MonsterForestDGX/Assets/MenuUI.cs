using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public Transform canvas;
    public GameObject ui;
    public Player player;

    public void HideUI()
    {
        player.MenuState(false);
        ui.SetActive(false);
    }

    public void ShowUI(Vector3 position, Quaternion rotation)
    {
        player.MenuState(true);

        canvas.position = position;
        canvas.rotation = rotation;

        ui.SetActive(true);
    }
}
