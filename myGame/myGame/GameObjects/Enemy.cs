using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.Animation;
using myGame.interfaces;

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

            // Update rectangle position
            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;

            // Update animation
            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effect = movingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, position, animation.CurrentFrame.SourceRectangle, 
                Color.White, 0, Vector2.Zero, 1.0f, effect, 0);
        }

        public Rectangle Bounds => rectangle;
    }
} 