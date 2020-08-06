using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_Breaker {
    public class Ball {

        private Texture2D Texture; //Ball's texture
        private Rectangle Rectangle; //Ball's rectangle
        private int Dx; //Ball's x direction
        private int Dy; //Ball's y direction

        //Add score event:
        public delegate void Score();
        public event Score OnScore;

        //Add miss event:
        public delegate void Miss();
        public event Miss OnMiss;

        //Add win event:
        public delegate void Win();
        public event Win OnWin;

        public Ball() {
            Rectangle = new Rectangle(BrickBreaker.Graphics.PreferredBackBufferWidth / 2 - 25,
                                      BrickBreaker.Graphics.PreferredBackBufferHeight / 2 - 25,
                                      25,
                                      25);

            Dx = -10;
            Dy = -10;
            LoadContent();
        }

        /// <summary>
        /// Load ball's content
        /// </summary>
        public void LoadContent() {
            Texture = BrickBreaker.ContentManager.Load<Texture2D>("images/ball/ball");
        }

        /// <summary>
        /// Respawn the ball
        /// </summary>
        private void Respawn() {
            Rectangle = new Rectangle(BrickBreaker.Graphics.PreferredBackBufferWidth / 2 - 25,
                                     BrickBreaker.Graphics.PreferredBackBufferHeight / 2 - 25,
                                     25,
                                     25);

            Dx *= -1;
            Dy *= -1;
        }

        /// <summary>
        /// Update ball
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="racket"></param>
        public void Update(Block [,] blocks, Racket racket) {

            Move();
            CheckScreenInteractions();
            CheckRacketIntersections(racket);
            CheckBlocksInteractions(blocks);
        }

        /// <summary>
        /// Move the ball
        /// </summary>
        private void Move() {
            Rectangle = new Rectangle(Rectangle.X += Dx, Rectangle.Y += Dy, Rectangle.Width, Rectangle.Height);
        }

        /// <summary>
        /// Checks for ball interactions with the game screen
        /// </summary>
        private void CheckScreenInteractions() {

            if (Rectangle.Right >= BrickBreaker.Graphics.PreferredBackBufferWidth)
                Rectangle = new Rectangle(Rectangle.X += (Dx *= -1), Rectangle.Y, Rectangle.Width, Rectangle.Height);

            if (Rectangle.Left <= 0)
                Rectangle = new Rectangle(Rectangle.X += (Dx *= -1), Rectangle.Y, Rectangle.Width, Rectangle.Height);

            if (Rectangle.Top <= 0) {
                Rectangle = new Rectangle(Rectangle.X, Rectangle.Y += (Dy *= -1), Rectangle.Width, Rectangle.Height);
                OnWin();
            }

            if (Rectangle.Top >= BrickBreaker.Graphics.PreferredBackBufferHeight) {
                OnMiss();
                Respawn();
            }
        }

        /// <summary>
        /// Check for ball interactions with the racket
        /// </summary>
        /// <param name="racket"></param>
        private void CheckRacketIntersections(Racket racket) {

            if (Rectangle.Bottom > racket.Rectangle.Top && Rectangle.Top < racket.Rectangle.Top &&
                Rectangle.Left + Rectangle.Width / 2 > racket.Rectangle.Left && Rectangle.Right - Rectangle.Width / 2 < racket.Rectangle.Right)
                Rectangle = new Rectangle(Rectangle.X, Rectangle.Y += (Dy *= -1), Rectangle.Width, Rectangle.Height);
        }

        /// <summary>
        /// Check for ball interactions with the blocks
        /// </summary>
        /// <param name="blocks"></param>
        private void CheckBlocksInteractions(Block[,] blocks) {
            for (int y = 0; y < blocks.GetLength(0); y++)
                for (int x = 0; x < blocks.GetLength(1); x++) {
                    if (Rectangle.Intersects(blocks[y, x].Rectangle) && blocks[y, x].IsActive && blocks[y, x].Visible) {

                        blocks[y, x].IsActive = false;
                        blocks[y, x].SetColor(Color.Red);
                        Rectangle = new Rectangle(Rectangle.X, Rectangle.Y += (Dy *= -1), Rectangle.Width, Rectangle.Height);

                        //Add score event:
                        OnScore();
                    }
                }
        }

        /// <summary>
        /// Draw ball
        /// </summary>
        public void Draw() {
            BrickBreaker.SpriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}
