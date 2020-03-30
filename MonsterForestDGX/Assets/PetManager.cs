using UnityEngine;
using UnityEngine.UI;

public class PetManager : SingletonClass<PetManager>
{
    public Image[] images;
    public GameObject petTab;
    public Color selectedColor;
    public int selectedPet = 0;
    public Pet[] pets;

    private void Awake()
    {
        Init(this);
    }

    private void Start()
    {
        images[selectedPet].color = selectedColor;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            petTab.SetActive(!petTab.activeSelf);
        }
    }

    public GameObject GetPet()
    {
        return pets[selectedPet].gameObject;
    }

    public void Select(int number)
    {
        images[selectedPet].color = Color.white;
        images[number].color = selectedColor;
        selectedPet = number;
    }
}
