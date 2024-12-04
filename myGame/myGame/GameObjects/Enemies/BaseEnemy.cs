using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.interfaces;
using myGame.Animations;

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
            this.position = startPosition;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }

        // Other common methods...
    }
}