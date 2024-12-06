using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace myGame.UI
{
    public class PauseScreen : UIScreen
    {
        private UIButton resumeButton;
        private UIButton replayButton;
        private UIButton quitButton;
        private string pauseText = "PAUSED";

        public PauseScreen(GraphicsDevice graphicsDevice, SpriteFont font)
            : base(new SpriteBatch(graphicsDevice), font)
        {
            int screenCenterX = graphicsDevice.Viewport.Width / 2;
            int screenCenterY = graphicsDevice.Viewport.Height / 2;

            resumeButton = new UIButton(
                graphicsDevice,
                new Rectangle(screenCenterX - 100, screenCenterY - 60, 200, 50),
                "Resume",
                font
            );

            replayButton = new UIButton(
                graphicsDevice,
                new Rectangle(screenCenterX - 100, screenCenterY, 200, 50),
                "Restart",
                font
            );

            quitButton = new UIButton(
                graphicsDevice,
                new Rectangle(screenCenterX - 100, screenCenterY + 60, 200, 50),
                "Quit",
                font
            );

            buttons.Add(resumeButton);
            buttons.Add(replayButton);
            buttons.Add(quitButton);
        }

        public string HandleInput(MouseState mouseState)
        {
            if (resumeButton.IsClicked(mouseState))
                return "resume";
            if (replayButton.IsClicked(mouseState))
                return "restart";
            if (quitButton.IsClicked(mouseState))
                return "quit";
            return "none";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Semi-transparent black background
            Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.Black * 0.7f });
            spriteBatch.Draw(pixel, spriteBatch.GraphicsDevice.Viewport.Bounds, Color.White);

            // Draw pause text
            Vector2 textSize = font.MeasureString(pauseText);
            Vector2 textPosition = new Vector2(
                resumeButton.Bounds.X + 100 - (textSize.X / 2),
                resumeButton.Bounds.Y - 80
            );
            spriteBatch.DrawString(font, pauseText, textPosition, Color.White);

            base.Draw(spriteBatch);
        }
    }
} 