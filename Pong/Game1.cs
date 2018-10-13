using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Paddle playerPaddle;
        private Paddle opponentPaddle;
        private Ball ball;
        private GameObjects gameObjects;
        private SpriteFont font;
        private GameScore score;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Score"); // Use the name of your sprite font file here instead of 'Score'.

            var gameBoundaries = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            var paddleContent = Content.Load<Texture2D>("paddle");
            var opponentLocation = new Vector2(gameBoundaries.Width - paddleContent.Width, 0);

            score = new GameScore(font, gameBoundaries);
            playerPaddle = new Paddle(paddleContent, Vector2.Zero, gameBoundaries, PlayerType.Human);
            opponentPaddle = new Paddle(paddleContent, opponentLocation, gameBoundaries, PlayerType.Computer);

            ball = new Ball(Content.Load<Texture2D>("ball"), Vector2.Zero, gameBoundaries);
            ball.AttachTo(playerPaddle);

            gameObjects = new GameObjects
            {
                Ball = ball,
                PlayerPaddle = playerPaddle,
                ComputerPaddle = opponentPaddle,
                Score = score
            };
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (ball.PastOpponent)
            {
                gameObjects.Score.PlayerScore += 1;
                ball.AttachTo(playerPaddle);
            }
            else if (ball.PastPlayer)
            {
                gameObjects.Score.OpponentScore += 1;
                ball.AttachTo(playerPaddle);
            }

            ball.Update(gameTime, gameObjects);
            playerPaddle.Update(gameTime, gameObjects);
            opponentPaddle.Update(gameTime, gameObjects);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            playerPaddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            opponentPaddle.Draw(spriteBatch);

            //gameObjects.Score.Draw(spriteBatch);

            spriteBatch.DrawString(font, $"Score {gameObjects.Score.PlayerScore} - {gameObjects.Score.OpponentScore}", new Vector2(100, 100), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
