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
        private float gravity = 0.5f;
        private bool isGrounded;
        private float jumpForce = -12f;
        private float maxFallSpeed = 10f;
        private bool isFacingRight = true;
        private int maxHealth = 3;
        private int currentHealth;
        private float invulnerabilityTime = 1.5f;
        private float invulnerabilityTimer = 0f;
        private bool isInvulnerable = false;

        public Hero(Texture2D texture, IInputReader reader)
        {
            heroTexture = texture;
            animatie = new Animatie();
            animatie.AddFrame(new AnimationFrame(new Rectangle(1, 1, 68, 56)));//i want this frame to be used as a mc resting or not moving framedw
            animatie.AddFrame(new AnimationFrame(new Rectangle(70, 1, 68, 56)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(1, 1, 68, 56)));
            animatie.AddFrame(new AnimationFrame(new Rectangle(139, 1, 68, 56)));
            position = new Vector2(100, 10);
            snelheid = new Vector2(0, 0);
            rectangle = new Rectangle((int)position.X, (int)position.Y, 68, 46);
            this.inputReader = reader;
            currentHealth = maxHealth;
        }

        public void Update(GameTime gameTime)
        {
            var direction = inputReader.ReadInput();
            
            // Handle jumping
            if (direction.Y < 0 && isGrounded)
            {
                snelheid.Y = jumpForce;
                isGrounded = false;
                System.Diagnostics.Debug.WriteLine("Jump initiated!");
            }

            // Apply gravity when not grounded
            if (!isGrounded)
            {
                snelheid.Y += gravity;
                if (snelheid.Y > maxFallSpeed)
                    snelheid.Y = maxFallSpeed;
            }

            // Handle horizontal movement
            if (direction.X != 0)
            {
                direction.X *= 4;
                position.X += direction.X;
                isFacingRight = direction.X > 0;
                animatie.Update(gameTime);
            }

            // Apply velocity to position
            position += snelheid;
            
            // Update rectangle position
            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;

            // Reset isGrounded - moved to end of update
            isGrounded = false;

            // Debug output
            //System.Diagnostics.Debug.WriteLine($"IsGrounded: {isGrounded}, Velocity Y: {snelheid.Y}, Position Y: {position.Y}");

            if (isInvulnerable)
            {
                invulnerabilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (invulnerabilityTimer <= 0)
                {
                    isInvulnerable = false;
                }
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                position.Y = rectangle.Y;
                snelheid.Y = 0;
                isGrounded = true;
                //System.Diagnostics.Debug.WriteLine("Touching ground!"); // Debug line
            }
            else if (rectangle.TouchBottomOf(newRectangle))
            {
                position.Y = newRectangle.Y + newRectangle.Height;
                rectangle.Y = (int)position.Y;
                snelheid.Y = 1;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width;
                rectangle.X = (int)position.X;
            }
            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width;
                rectangle.X = (int)position.X;
            }

            // World bounds collision
            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
            if (position.Y < 0) 
            {
                position.Y = 0;
                snelheid.Y = 0;
            }
            if (position.Y > yOffset - rectangle.Height) 
            {
                position.Y = yOffset - rectangle.Height;
                isGrounded = true;
                snelheid.Y = 0;
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
            SpriteEffects effect = isFacingRight ? SpriteEffects.FlipHorizontally : SpriteEffects.None ;
            spriteBatch.Draw(heroTexture, position, animatie.CurrentFrame.SourceRectangle, 
                Color.White, 0, new Vector2(0,0), 1.0f, effect, 0);
        }

        public Vector2 Position => position;

        public void TakeDamage(GameTime gameTime)
        {
            if (!isInvulnerable)
            {
                currentHealth--;
                isInvulnerable = true;
                invulnerabilityTimer = invulnerabilityTime;
            }
        }

        public int Health => currentHealth;

        public void Reset()
        {
            position = new Vector2(100, 10);  // Initial position from constructor
            snelheid = new Vector2(0, 0);     // Reset velocity
            currentHealth = maxHealth;         // Reset health
            isInvulnerable = false;           // Reset invulnerability
            invulnerabilityTimer = 0f;        // Reset timer
        }
    }
}
