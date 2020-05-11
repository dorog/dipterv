using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public readonly string fileName = "gameData.txt";
    private static string deviceFileLocation;

    public GameData gameData;
    public int attackSpells;
    public int defenseSpells;
    public int aliveMonstersNumber;

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
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(deviceFileLocation, FileMode.Open);
        gameData = (GameData)bf.Deserialize(file);
        file.Close();
    }

    private void Create()
    {
        gameData = new GameData
        {
            SpellTreeLineData = new SpellTreeLineData(attackSpells, defenseSpells),
            AliveMonsters = new AliveMonsters(aliveMonstersNumber)
        };

        Save(gameData);
    }


    public void Save<T>(T data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileForSave = File.Create(deviceFileLocation);
        bf.Serialize(fileForSave, data);
        fileForSave.Close();
    }

    public void SaveMonsterDeath(int id)
    {
        gameData.AliveMonsters.alive[id] = false;

        Save(gameData);
    }

    public void Won(SpellTreeLineData spellTreeLineData)
    {
        gameData.SpellTreeLineData = spellTreeLineData;

        Save(gameData);
    }
}
