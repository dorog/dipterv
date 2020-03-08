using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "new SpellPatternPoints", menuName = "SpellPatternPoints")]
public class SpellPatternPoints : ScriptableObject
{
    [HideInInspector]
    public bool isAttack = true;
    [HideInInspector]
    public int treeLine = 0;

    public GameObject Spell;
    public ElementType attackType = ElementType.TrueDamage;
    public string patternName;
    private List<Vector3> Points = null;

    //Make config for it
    private static string saveLocation = @"\Assets\Patterns";
    private string location = Directory.GetCurrentDirectory() + saveLocation;

    public List<Vector3> GetPoints()
    {
        ReadData();

        /*if (Points == null)
        {
            Debug.Log("Read");
            ReadData();
        }*/

        return Points;
    }

    private void ReadData()
    {
        Points = new List<Vector3>();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(location + @"\" + patternName + ".txt", FileMode.Open);
        SpellPatternData spellPatternData = (SpellPatternData)bf.Deserialize(file);
        file.Close();

        for(int i = 0; i < spellPatternData.x.Length; i++)
        {
            Points.Add(new Vector3(spellPatternData.x[i], spellPatternData.y[i], spellPatternData.z[i]));
        }
    }
}
