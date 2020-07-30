using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PatternFormulaTests
    {

        private static List<Vector2> points = new List<Vector2>() { new Vector2(0, 0), new Vector2(0, 100) };
        private static PatternFormula patternFormula = null;

        [SetUp]
        public void SetUp()
        {
            patternFormula = new PatternFormula(points, null)
            {
                level = 1
            };
        }

        [TearDown]
        public void TearDown()
        {
            patternFormula = null;
        }

        // A Test behaves as an ordinary method
        [Test]
        public void OneCorrectGuessTest()
        {
            patternFormula.Guess(new Vector2(0, 1));
            Assert.IsTrue(patternFormula.GetResult() == 0.1f);
        }

        [Test]
        public void MultiplyCorrectGuessTest()
        {
            patternFormula.Guess(new Vector2(0, 1));
            patternFormula.Guess(new Vector2(0, 12));
            patternFormula.Guess(new Vector2(0, 15));
            patternFormula.Guess(new Vector2(0, 24));

            Assert.IsTrue(patternFormula.GetResult() == 0.3f);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        /*[UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }*/
    }
}
