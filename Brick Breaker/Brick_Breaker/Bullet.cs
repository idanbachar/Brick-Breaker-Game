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
using System.Text;

namespace Brick_Breaker
{
    public class Bullet : Shape
    {
        public Bullet(Rectangle rectangle, ContentManager content)
            : base(rectangle, content) {

        }

        public void Move() {
            position = new Rectangle(position.X, position.Y -= 10, position.Width, position.Height);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, Color.Orange);
        }

    }
}
