using UnityEngine;
using UnityEngine.UI;

public class PetManager : SingletonClass<PetManager>
{
    private Image selectedImage = null;
    private readonly int notSelectedPetValue = -1;
    public int selectedPet;
    private Pet[] pets;

    public PetTab petTab;

    public Sprite selectedImageSprite;
    private Sprite originalSprite;

    private static readonly string lastPetKey = "lastPetKey";

    private void Awake()
    {
        Init(this);
    }

    private void Start()
    {
        pets = DataManager.GetInstance().GetAvailablePets();
        if(pets == null)
        {
            Debug.LogError("PetManager: Null");
        }
        else
        {
            selectedPet = PlayerPrefs.GetInt(lastPetKey, notSelectedPetValue);
            if(selectedPet >= pets.Length)
            {
                selectedPet = notSelectedPetValue;
                petTab.SetUpUI(pets);
            }
            else
            {
                petTab.SetUpUI(pets, selectedPet);
            }
        }
    }

    public GameObject GetPet()
    {
        if(selectedPet == notSelectedPetValue)
        {
            return null;
        }
        return pets[selectedPet].gameObject;
    }

    public void Select(Image image, int number)
    {
        if(selectedImage != null)
        {
            selectedImage.sprite = originalSprite;
        }
        selectedImage = image;
        originalSprite = selectedImage.sprite;
        selectedImage.sprite = selectedImageSprite;
        selectedPet = number;

        PlayerPrefs.SetInt(lastPetKey, selectedPet);
    }

    public Pet[] GetPets()
    {
        return pets;
    }
}
