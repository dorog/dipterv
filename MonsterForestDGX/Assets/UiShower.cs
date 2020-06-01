using UnityEngine;

public class UiShower : MonoBehaviour
{
    public GameObject portUI;

    public void ShowUI()
    {
        portUI.SetActive(true);
    }
}
