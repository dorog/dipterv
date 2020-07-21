using UnityEngine;

//TODO: Merge this and EnableShopUI
public class UiShower : MonoBehaviour
{
    public MenuUI menuUI;
    public Vector3 offset;

    public void ShowUI(EnableShopUI enableShopUI)
    {
        menuUI.ShowUI(transform.position + offset, transform.rotation, enableShopUI);
    }
}
