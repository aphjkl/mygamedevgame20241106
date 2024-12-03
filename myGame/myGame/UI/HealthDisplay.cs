using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace myGame.UI
{
    public class HealthDisplay
    {
        private Texture2D heartTexture;
        private Vector2 position;
        private int maxHearts;
        private float scale = 0.07f;
        private int padding = 5;

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
                float opacity = i < currentHealth ? 1f : 0.3f;

                spriteBatch.Draw(
                    heartTexture,
                    currentPos,
                    null,
                    Color.White * opacity,
                    0f,
                    Vector2.Zero,
                    scale,
                    SpriteEffects.None,
                    0f
                );

                currentPos.X += (heartTexture.Width * scale) + padding;
            }
        }
    }
} 