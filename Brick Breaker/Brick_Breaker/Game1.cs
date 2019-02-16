using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Brick_Breaker
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Racket racket;
        Block[,] blocks;
        Ball ball;
        SpriteFont font;
        int score, moneBlocks;
        bool isGameOver, isWin, canShoot = true;
        List<Bomb> bombs = new List<Bomb>();

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
            Window.Title = "Brick Breaker [Made by Idan Bachar]";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            racket = new Racket(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 100, graphics.PreferredBackBufferHeight - 50, 100, 15), Content);
            blocks = new Block[5, 16];
            for (int y = 0; y < 5; y++) {
                for(int x = 0; x < 16; x++) {
                    blocks[y, x] = new Block(new Rectangle(x * 50, y * 50, 50, 50), Content);
                }
            }
            ball = new Ball(new Rectangle(graphics.PreferredBackBufferWidth / 2 - 25, graphics.PreferredBackBufferHeight / 2 - 25, 25, 25), -5, -6, Content, graphics);

            font = Content.Load<SpriteFont>("fonts/Font");


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            if (!isGameOver && !isWin) {

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    if (racket.GetPosition().Right < graphics.PreferredBackBufferWidth)
                        racket.MoveRight();
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    if (racket.GetPosition().Left > 0)
                        racket.MoveLeft();

                if (Keyboard.GetState().IsKeyDown(Keys.Space) && canShoot) {
                    canShoot = false;
                    racket.Bullets.Add(new Bullet(new Rectangle(racket.GetPosition().Center.X, racket.GetPosition().Top, 5, 15), Content));
                }

                if (Keyboard.GetState().IsKeyUp(Keys.Space))
                    canShoot = true;

                for (int i = 0; i < racket.Bullets.Count; i++) {
                    racket.Bullets[i].Move();
                    for (int j = 0; j < bombs.Count; j++) {
                        if (racket.Bullets[i].GetPosition().Intersects(bombs[j].GetPosition()) && bombs[j].Visible) {
                            racket.Bullets.RemoveAt(i);
                            bombs.RemoveAt(j);
                        }
                    }
                }
                ball.Move(racket, blocks, this);

                if (moneBlocks == (blocks.GetLength(0) * blocks.GetLength(1)))
                    isWin = true;

                foreach (Bomb b in bombs)
                    b.Move();

                racket.Check(this);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            racket.Draw(spriteBatch);

            foreach (Block b in blocks)
                b.Draw(spriteBatch);

            foreach (Bomb b in bombs)
                b.Draw(spriteBatch);

            foreach (Bullet b in racket.Bullets)
                b.Draw(spriteBatch);

            ball.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Your score: " + score, new Vector2(0, graphics.PreferredBackBufferHeight - 25), Color.White);

            if (isGameOver)
                spriteBatch.DrawString(font, "Game Over!", new Vector2(graphics.PreferredBackBufferWidth / 2 - 50, graphics.PreferredBackBufferHeight / 2), Color.White);
            else if(isWin)
                spriteBatch.DrawString(font, "You win! with " + score + " score!", new Vector2(graphics.PreferredBackBufferWidth / 2 - 50, graphics.PreferredBackBufferHeight / 2), Color.White);

            spriteBatch.DrawString(font, "Made by Idan Bachar.", new Vector2(graphics.PreferredBackBufferWidth - 200, graphics.PreferredBackBufferHeight - 25), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public int Score { get { return this.score; } set { this.score = value; } }
        public int MoneBlocks { get { return this.moneBlocks; } set { this.moneBlocks = value; } }
        public bool IsGameOver { get { return this.isGameOver; } set { this.isGameOver = value; } }
        public List<Bomb> Bombs { get { return this.bombs; } }
    }
}
