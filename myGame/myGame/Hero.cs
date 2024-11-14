using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.Animation;
using myGame.interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace myGame
{
    internal class Hero : IGameObject
    {
        Texture2D heroTexture;
        Animatie animatie;

        public Hero(Texture2D texture)
        {
            heroTexture = texture;
            //deelRectangle = new Rectangle(moveOn_x, 0, 190, 225);
            animatie = new Animatie();
            animatie.AddFrame(new AnimationFrame(new Rectangle(0, 0, 180, 247)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(180, 0, 180, 247)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(360, 0, 180, 247)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(540, 0, 180, 247)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(720, 0, 180, 247)));
        }


        public void Update(GameTime gameTime)
        {
            animatie.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
             spriteBatch.Draw(heroTexture, new Vector2(10, 10), animatie.CurrentFrame.SourceRectangle, Color.White);
        }
    }
}
