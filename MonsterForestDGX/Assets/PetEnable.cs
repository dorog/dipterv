using UnityEngine;

public class PetEnable : MonoBehaviour
{
    public int id = 0;
    private PetManager petManager;

    public GameObject pet;

    private bool collected = false;

    public GameObject availableSign;

    void Start()
    {
        bool[] pets = DataManager.GetInstance().GetAllPetsAvailability();
        petManager = PetManager.GetInstance();

        if (pets[id])
        {
            Collected();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!collected && other.gameObject.tag == "Player")
        {
            petManager.SetAvailablePet(this, id);
            availableSign.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!collected && other.gameObject.tag == "Player")
        {
            petManager.DisableAvailablePet();
            availableSign.SetActive(false);
        }
    }

    public void Collected()
    {
        pet.SetActive(false);
        collected = true;
        availableSign.SetActive(false);
    }
}
