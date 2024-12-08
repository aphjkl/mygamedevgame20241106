using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace myGame.GameObjects
{
    public class LevelPortal
    {
        private Rectangle bounds;
        private Texture2D texture;
        private float activationTimer = 0f;
        private const float ACTIVATION_TIME = 2f; // Time player needs to stand in portal
        private bool isPlayerInPortal = false;

        public LevelPortal(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            bounds = new Rectangle((int)position.X, (int)position.Y, 64, 96); // Adjust size as needed
        }

        public void Update(GameTime gameTime, Rectangle playerBounds)
        {
            isPlayerInPortal = bounds.Intersects(playerBounds);
            
            if (isPlayerInPortal)
            {
                activationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                activationTimer = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }

        public bool IsActivated => activationTimer >= ACTIVATION_TIME;
    }
} 