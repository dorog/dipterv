using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public readonly string fileName = "gameData.json";
    private static string deviceFileLocation;

    public GameConfig gameConfig;

    private static DataManager instance = null;

    private GameData gameData = null;

    public delegate void ExpChangedEvent(float exp);
    public ExpChangedEvent expChangedEvent;

    public delegate void SpellLevelChangedEvent(int id);
    public SpellLevelChangedEvent spellLevelChangedEvent;

    public static DataManager GetInstance()
    {
        return instance;
    }

    public void Awake()
    {
        deviceFileLocation = Application.persistentDataPath + "/" + fileName;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one " + nameof(DataManager) + "!");
        }

        GetGameData();

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        expChangedEvent(gameData.exp);
    }

    private void GetGameData()
    {
        if (File.Exists(deviceFileLocation))
        {
            //TODO: Check attack and defense data count?
            Read();
        }
        else
        {
            Create();
        }
    }

    private void Read()
    {
        gameData = JsonUtility.FromJson<GameData>(File.ReadAllText(deviceFileLocation));
    }

    private void Create()
    {
        gameData = new GameData(gameConfig);

        Save(gameData);
    }


    public void Save<T>(T data)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(deviceFileLocation, jsonData);
    }

    public void SaveMonsterDeath(int id)
    {
        gameData.aliveMonsters[id] = false;

        Save(gameData);
    }

    public void Won(List<ISpellPattern> spellPatterns, float exp)
    {
        gameData.exp = exp;
        for (int i = 0; i < spellPatterns.Count; i++)
        {
            gameData.basePatternSpellLevels[i] = spellPatterns[i].GetLevelValue();
        }

        Save(gameData);

        expChangedEvent(exp);
    }

    public Pet[] GetAvailablePets()
    {
        List<Pet> pets = new List<Pet>();
        for(int i = 0; i < gameConfig.pets.Length; i++)
        {
            if (gameData.availablePets[i])
            {
                pets.Add(gameConfig.pets[i]);
            }
        }

        return pets.ToArray();
    }

    public bool[] GetAllPetsAvailability()
    {
        return gameData.availablePets;
    }

    public GameObject GetLastLocation()
    {
        return gameData.lastLocation;
    }

    public void SavePortLocation(GameObject lastLocation)
    {
        gameData.lastLocation = lastLocation;

        Save(gameData);
    }

    public void SaveTeleportUnlock(int id)
    {
        gameData.teleports[id] = true;

        Save(gameData);
    }

    public bool[] GetAliveMonsters()
    {
        return gameData.aliveMonsters;
    }

    public bool[] GetTeleportsState()
    {
        return gameData.teleports;
    }

    public List<BasePatternSpell> GetBasePatterns()
    {
        return gameConfig.baseSpells.ToList();
    }

    public List<int> GetBasePatternLevels()
    {
        return gameData.basePatternSpellLevels.ToList();
    }

    public void LevelUpSpell(int id, int exp)
    {
        gameData.exp -= exp;
        gameData.basePatternSpellLevels[id]++;

        Save(gameData);

        expChangedEvent(gameData.exp);
        spellLevelChangedEvent(id);
    }

    public void CollectPet(int id)
    {
        gameData.availablePets[id] = true;
        Save(gameData);
    }
}
