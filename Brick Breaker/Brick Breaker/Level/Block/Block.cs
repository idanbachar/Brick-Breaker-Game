using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_Breaker {
    public class Block {

        public bool IsActive; //Indication if block is active
        public bool Visible; //Indication of block visibility
        public Rectangle Rectangle; //Block's rectangle


        private Texture2D Texture; //Block's texture
        private Color Color; //Current block's color
        private int Speed; //Current block's speed

        /// <summary>
        /// Receives coordinates and size
        /// and initializes block
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Block(int x, int y, int width, int height, Color color) {
            IsActive = true;
            Rectangle = new Rectangle(x, y, width, height);
            Speed = 8;
            Color = color;
            Visible = true;
            LoadContent();
        }

        /// <summary>
        /// Load block's content
        /// </summary>
        public void LoadContent() {

            Texture = BrickBreaker.ContentManager.Load<Texture2D>("images/block/block");
        }

        /// <summary>
        /// Receives new color and sets block's color
        /// </summary>
        /// <param name="newColor"></param>
        public void SetColor(Color newColor) {
            Color = newColor;
        }

        /// <summary>
        /// Update block
        /// </summary>
        public void Update() {

            //Checks if block is not active, then make it fall:
            if (!IsActive)
                Fall();
        }

        /// <summary>
        /// Make the block fall
        /// </summary>
        public void Fall() {
            Rectangle = new Rectangle(Rectangle.X, Rectangle.Y + Speed, Rectangle.Width, Rectangle.Height);
        }

        /// <summary>
        /// Draw block
        /// </summary>
        public void Draw() {

            if (Visible)
                BrickBreaker.SpriteBatch.Draw(Texture, Rectangle, Color);
        }
    }
}
