using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject player;
    public GameObject portUI;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            portUI.SetActive(!portUI.activeSelf);
        }
    }

    public void Port(GameObject port)
    {
        player.transform.position = port.transform.position;
        player.transform.rotation = port.transform.rotation;
    }
}
