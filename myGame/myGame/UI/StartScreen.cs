using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace myGame.UI
{
    public class StartScreen : UIScreen
    {
        private UIButton startButton;

        public StartScreen(GraphicsDevice graphicsDevice, SpriteFont font)
            : base(new SpriteBatch(graphicsDevice), font)
        {
            // Create start button centered on screen
            startButton = new UIButton(
                graphicsDevice,
                new Rectangle(
                    graphicsDevice.Viewport.Width / 2 - 100,
                    graphicsDevice.Viewport.Height / 2 - 25,
                    200, 50
                ),
                "Start",
                font
            );
            buttons.Add(startButton);
        }

        public bool HandleInput(MouseState mouseState)
        {
            return startButton.IsClicked(mouseState);
        }

    }
} 