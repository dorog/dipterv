using UnityEngine;

public class Pet : MonoBehaviour
{
    public Resistant resistant;

    public PetAbility[] normalAbilities;
    public UpdatePetAbility[] updateAbilities;

    public void AddPlayer(Player player)
    {
        foreach(var petAbility in updateAbilities)
        {
            petAbility.Init(player);
        }
        foreach (var petAbility in normalAbilities)
        {
            petAbility.Init(player);
        }
    }

    private void Update()
    {
        foreach (var petAbility in updateAbilities)
        {
            petAbility.UpdateEffect();
        }
    }
}
