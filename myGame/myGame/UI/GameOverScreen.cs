using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace myGame.UI
{
    public class GameOverScreen
    {
        private Texture2D buttonTexture;
        private Rectangle replayButtonRect;
        private Rectangle quitButtonRect;
        private SpriteFont font;
        private string replayText = "Replay";
        private string quitText = "Quit";
        private string gameOverText = "Game Over";
        private Color buttonColor = Color.DarkGray;

        public GameOverScreen(GraphicsDevice graphicsDevice, SpriteFont font)
        {
            this.font = font;
            
            // Create button texture
            buttonTexture = new Texture2D(graphicsDevice, 200, 50);
            Color[] data = new Color[200 * 50];
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.DarkGray;
            buttonTexture.SetData(data);

            // Center the buttons
            int screenCenterX = graphicsDevice.Viewport.Width / 2;
            int screenCenterY = graphicsDevice.Viewport.Height / 2;

            replayButtonRect = new Rectangle(
                screenCenterX - 100,
                screenCenterY - 30,
                200, 50);

            quitButtonRect = new Rectangle(
                screenCenterX - 100,
                screenCenterY + 40,
                200, 50);
        }

        public string HandleInput(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (replayButtonRect.Contains(mouseState.Position))
                    return "replay";
                if (quitButtonRect.Contains(mouseState.Position))
                    return "quit";
            }
            return "none";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw "Game Over" text
            Vector2 gameOverSize = font.MeasureString(gameOverText);
            Vector2 gameOverPos = new Vector2(
                (replayButtonRect.X + 100) - (gameOverSize.X / 2),
                replayButtonRect.Y - 80
            );
            spriteBatch.DrawString(font, gameOverText, gameOverPos, Color.Red);

            // Draw Replay button
            spriteBatch.Draw(buttonTexture, replayButtonRect, buttonColor);
            Vector2 replaySize = font.MeasureString(replayText);
            Vector2 replayPos = new Vector2(
                replayButtonRect.X + (replayButtonRect.Width - replaySize.X) / 2,
                replayButtonRect.Y + (replayButtonRect.Height - replaySize.Y) / 2
            );
            spriteBatch.DrawString(font, replayText, replayPos, Color.White);

            // Draw Quit button
            spriteBatch.Draw(buttonTexture, quitButtonRect, buttonColor);
            Vector2 quitSize = font.MeasureString(quitText);
            Vector2 quitPos = new Vector2(
                quitButtonRect.X + (quitButtonRect.Width - quitSize.X) / 2,
                quitButtonRect.Y + (quitButtonRect.Height - quitSize.Y) / 2
            );
            spriteBatch.DrawString(font, quitText, quitPos, Color.White);
        }
    }
} 