using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace myGame.UI
{
    public class UIButton
    {
        private Texture2D texture;
        private Rectangle bounds;
        private string text;
        private SpriteFont font;
        private Color buttonColor;
        private Color textColor;
        private bool isHovered;

        public Rectangle Bounds => bounds;

        public UIButton(GraphicsDevice graphicsDevice, Rectangle bounds, string text, SpriteFont font)
        {
            this.bounds = bounds;
            this.text = text;
            this.font = font;
            this.buttonColor = Color.DarkGray;
            this.textColor = Color.White;

            // Create button texture
            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new[] { Color.White });
        }

        public void Update(MouseState mouseState)
        {
            isHovered = bounds.Contains(mouseState.Position);
            if (isHovered)
            {
                buttonColor = Color.Gray; // Lighten when hovered
            }
            else
            {
                buttonColor = Color.DarkGray;
            }
        }

        public bool IsClicked(MouseState mouseState)
        {
            return mouseState.LeftButton == ButtonState.Pressed && 
                   bounds.Contains(mouseState.Position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw button background
            spriteBatch.Draw(texture, bounds, buttonColor);

            // Center and draw text
            Vector2 textSize = font.MeasureString(text);
            Vector2 textPosition = new Vector2(
                bounds.X + (bounds.Width - textSize.X) / 2,
                bounds.Y + (bounds.Height - textSize.Y) / 2
            );
            spriteBatch.DrawString(font, text, textPosition, textColor);
        }

        
    }
} 