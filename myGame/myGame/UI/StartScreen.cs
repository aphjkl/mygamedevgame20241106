using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace myGame.UI
{
    public class StartScreen
    {
        private Texture2D buttonTexture;
        private Rectangle buttonRect;
        private SpriteFont font;
        private string buttonText = "Start";
        private Color buttonColor = Color.White;

        public StartScreen(GraphicsDevice graphicsDevice, SpriteFont font)
        {
            this.font = font;
            // Create a simple button texture
            buttonTexture = new Texture2D(graphicsDevice, 200, 50);
            Color[] data = new Color[200 * 50];
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.DarkGray;
            buttonTexture.SetData(data);

            // Center the button
            buttonRect = new Rectangle(
                graphicsDevice.Viewport.Width / 2 - 100,
                graphicsDevice.Viewport.Height / 2 - 25,
                200, 50);
        }

        public bool HandleInput(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed && 
                buttonRect.Contains(mouseState.Position))
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw button with a darker color for better visibility
            buttonColor = Color.DarkGray;
            spriteBatch.Draw(buttonTexture, buttonRect, buttonColor);
            
            Vector2 textSize = font.MeasureString(buttonText);
            Vector2 textPosition = new Vector2(
                buttonRect.X + (buttonRect.Width - textSize.X) / 2,
                buttonRect.Y + (buttonRect.Height - textSize.Y) / 2
            );
            
            // Draw text in white for better contrast
            spriteBatch.DrawString(font, buttonText, textPosition, Color.White);
        }
    }
} 