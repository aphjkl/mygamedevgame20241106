using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace myGame.UI
{
    public class GameOverScreen : UIScreen
    {
        private UIButton replayButton;
        private UIButton quitButton;
        private string gameOverText = "Game Over";

        public GameOverScreen(GraphicsDevice graphicsDevice, SpriteFont font)
            : base(new SpriteBatch(graphicsDevice), font)
        {
            int screenCenterX = graphicsDevice.Viewport.Width / 2;
            int screenCenterY = graphicsDevice.Viewport.Height / 2;

            replayButton = new UIButton(
                graphicsDevice,
                new Rectangle(screenCenterX - 100, screenCenterY - 30, 200, 50),
                "Replay",
                font
            );

            quitButton = new UIButton(
                graphicsDevice,
                new Rectangle(screenCenterX - 100, screenCenterY + 40, 200, 50),
                "Quit",
                font
            );

            buttons.Add(replayButton);
            buttons.Add(quitButton);
        }

        public string HandleInput(MouseState mouseState)
        {
            if (replayButton.IsClicked(mouseState))
                return "replay";
            if (quitButton.IsClicked(mouseState))
                return "quit";
            return "none";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw "Game Over" text
            Vector2 textSize = font.MeasureString(gameOverText);
            Vector2 textPosition = new Vector2(
                replayButton.Bounds.X + 100 - (textSize.X / 2),
                replayButton.Bounds.Y - 80
            );
            spriteBatch.DrawString(font, gameOverText, textPosition, Color.Red);

            // Draw buttons using base class method
            base.Draw(spriteBatch);
        }
    }
} 