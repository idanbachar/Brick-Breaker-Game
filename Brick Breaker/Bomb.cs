using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Brick_Breaker
{
    public class Bomb : Shape
    {
        private bool visible;

        public Bomb(Rectangle rectangle, ContentManager content)
            : base(rectangle, content) {

            visible = true;
        }

        public bool Visible { get { return this.visible; } set { this.visible = value; } }

        public void Move() {
            position = new Rectangle(position.X, position.Y += 5, position.Width, position.Height);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if(visible)
                spriteBatch.Draw(texture, position, Color.Yellow);
        }

    }
}
