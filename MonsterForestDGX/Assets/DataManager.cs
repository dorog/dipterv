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
        PlayerExperience playerExperience = PlayerExperience.GetInstance();
        playerExperience.SetExp(gameData.exp);
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

    public void Won(List<ISpellPattern> spellPatterns)
    {
        for (int i = 0; i < spellPatterns.Count; i++)
        {
            gameData.basePatternSpellLevels[i] = spellPatterns[i].GetLevelValue();
        }

        Save(gameData);
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

    public GameObject GetLastLocation()
    {
        return gameData.lastLocation;
    }

    public void SavePortLocation(GameObject lastLocation)
    {
        gameData.lastLocation = lastLocation;

        Save(gameData);
    }

    public bool[] GetAliveMonsters()
    {
        return gameData.aliveMonsters;
    }

    public List<BasePatternSpell> GetBasePatterns()
    {
        return gameConfig.baseSpells.ToList();
    }

    public List<int> GetBasePatternLevels()
    {
        return gameData.basePatternSpellLevels.ToList();
    }
}
