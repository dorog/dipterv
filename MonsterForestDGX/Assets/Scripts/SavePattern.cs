using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SavePattern : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Text patternName;
    public static string saveLocation = @"\Assets\Patterns";
    private string location = Directory.GetCurrentDirectory() + saveLocation;

    public void Save()
    {
        int count = lineRenderer.positionCount;
        Debug.Log(count);
        SpellPatternData spellPatternData = new SpellPatternData
        {
            x = new float[count],
            y = new float[count],
            z = new float[count]
        };

        for(int i = 0; i < count; i++)
        {
            Vector3 position = lineRenderer.GetPosition(i);
            spellPatternData.x[i] = position.x;
            spellPatternData.y[i] = position.y;
            spellPatternData.z[i] = position.z;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(location + @"\" + patternName.text + ".txt", FileMode.Create);

        bf.Serialize(file, spellPatternData);
        file.Close();
    }
}
