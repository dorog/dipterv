using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SavePattern : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Text patternName;
    public static string saveLocation = @"\Assets\Patterns";
    private string location = Directory.GetCurrentDirectory() + saveLocation;
    private readonly float scale = 100f;

    public void Save()
    {
        int count = lineRenderer.positionCount;
        SpellPatternData spellPatternData = new SpellPatternData
        {
            x = new float[count],
            y = new float[count],
        };

        for(int i = 0; i < count; i++)
        {
            Vector3 position = lineRenderer.GetPosition(i);
            spellPatternData.x[i] = position.x * scale;
            spellPatternData.y[i] = position.y * scale;
        }

        string jsonData = JsonUtility.ToJson(spellPatternData, true);
        File.WriteAllText(location + @"\" + patternName.text + ".json", jsonData);

        lineRenderer.positionCount = 0;
        patternName.text = "";
    }
}
