using UnityEngine;

public class PetTab : MonoBehaviour
{
    public GameObject root;
    public Transform parent;
    public PetUI petUI;

    public void SetUpUI(Pet[] pets, int defaultImage = -1)
    {
        if (pets == null || pets.Length == 0)
        {
            root.SetActive(false);
        }
        else
        {
            DeleteChilds();

            for (int i = 0; i < pets.Length; i++)
            {
                GameObject petUiGameObject = Instantiate(petUI.gameObject, parent);
                PetUI petUiIntance = petUiGameObject.GetComponent<PetUI>();
                petUiIntance.SetUI(i, pets[i]);
                if (i == defaultImage)
                {
                    petUiIntance.ChangePet();
                }
            }

            root.SetActive(true);
        }
    }

    private void DeleteChilds()
    {
        for(int i = parent.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
}
