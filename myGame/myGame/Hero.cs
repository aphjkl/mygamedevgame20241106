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

            animatie = new Animatie();
            animatie.AddFrame(new AnimationFrame(new Rectangle(1, 1, 68, 56)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(70, 1, 68, 56)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(1, 1, 68, 56)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(139, 1, 68, 56)));



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
