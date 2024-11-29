/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.UI;

namespace myGame.GameStates
{
    public class GameOverState : BaseGameState
    {
        private GameOverScreen gameOverScreen;
        private SpriteBatch spriteBatch;

        public GameOverState(Game1 game) : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            gameOverScreen = new GameOverScreen(game.GraphicsDevice, game.Services.GetService<SpriteFont>());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            gameOverScreen.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Enter()
        {
            // Initialize game over state
        }

        public override void Exit()
        {
            // Cleanup game over state
        }

        public override void Update(GameTime gameTime)
        {
            string action = gameOverScreen.HandleInput(Microsoft.Xna.Framework.Input.Mouse.GetState());
            if (action == "replay")
            {
                gameRef.StateManager.SwitchState("Playing");
            }
            else if (action == "quit")
            {
                gameRef.Exit();
            }
        }
    }
} */