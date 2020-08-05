using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_Breaker {
    public class Racket {

        private Texture2D Texture; //Racket's texture
        public Rectangle Rectangle; //Racket's rectangle
        private int Speed; //Racket's speed

        /// <summary>
        /// Receives coourdinats and size
        /// and initializes racket
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Racket(int x, int y, int width, int height) {
            Rectangle = new Rectangle(x, y, width, height);
            Speed = 8;
            LoadContent();
        }

        /// <summary>
        /// Load content
        /// </summary>
        public void LoadContent() {
            Texture = BrickBreaker.ContentManager.Load<Texture2D>("images/player/racket");
        }

        /// <summary>
        /// Move right
        /// </summary>
        public void MoveRight() {
            if (Rectangle.Right < BrickBreaker.Graphics.PreferredBackBufferWidth)
                Rectangle = new Rectangle(Rectangle.X + Speed, Rectangle.Y, Rectangle.Width, Rectangle.Height);
        }

        /// <summary>
        /// Move left
        /// </summary>
        public void MoveLeft() {
            if (Rectangle.Left > 0)
                Rectangle = new Rectangle(Rectangle.X - Speed, Rectangle.Y, Rectangle.Width, Rectangle.Height);
        }

        /// <summary>
        /// Draw racket
        /// </summary>
        public void Draw() {
            BrickBreaker.SpriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}
