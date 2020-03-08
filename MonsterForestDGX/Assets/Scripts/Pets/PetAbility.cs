using System;
using UnityEngine;

[Serializable]
public abstract class PetAbility : MonoBehaviour
{
    public abstract void Init(Player player);
}
