using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Test.Unit
{
    [TestClass]
    public class Paddle
    {
        private Pong.Paddle _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new Pong.Paddle(null, Vector2.Zero, new Rectangle());

            //new Texture2D(new GraphicsDevice(), 0, 0)), Vector2.Zero, new Rectangle()

        }

        [TestMethod]
        public void TestMethod1()
        {

            _sut.Update(new GameTime());

            Assert.IsTrue(true);
        }
    }
}
