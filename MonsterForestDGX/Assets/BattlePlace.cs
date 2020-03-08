using UnityEngine;

public class BattlePlace : MonoBehaviour
{
    public BattleManager battleManager;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = transform.position;
            other.gameObject.transform.rotation = transform.rotation;
            battleManager.Battle();

            gameObject.SetActive(false);
        }
    }
}
