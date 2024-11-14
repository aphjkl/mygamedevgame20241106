using game_monogame1.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game_monogame1.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_monogame1
{
    internal class Hero : IGameObject
    {
        Texture2D heroTexture;
        private Rectangle deelRectangle;
        private int moveOn_x = 0;

        public Hero(Texture2D texture)
        {
            heroTexture = texture;
            deelRectangle = new Rectangle(moveOn_x, 0, 190, 225);

        }



        public void Update()
        {
            moveOn_x += 200;
            if (moveOn_x > 1200)
                moveOn_x = 0;

            deelRectangle.X = moveOn_x;
        }
        public void Draw(SpriteBatch spritebatch)
        {


            spriteBatch.Draw(texture, new Vector2(10, 10), deelRectangle, Color.White);

        }

       
    }
}
