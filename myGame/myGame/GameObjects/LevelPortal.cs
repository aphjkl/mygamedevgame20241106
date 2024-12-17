using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace myGame.GameObjects
{
    public class LevelPortal
    {
        private Rectangle bounds;
        private Texture2D texture;
        private float activationTimer = 0f;
        private const float ACTIVATION_TIME = 1.5f; 
        private bool isPlayerInPortal = false;
        private const int PORTAL_WIDTH = 200;  
        private const int PORTAL_HEIGHT = 200; 

        public LevelPortal(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            bounds = new Rectangle(
                (int)position.X, 
                (int)position.Y, 
                PORTAL_WIDTH, 
                PORTAL_HEIGHT
            );
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
            Color portalColor = isPlayerInPortal ? Color.White * 0.8f : Color.White;
            spriteBatch.Draw(texture, bounds, portalColor);
        }

        public bool IsActivated => activationTimer >= ACTIVATION_TIME;
    }
} 