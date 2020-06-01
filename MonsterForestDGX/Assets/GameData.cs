﻿using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public int[] basePatternSpellLevels;
    public bool[] aliveMonsters;
    public bool[] availablePets;
    public float exp;
    public GameObject lastLocation = null;

    public GameData() { }

    public GameData(GameConfig gameConfig, float playerExp)
    {
        aliveMonsters = CreateArrayWithDefaultValue(gameConfig.aliveMonsters, true);
        basePatternSpellLevels = CreateArrayWithDefaultValue(gameConfig.baseSpells.Length, 1);
        availablePets = CreateArrayWithDefaultValue(gameConfig.pets.Length, false);
        exp = playerExp;
    }

    public T[] CreateArrayWithDefaultValue<T>(int count, T value)
    {
        if (count == 0)
        {
            Debug.LogError(nameof(CreateArrayWithDefaultValue) + " Error!");
            return new T[1];
        }
        else
        {
            T[] array = new T[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = value;
            }

            return array;
        }
    }
}
