using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace myGame.UI
{
    public class HealthDisplay
    {
        private Texture2D heartTexture;
        private Vector2 position;
        private int maxHearts;
        private float scale = 2f;
        private int padding = 10;

        public HealthDisplay(Texture2D heartTexture, Vector2 position, int maxHearts = 3)
        {
            this.heartTexture = heartTexture;
            this.position = position;
            this.maxHearts = maxHearts;
        }

        public void Draw(SpriteBatch spriteBatch, int currentHealth)
        {
            Vector2 currentPos = position;
            
            for (int i = 0; i < maxHearts; i++)
            {
                Rectangle sourceRect;
                if (i < currentHealth)
                {
                    sourceRect = new Rectangle(0, 0, 16, 16); // Full heart
                }
                else
                {
                    sourceRect = new Rectangle(16, 0, 16, 16); // Empty heart
                }

                spriteBatch.Draw(
                    heartTexture,
                    currentPos,
                    sourceRect,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    scale,
                    SpriteEffects.None,
                    0f
                );

                currentPos.X += (16 * scale) + padding;
            }
        }
    }
} 