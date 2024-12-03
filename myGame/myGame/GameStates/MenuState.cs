using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using myGame.UI;
using System.Collections.Generic;

namespace myGame.GameStates
{
    public class MenuState : BaseGameState
    {
        private StartScreen startScreen;

        public MenuState(Game1 game) : base(game)
        {
            startScreen = new StartScreen(game.GraphicsDevice, game.Services.GetService<SpriteFont>());
        }

        public override void Draw()
        {
            spriteBatch.Begin();
            startScreen.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Enter()
        {
            // Initialize menu state if needed
        }

        public override void Exit()
        {
            // Cleanup menu state if needed
        }

        public override void Update(GameTime gameTime)
        {
            startScreen.Update(gameTime);
            if (startScreen.HandleInput(Mouse.GetState()))
            {
                gameRef.StateManager.SetState(GameState.Playing);
            }
        }
    }
}