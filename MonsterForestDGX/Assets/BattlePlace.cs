using UnityEngine;

public class BattlePlace : MonoBehaviour
{
    public BattleManager battleManager;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            battleManager.Battle();
            other.gameObject.transform.position = transform.position;
            other.gameObject.transform.rotation = transform.rotation;

            gameObject.SetActive(false);
        }
    }
}
