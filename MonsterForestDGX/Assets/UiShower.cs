using UnityEngine;

public class UiShower : MonoBehaviour
{
    public MenuUI menuUI;
    public Vector3 offset;

    public void ShowUI()
    {
        /*portUI.transform.rotation = transform.rotation;
        portUI.transform.position = transform.position + offset;

        portUI.SetActive(true);*/
        menuUI.ShowUI(transform.position + offset, transform.rotation);
    }
}
