using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Brick_Breaker
{
    public class Ball
    {
        private Texture2D texture;
        private Rectangle position;
        private int dx;
        private int dy;
        private GraphicsDeviceManager graphics;

        public Ball(Rectangle rectangle, int dx, int dy, ContentManager content, GraphicsDeviceManager graphics) {

            texture = content.Load<Texture2D>("images/ball");
            position = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            this.dx = dx;
            this.dy = dy;
            this.graphics = graphics;
        }

        public void Move(Racket racket, Block[,] blocks, Game1 game1) {

            position = new Rectangle(position.X += dx, position.Y += dy, position.Width, position.Height);

            if (position.Right >= graphics.PreferredBackBufferWidth)
                position = new Rectangle(position.X += (dx *= -1), position.Y, position.Width, position.Height);

            if (position.Left <= 0)
                position = new Rectangle(position.X += (dx *= -1), position.Y, position.Width, position.Height);

            if (position.Top <= 0)
                position = new Rectangle(position.X, position.Y += (dy *= -1), position.Width, position.Height);

            if (position.Top >= graphics.PreferredBackBufferHeight)
                game1.IsGameOver = true;

                if (position.Bottom >= racket.GetPosition().Top && position.Top <= racket.GetPosition().Top && position.Left >= racket.GetPosition().Left && position.Right <= racket.GetPosition().Right)
                    position = new Rectangle(position.X, position.Y += (dy *= -1), position.Width, position.Height);

            for (int y = 0; y < blocks.GetLength(0); y++)
                for (int x = 0; x < blocks.GetLength(1); x++) {
                    if (position.Intersects(blocks[y, x].GetPosition()) && blocks[y, x].Visible) {
                        blocks[y, x].Visible = false;


                        position = new Rectangle(position.X, position.Y += (dy *= -1), position.Width, position.Height);
                        game1.Score++;
                        game1.MoneBlocks++;
                        game1.Bombs.Add(new Bomb(new Rectangle(blocks[y,x].GetPosition().X, blocks[y, x].GetPosition().Y, blocks[y, x].GetPosition().Width, blocks[y, x].GetPosition().Height), game1.Content));
                    }
                }
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
