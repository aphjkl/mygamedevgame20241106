using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using myGame.UI;

namespace myGame.GameStates
{
    public class GameOverState : BaseGameState
    {
        private GameOverScreen gameOverScreen;

        public GameOverState(Game1 game) : base(game)
        {
            gameOverScreen = new GameOverScreen(game.GraphicsDevice, game.Services.GetService<SpriteFont>());
        }

        public override void Draw()
        {
            spriteBatch.Begin();
            gameOverScreen.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            gameOverScreen.Update(gameTime);
            string action = gameOverScreen.HandleInput(Mouse.GetState());
            
            if (action == "replay")
            {
                gameRef.StateManager.SetState(GameState.Playing);
            }
            else if (action == "quit")
            {
                gameRef.Exit();
            }
        }

        public override void Enter()
        {
            // Initialize game over state if needed
        }

        public override void Exit()
        {
            // Cleanup if needed
        }
    }
}