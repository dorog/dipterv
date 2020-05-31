using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatternFormulaTest : MonoBehaviour
{
    public Vector2[] points;
    public Vector2[] guesses;

    // Start is called before the first frame update
    void Start()
    {
        PatternFormula patternFormula = new PatternFormula(points.ToList(), null, 2);
        for(int i = 0; i < guesses.Length; i++)
        {
            patternFormula.Guess(guesses[i]);
        }

        Debug.Log(patternFormula.GetResult());
    }
}
