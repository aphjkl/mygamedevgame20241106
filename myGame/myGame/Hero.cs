using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Vector2 position;
        Vector2 snelheid;
        Vector2 versnelling;
        Vector2 mouseVector;

        public Hero(Texture2D texture)
        {
            heroTexture = texture;
            animatie = new Animatie();
            animatie.AddFrame(new AnimationFrame(new Rectangle(1, 1, 68, 56)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(70, 1, 68, 56)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(1, 1, 68, 56)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(139, 1, 68, 56)));
            position = new Vector2(10, 10);
            snelheid = new Vector2(1, 1);
            versnelling = new Vector2(0.1f, 0.1f);



        }


        public void Update(GameTime gameTime)
        {
            var direction = Vector2.Zero;
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Left))
                direction = new Vector2(-1, 0);
                if(state.IsKeyDown(Keys.Right))
                direction = new Vector2(1, 0);

            direction *= 4;
            position += direction;
            //Move();
            animatie.Update(gameTime);
        }
        private void Move()
        {
            position += snelheid;
        }
        private Vector2 GetMouseState()
        {
            MouseState state = Mouse.GetState();
            mouseVector = new Vector2(state.X, state.Y);
            return mouseVector;
        }
        private void Move(Vector2 mouse)
        {
            var direction = Vector2.Add(mouse,-position);
            direction.Normalize();
            direction = Vector2.Multiply(direction, 0.1f);

            snelheid += direction;
            snelheid = Limit(snelheid,6);

            position += snelheid;

            if (position.X > 600 || position.X < 0)
            {
                snelheid.X *= 1;
                versnelling.X *= 1;
            }

            if (position.Y > 400 || position.Y < 0)
            {
                snelheid.Y *= 1;
                versnelling*= 1;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
             spriteBatch.Draw(heroTexture, new Vector2(10, 10), animatie.CurrentFrame.SourceRectangle, Color.White,0, new Vector2(0,0),1.5f,SpriteEffects.None,0);
        }
    }
}
