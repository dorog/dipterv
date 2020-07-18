using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableShopUI : MonoBehaviour
{
    public UiShower uiShower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            uiShower.ShowUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
