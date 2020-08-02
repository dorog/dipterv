using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class RectangleTests
    {
        [Test]
        public void OneMissGuess()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(0, 100), 2);
            rectangle.Guess(new Vector2(0, -5), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 0);
        }

        [Test]
        public void MultiplyMissGuess()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(0, 100), 2);

            rectangle.Guess(new Vector2(0, -5), -1);
            rectangle.Guess(new Vector2(3, -3), -1);
            rectangle.Guess(new Vector2(3, 11), -1);
            rectangle.Guess(new Vector2(4, 105), -1);
            rectangle.Guess(new Vector2(-3.5f, 102.5f), -1);
            rectangle.Guess(new Vector2(-2.1f, 90), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 0);
        }

        [Test]
        public void OneCorrectGuess()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(0, 100), 2);
            rectangle.Guess(new Vector2(0, 1), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 1);
        }

        [Test]
        public void DuplicatedMultiplyCorrectGuess()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(0, 100), 2);
            rectangle.Guess(new Vector2(0, 1), -1);
            rectangle.Guess(new Vector2(0, 11), -1);
            rectangle.Guess(new Vector2(0, 13), -1);
            rectangle.Guess(new Vector2(0, 22), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 3);
        }

        [Test]
        public void IncludeToInclude()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(0, 100), 2);

            rectangle.Guess(new Vector2(0, 1), -1);
            rectangle.Guess(new Vector2(0, 95), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 10);
        }

        [Test]
        public void NotToNot()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(0, 100), 2);

            rectangle.Guess(new Vector2(0, -5), -1);
            rectangle.Guess(new Vector2(0, 120), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 10);
        }

        [Test]
        public void NotToInclude()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(0, 100), 2);

            rectangle.Guess(new Vector2(0, -5), -1);
            rectangle.Guess(new Vector2(0, 95), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 10);
        }

        [Test]
        public void IncludeToNot()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(0, 100), 2);

            rectangle.Guess(new Vector2(0, 5), -1);
            rectangle.Guess(new Vector2(0, 120), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 10);
        }

        [Test]
        public void ThreeOfFour()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(0, 100), 2);

            rectangle.Guess(new Vector2(2, 1), -1);
            rectangle.Guess(new Vector2(-2, 100), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 10);
        }

        [Test]
        public void FullCross()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(0, 100), 2);

            rectangle.Guess(new Vector2(2, 0), -1);
            rectangle.Guess(new Vector2(-2, 100), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 10);
        }

        [Test]
        public void FirstQuarter()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(100, 100), 2);

            rectangle.Guess(new Vector2(40, 40), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 1);
        }

        [Test]
        public void SecondQuarter()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(-100, 100), 2);

            rectangle.Guess(new Vector2(-40, 40), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 1);
        }

        [Test]
        public void ThirdQuarter()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(-100, -100), 2);

            rectangle.Guess(new Vector2(-40, -40), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 1);
        }

        [Test]
        public void FourthQuarter()
        {
            Rectangle rectangle = new Rectangle(0, 10, 10, new Vector2(0, 0), new Vector2(100, -100), 2);

            rectangle.Guess(new Vector2(40, -40), -1);

            Assert.IsTrue(rectangle.GetHitNumber() == 1);
        }
    }
}
