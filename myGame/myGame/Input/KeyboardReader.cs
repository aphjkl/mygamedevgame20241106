﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace myGame.Input
{
    class KeyboardReader : IInputReader
    {
        public Vector2 ReadInput()
        {
            //var direction = Vector2.Zero;
            //KeyboardState state = Keyboard.GetState();
            //if (state.IsKeyDown(Keys.Left))
            //    direction = new Vector2(-1, 0);
            //if (state.IsKeyDown(Keys.Right))
            //    direction = new Vector2(1, 0);
            //return direction;
            var direction = Vector2.Zero;
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.A))
                direction.X -= 1;
            if (state.IsKeyDown(Keys.D))
                direction.X += 1;

            return direction;
        }
    }
}