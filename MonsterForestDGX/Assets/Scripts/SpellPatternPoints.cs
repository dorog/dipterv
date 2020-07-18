using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "new SpellPatternPoints", menuName = "SpellPatternPoints")]
public class SpellPatternPoints : ScriptableObject
{
    public ElementType attackType = ElementType.TrueDamage;
    public string patternName;

    //Make config for it
    private static string saveLocation = @"\Assets\Patterns";
    private string location = Directory.GetCurrentDirectory() + saveLocation;

    public List<SpellPatternPoint> GetPoints()
    {
        List<SpellPatternPoint> spellPatternPoints = new List<SpellPatternPoint>();

        SpellPatternData spellPatternData = JsonUtility.FromJson<SpellPatternData>(File.ReadAllText(location + @"\" + patternName + ".json"));

        for (int i = 0; i < spellPatternData.x.Length; i++)
        {
            spellPatternPoints.Add(new SpellPatternPoint(i, new Vector3(spellPatternData.x[i], spellPatternData.y[i])));
        }

        return spellPatternPoints;
    }
}
