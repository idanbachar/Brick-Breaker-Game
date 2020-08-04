using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Brick_Breaker
{
    public class Racket
    {
        private Rectangle position;
        private Texture2D texture;
        private int speed;
        private List<Bullet> bullets;


        public Racket(Rectangle rectangle, ContentManager content) {

            position = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            texture = content.Load<Texture2D>("images/block");
            speed = 8;
            bullets = new List<Bullet>();
        }

        public void MoveLeft() {
            position = new Rectangle((int)(position.X - speed), position.Y, position.Width, position.Height);
        }

        public void MoveRight() {
            position = new Rectangle((int)(position.X + speed), position.Y, position.Width, position.Height);
        }

        public void Check(Game1 game1) {

            foreach (Bomb b in game1.Bombs)
                if (position.Intersects(b.GetPosition()))
                    game1.IsGameOver = true;
        }

        public Rectangle GetPosition() {
            return position;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, Color.Blue);
        }

        public List<Bullet> Bullets { get { return this.bullets; } }
    }
}
