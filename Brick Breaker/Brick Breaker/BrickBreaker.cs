using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Brick_Breaker
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class BrickBreaker : Game
    {
        public static GraphicsDeviceManager Graphics; //Game graphics
        public static SpriteBatch SpriteBatch; //Game spritebatch
        public static ContentManager ContentManager; //Game contentmanager
        private Player Player; //Player
        private Ball Ball; //Ball
        private Level Level; //Level

        public BrickBreaker()
        {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = 800;
            Graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
            Window.Title = "Brick Breaker";
        }
 
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            ContentManager = Content;
            Level = new Level();
            Player = new Player();
            Ball = new Ball();

            //Events:
            Ball.OnScore += Level.AddScore;
            Ball.OnMiss += Level.DecreaseChance;
            Ball.OnWin += Level.Win;
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //If game not yet over:
            if (!Level.IsGameOver) {
                //Update player:
                Player.Update();

                //Update ball:
                Ball.Update(Level.Blocks, Player.Racket);

                //Update level:
                Level.Update();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin();

            //Draw level:
            Level.Draw();

            //Draw player:
            Player.Draw();

            //Draw ball:
            Ball.Draw();

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
