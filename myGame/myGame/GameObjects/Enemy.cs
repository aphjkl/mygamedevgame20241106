using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.Animations;
using myGame.interfaces;
using System;

namespace myGame.GameObjects
{
    public class Enemy : IGameObject
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle rectangle;
        private float moveSpeed = 2f;
        private bool movingRight = true;
        private float patrolDistance = 300f;
        private float startX;
        private Animatie animation;
        private bool isAttacking = false;
        private float attackRange = 100f;
        private float deathTimer = 0.2f;
        private bool isDying = false;

        public Enemy(Texture2D texture, Vector2 startPosition)
        {
            this.texture = texture;
            this.position = new Vector2(startPosition.X, startPosition.Y - 30);
            this.startX = startPosition.X;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, 74, 60);
            
            InitializeAnimation();
        }

        private void InitializeAnimation()
        {
            animation = new Animatie();
            // Adjust these rectangles based on your enemy sprite sheet
            animation.AddFrame(new AnimationFrame(new Rectangle(1, 62, 70, 60)));
            animation.AddFrame(new AnimationFrame(new Rectangle(151, 1, 74, 60)));
            animation.AddFrame(new AnimationFrame(new Rectangle(78, -1, 74, 60)));
            animation.AddFrame(new AnimationFrame(new Rectangle(1, 0, 74, 60)));
            animation.AddFrame(new AnimationFrame(new Rectangle(151, 1, 74, 60)));

        }

        public void Update(GameTime gameTime)
        {
            if (isDying)
            {
                deathTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                return; // Don't do anything else while dying
            }

            if (isAttacking)
            {
                animation.Update(gameTime);
                // If animation completes one cycle, check if we should continue attacking
                if (animation.IsAnimationComplete())
                {
                    isAttacking = false;
                    InitializeAnimation(); // Reset to walking animation
                }
            }
            
            if (!isAttacking)
            {
                // Basic patrol movement
                if (movingRight)
                {
                    position.X += moveSpeed;
                    if (position.X >= startX + patrolDistance)
                        movingRight = false;
                }
                else
                {
                    position.X -= moveSpeed;
                    if (position.X <= startX)
                        movingRight = true;
                }
                animation.Update(gameTime);
            }

            // Update rectangle position
            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effect = movingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, position, animation.CurrentFrame.SourceRectangle, 
                Color.White, 0, Vector2.Zero, 1.0f, effect, 0);
        }

        public Rectangle Bounds => rectangle;

        public bool CheckPlayerInRange(Vector2 playerPosition)
        {
            if (isDying || isAttacking) return false;

            float verticalDistance = Math.Abs(position.Y - playerPosition.Y);
            float horizontalDistance = Math.Abs(position.X - playerPosition.X);
            
            bool isPlayerInFront = (movingRight && playerPosition.X > position.X) || 
                                  (!movingRight && playerPosition.X < position.X);
            
            if (horizontalDistance <= attackRange && 
                verticalDistance < 30 && 
                isPlayerInFront)
            {
                isAttacking = true;
                animation = new Animatie();
                animation.AddFrame(new AnimationFrame(new Rectangle(82, 62, 74, 60)));
                animation.AddFrame(new AnimationFrame(new Rectangle(151, 62, 74, 60)));
                animation.AddFrame(new AnimationFrame(new Rectangle(78, 123, 74, 60)));
                animation.AddFrame(new AnimationFrame(new Rectangle(1, 123, 74, 60)));
                animation.AddFrame(new AnimationFrame(new Rectangle(78, 123, 74, 60)));
                
                return true;
            }
            return false;
        }

        public void Reset()
        {
            position = new Vector2(startX, position.Y);  // Reset to start X position
            movingRight = true;                         // Reset direction
            isAttacking = false;                        // Reset attack state
            InitializeAnimation();                      // Reset animation
        }

        public bool IsDying => isDying;

        public void OnBeingJumpedOn()
        {
            isDying = true;
        }
    }
} 