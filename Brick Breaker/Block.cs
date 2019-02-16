using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Brick_Breaker
{
    public class Block: Shape
    {
        private bool visible;
        private Random rnd;
        private int numColor;

        public Block(Rectangle rectangle, ContentManager content) 
            :base (rectangle, content){

            rnd = new Random();
            numColor = rnd.Next(1, 10);
            visible = true;
        }

        public bool Visible { get { return this.visible;} set { this.visible = value; } }

        public override void Draw(SpriteBatch spriteBatch) {
            if (visible) {
                switch (numColor) {
                    case 1:
                        spriteBatch.Draw(texture, position, Color.DarkRed);
                        break;
                    case 2:
                        spriteBatch.Draw(texture, position, Color.DarkGreen);
                        break;
                    case 3:
                        spriteBatch.Draw(texture, position, Color.DarkCyan);
                        break;
                    case 4:
                        spriteBatch.Draw(texture, position, Color.DarkBlue);
                        break;
                    case 5:
                        spriteBatch.Draw(texture, position, Color.DarkMagenta);
                        break;
                    case 6:
                        spriteBatch.Draw(texture, position, Color.DarkKhaki);
                        break;
                    case 7:
                        spriteBatch.Draw(texture, position, Color.White);
                        break;
                    case 8:
                        spriteBatch.Draw(texture, position, Color.DarkViolet);
                        break;
                    case 9:
                        spriteBatch.Draw(texture, position, Color.DarkGray);
                        break;
                }
            }
        }

    }
}
