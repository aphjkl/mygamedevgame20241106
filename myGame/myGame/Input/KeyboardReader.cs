using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myGame.Input
{
    class KeyboardReader : IInputReader
    {
        public Vector2 ReadInput()
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;
            
            if (state.IsKeyDown(Keys.A)||state.IsKeyDown(Keys.Left))
                direction.X -= 1;
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
                direction.X += 1;
            if (state.IsKeyDown(Keys.W)|| state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Up))
                direction.Y -= 1;
            

            return direction;
        }

       
    }
}
