using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_monogame1.Interfaces
{
    internal interface IGameObject
{

        void Update();
        void Draw(SpriteBatch spritebatch);
}
}
 