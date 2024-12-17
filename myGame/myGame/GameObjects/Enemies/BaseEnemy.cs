using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.Animations;
using myGame.interfaces;
using System;

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
        protected float attackRange;

        public BaseEnemy(Texture2D texture, Vector2 startPosition, float moveSpeed = 2f, float attackRange = 60f)
        {
            this.texture = texture;
            this.position = new Vector2(startPosition.X, startPosition.Y - 30);
            this.startX = startPosition.X;
            this.moveSpeed = moveSpeed;
            this.attackRange = attackRange;
            InitializeAnimation();
        }

        protected abstract void InitializeAnimation();
        protected abstract void InitializeAttackAnimation();

        protected virtual void UpdateBehavior(GameTime gameTime)
        {
            if (isAttacking)
            {
                animation.Update(gameTime);
                if (animation.IsAnimationComplete())
                {
                    isAttacking = false;
                    InitializeAnimation();
                }
                return;
            }

            animation.Update(gameTime);
        }

        public virtual bool CheckPlayerInRange(Vector2 playerPosition)
        {
            if (isDying || isAttacking) return false;

            float horizontalDistance = Math.Abs(position.X - playerPosition.X);
            float verticalDistance = Math.Abs(position.Y - playerPosition.Y);
            
            if (horizontalDistance <= attackRange && verticalDistance < 30)
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    InitializeAttackAnimation();
                    movingRight = playerPosition.X > position.X;
                }
                return true;
            }
            return false;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (isDying)
            {
                deathTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                return;
            }

            UpdateBehavior(gameTime);

            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (texture == null || isDying) return;

            SpriteEffects effect = movingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, position, animation.CurrentFrame.SourceRectangle, 
                Color.White, 0, Vector2.Zero, 1.0f, effect, 0);
        }

        public Rectangle Bounds => rectangle;
        public bool IsDying => isDying;

        public virtual void OnBeingJumpedOn()
        {
            isDying = true;
            deathTimer = 0.5f;
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

    }
}