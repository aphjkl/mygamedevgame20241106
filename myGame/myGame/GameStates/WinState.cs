using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using myGame.UI;

namespace myGame.GameStates
{
    public class WinState : BaseGameState
    {
        private WinScreen winScreen;

        public WinState(Game1 game) : base(game)
        {
            winScreen = new WinScreen(game.GraphicsDevice, game.Services.GetService<SpriteFont>());
        }

        public override void Draw()
        {
            spriteBatch.Begin();
            winScreen.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            winScreen.Update(gameTime);
            string action = winScreen.HandleInput(Mouse.GetState());

            if (action == "menu")
            {
                gameRef.StateManager.SetState(GameState.StartScreen);
            }
            else if (action == "quit")
            {
                gameRef.Exit();
            }
        }

        public override void Enter()
        {
            // Initialize win state if needed
        }

        public override void Exit()
        {
            // Cleanup if needed
        }
    }
} 