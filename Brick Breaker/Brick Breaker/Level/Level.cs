using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Brick_Breaker {
    public class Level {

        public Block[,] Blocks; //Level's blocks
        private int Rows; //Block's rows
        private int Cols; //Block's cols
        public bool IsGameOver; //Game over indication
        private int Score; //Score
        private int Chances; //Chances before lost

        private SpriteFont Font; //Font
        private string FinishMessage; //Finish game message

        private Random Random; //Random

        //Blocks colors:
        private Color[] Colors = {
            Color.DarkRed,
            Color.DarkGreen,
            Color.DarkCyan,
            Color.DarkBlue,
            Color.DarkMagenta,
            Color.DarkKhaki,
            Color.White,
            Color.DarkViolet,
            Color.DarkGreen
        };

        /// <summary>
        /// Initializes level
        /// </summary>
        public Level() {
            Random = new Random();
            Rows = 4;
            Cols = 16;
            Blocks = new Block[Rows, Cols];
            IsGameOver = false;
            Score = 0;
            Chances = 3;
            FinishMessage = string.Empty;
            LoadContent();
        }

        /// <summary>
        /// Add score
        /// </summary>
        public void AddScore() {
            Score++;
        }

        /// <summary>
        /// Win
        /// </summary>
        public void Win() {
            IsGameOver = true;
            FinishMessage = "You won!! total score is " + Score;
        }

        /// <summary>
        /// Lost
        /// </summary>
        public void Lost() {
            IsGameOver = true;
            FinishMessage = "You lost :( total score is " + Score;
        }

        /// <summary>
        /// Decrease life
        /// </summary>
        public void DecreaseChance() {
            Chances--;
        }

        /// <summary>
        /// Initializes blocks
        /// </summary>
        private void InitializeBlocks() {

            Color blockColor = Colors[Random.Next(Colors.Length)];

            for (int y = 0; y < Blocks.GetLength(0); y++)
                for (int x = 0; x < Blocks.GetLength(1); x++)
                    Blocks[y, x] = new Block(x * 50, y * 50, 50, 50, blockColor);
        }

        /// <summary>
        /// Update blocks
        /// </summary>
        public void Update() {

            foreach (Block block in Blocks)
                block.Update();

            if (Chances == 0)
                Lost();
        }

        /// <summary>
        /// Load content
        /// </summary>
        private void LoadContent() {

            Font = BrickBreaker.ContentManager.Load<SpriteFont>("fonts/Font");

            InitializeBlocks();
        }

        /// <summary>
        /// Draw level
        /// </summary>
        public void Draw() {

            //Draw blocks:
            foreach (Block block in Blocks)
                block.Draw();

            //Draw copyright:
            BrickBreaker.SpriteBatch.DrawString(Font, "Made by Idan Bachar.", new Vector2(BrickBreaker.Graphics.PreferredBackBufferWidth / 2 + 200,
                                                                                          BrickBreaker.Graphics.PreferredBackBufferHeight - 35), Color.White);

            //Draw score/chances:
            BrickBreaker.SpriteBatch.DrawString(Font, "Score: " + Score + "\n" + "Chances: " + Chances + "/3", new Vector2(10, 
                                                                                     BrickBreaker.Graphics.PreferredBackBufferHeight - 50), Color.White);


            //Draw game over message:
            if (IsGameOver)
                BrickBreaker.SpriteBatch.DrawString(Font, FinishMessage, new Vector2(BrickBreaker.Graphics.PreferredBackBufferWidth / 2 - 100, BrickBreaker.Graphics.PreferredBackBufferHeight / 2 - 100), Color.White);
        }
    }
}
