using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using myGame.Animation;
using myGame.Input;
using myGame.interfaces;
using myGame.TileMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
        IInputReader inputReader;
        Rectangle rectangle;
        bool hasJumped = false;

        public Hero(Texture2D texture, IInputReader reader)
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

            
            this.inputReader = reader;
        }


        public void Update(GameTime gameTime)
        {

            var direction = inputReader.ReadInput();
            if (direction != Vector2.Zero)
            {
                direction *= 4;
                position += direction;
            }
            //Move(GetMouseState());
            animatie.Update(gameTime);
            
        }
        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if(rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                snelheid.Y = 0;
                hasJumped = false;
            }
            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;
            }
            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 2;
            }
            if (rectangle.TouchBottomOf(newRectangle))
            {
                snelheid.Y = 1;
            }

            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
            if (position.Y < 0) snelheid.Y = 1;
            if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;

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
            snelheid = Limit(snelheid, 5);
            position += snelheid;

            if (position.X > 600 || position.X < 0)
            {
                snelheid.X *= -1;
                versnelling.X *= -1;
            }

            if (position.Y > 400 || position.Y < 0)
            {
                snelheid.Y *= -1;
                versnelling *= -1;
            }
        }

        private Vector2 Limit(Vector2 vector, float limit)
        {
            if (vector.Length() > limit)
            {
                var ratio = limit / vector.Length();
                vector.X *= ratio;
                vector.Y *= ratio;
            }
            return vector;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
             spriteBatch.Draw(heroTexture,position, animatie.CurrentFrame.SourceRectangle, Color.White,0, new Vector2(0,0),1.5f,SpriteEffects.None,0);
        }
    }
}
