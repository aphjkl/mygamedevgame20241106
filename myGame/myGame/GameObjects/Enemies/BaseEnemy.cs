using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.interfaces;
using myGame.Animations;
using System;

using myGame.Animations;

namespace myGame.GameObjects.Enemies
{
    public abstract class BaseEnemy : IGameObject
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle rectangle;
        protected float moveSpeed;
        protected bool movingRight;
        protected float startX;
        protected Animatie animation;
        protected bool isAttacking;
        protected bool isDying;
        protected float deathTimer;

        public BaseEnemy(Texture2D texture, Vector2 startPosition, float moveSpeed = 2f)
        {
            this.texture = texture;
            this.position = new Vector2(startPosition.X, startPosition.Y - 30);
            this.startX = startPosition.X;
            this.moveSpeed = moveSpeed;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, 74, 60);
            InitializeAnimation();
        }

        protected abstract void InitializeAnimation();
        protected abstract void UpdateBehavior(GameTime gameTime);

        public virtual void Update(GameTime gameTime)
        {
            if (isDying)
            {
                deathTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                return;
            }

            UpdateBehavior(gameTime);

            // Update rectangle position
            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
        }

        public Rectangle Bounds => rectangle;
        public bool IsDying => isDying;

        public virtual void OnBeingJumpedOn()
        {
            isDying = true;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effect = movingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, position, animation.CurrentFrame.SourceRectangle, 
                Color.White, 0, Vector2.Zero, 1.0f, effect, 0);
        }

        public virtual bool CheckPlayerInRange(Vector2 playerPosition)
        {
            return false; // Base implementation returns false, derived classes will override
        }

        public Vector2 Position
        {
            get => position;
            set 
            { 
                position = value;
                rectangle.X = (int)position.X;
                rectangle.Y = (int)position.Y;
            }
        }

        // Other common methods...
    }
}