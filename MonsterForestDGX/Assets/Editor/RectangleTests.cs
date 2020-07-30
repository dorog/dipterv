using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class RectangleTests
    {
        [Test]
        public void OneCorrectGuessTest()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(0, 100), 2);
            rectangle.Guess(new Vector2(0, 1), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 1);
        }

        [Test]
        public void MultiplyCorrectGuessTest()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(0, 100), 2);
            rectangle.Guess(new Vector2(0, 1), -1);
            rectangle.Guess(new Vector2(0, 11), -1);
            rectangle.Guess(new Vector2(0, 13), -1);
            rectangle.Guess(new Vector2(0, 22), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 3);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        /*[UnityTest]
        public IEnumerator RectangleTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }*/
    }
}
