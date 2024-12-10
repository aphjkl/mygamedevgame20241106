using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace myGame.UI
{
    public class WinScreen : UIScreen
    {
        private UIButton menuButton;
        private UIButton quitButton;
        private string winText = "Congratulations!\nYou Won!";

        public WinScreen(GraphicsDevice graphicsDevice, SpriteFont font)
            : base(new SpriteBatch(graphicsDevice), font)
        {
            int screenCenterX = graphicsDevice.Viewport.Width / 2;
            int screenCenterY = graphicsDevice.Viewport.Height / 2;

            menuButton = new UIButton(
                graphicsDevice,
                new Rectangle(screenCenterX - 100, screenCenterY - 30, 200, 50),
                "Main Menu",
                font
            );

            quitButton = new UIButton(
                graphicsDevice,
                new Rectangle(screenCenterX - 100, screenCenterY + 40, 200, 50),
                "Quit",
                font
            );

            buttons.Add(menuButton);
            buttons.Add(quitButton);
        }

        public string HandleInput(MouseState mouseState)
        {
            if (menuButton.IsClicked(mouseState))
                return "menu";
            if (quitButton.IsClicked(mouseState))
                return "quit";
            return "none";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textSize = font.MeasureString(winText);
            Vector2 textPosition = new Vector2(
                menuButton.Bounds.X + 100 - (textSize.X / 2),
                menuButton.Bounds.Y - 100
            );
            spriteBatch.DrawString(font, winText, textPosition, Color.Gold);
            base.Draw(spriteBatch);
        }
    }
} 