using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_Breaker {
    public class Player {

        public Racket Racket; //Racket

        /// <summary>
        /// Initializes player
        /// </summary>
        public Player() {

            int x = BrickBreaker.Graphics.PreferredBackBufferWidth / 2 - 100;
            int y = BrickBreaker.Graphics.PreferredBackBufferHeight - 50;
            int width = 100;
            int height = 15;

            //Create a new racket:
            Racket = new Racket(x, y, width, height);
        }

        public void Update(Block[,] blocks) {

            //Go right:
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Move(Direction.Right);
            else if (Keyboard.GetState().IsKeyDown(Keys.A)) //Go left:
                Move(Direction.Left);

            //Check for falling blocks interactions:
            Racket.CheckBlocksInteractions(blocks);
        }


        /// <summary>
        /// Move racket to received direction
        /// </summary>
        /// <param name="direction"></param>
        public void Move(Direction direction) {

            switch (direction) {
                case Direction.Right:  //Move right:
                    Racket.MoveRight();
                    break;
                case Direction.Left:  //Move left:
                    Racket.MoveLeft();
                    break;
            }
        }

        /// <summary>
        /// Draw racket
        /// </summary>
        public void Draw() {
            Racket.Draw();
        }
    }
}
