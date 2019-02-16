using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Brick_Breaker
{
    public abstract class Shape
    {
        protected Rectangle position;
        protected Texture2D texture;

        public Shape(Rectangle rectangle, ContentManager content) {

            position = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            texture = content.Load<Texture2D>("images/block");
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public Rectangle GetPosition() {
            return position;
        }
    }
}
