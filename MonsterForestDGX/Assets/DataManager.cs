using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public readonly string fileName = "gameData.json";
    private static string deviceFileLocation;

    public GameConfig gameConfig;

    private static DataManager instance = null;
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
        GameData gameData = JsonUtility.FromJson<GameData>(File.ReadAllText(deviceFileLocation));

        SharedData.GameConfig = gameConfig;

        for (int i = 0; i < gameData.BasePatternSpells.Length; i++)
        {
            SharedData.GameConfig.baseSpells[i].xp = gameData.BasePatternSpells[i].xp;
            SharedData.GameConfig.baseSpells[i].level = gameData.BasePatternSpells[i].level;
        }

        SharedData.GameConfig.aliveMonsters = gameData.AliveMonsters.alive;
    }

    private void Create()
    {
        GameData gameData = new GameData(gameConfig);

        SharedData.GameConfig = gameConfig;
        for (int i = 0; i < gameData.BasePatternSpells.Length; i++)
        {
            SharedData.GameConfig.baseSpells[i].xp = gameData.BasePatternSpells[i].xp;
            SharedData.GameConfig.baseSpells[i].level = gameData.BasePatternSpells[i].level;
        }

        Save(gameData);
    }


    public void Save<T>(T data)
    {
        /*BinaryFormatter bf = new BinaryFormatter();
        FileStream fileForSave = File.Create(deviceFileLocation);
        bf.Serialize(fileForSave, data);
        fileForSave.Close();*/

        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(deviceFileLocation, jsonData);
    }

    public void SaveMonsterDeath(int id)
    {
        GameData gameData = new GameData(SharedData.GameConfig);
        gameData.AliveMonsters.alive[id] = false;

        Save(gameData);
    }

    public void Won()
    {
        Save(SharedData.GameConfig);
    }
}
