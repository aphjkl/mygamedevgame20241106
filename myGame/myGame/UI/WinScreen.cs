using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace myGame.UI
{
    public class WinScreen : UIScreen
    {
        private UIButton restartButton;
        private UIButton quitButton;
        private string winText = "Congratulations!\nYou Won!";

        public WinScreen(GraphicsDevice graphicsDevice, SpriteFont font)
            : base(new SpriteBatch(graphicsDevice), font)
        {
            int screenCenterX = graphicsDevice.Viewport.Width / 2;
            int screenCenterY = graphicsDevice.Viewport.Height / 2;

            restartButton = new UIButton(
                graphicsDevice,
                new Rectangle(screenCenterX - 100, screenCenterY - 30, 200, 50),
                "Restart",
                font
            );

            quitButton = new UIButton(
                graphicsDevice,
                new Rectangle(screenCenterX - 100, screenCenterY + 40, 200, 50),
                "Quit",
                font
            );

            buttons.Add(restartButton);
            buttons.Add(quitButton);
        }

        public string HandleInput(MouseState mouseState)
        {
            if (restartButton.IsClicked(mouseState))
                return "restart";
            if (quitButton.IsClicked(mouseState))
                return "quit";
            return "none";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textSize = font.MeasureString(winText);
            Vector2 textPosition = new Vector2(
                restartButton.Bounds.X + 100 - (textSize.X / 2),
                restartButton.Bounds.Y - 100
            );
            spriteBatch.DrawString(font, winText, textPosition, Color.Gold);
            base.Draw(spriteBatch);
        }
    }
} 