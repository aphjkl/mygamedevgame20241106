using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using myGame.UI;

namespace myGame.GameStates
{
    public class PauseState : BaseGameState
    {
        private PauseScreen pauseScreen;
        private KeyboardState previousKeyboardState;

        public PauseState(Game1 game) : base(game)
        {
            pauseScreen = new PauseScreen(game.GraphicsDevice, game.Services.GetService<SpriteFont>());
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            pauseScreen.Update(gameTime);
            string action = pauseScreen.HandleInput(Mouse.GetState());

            if (currentKeyboardState.IsKeyDown(Keys.Escape) &&
                previousKeyboardState.IsKeyUp(Keys.Escape))
            {
                gameRef.StateManager.SetState(GameState.Playing);
                return;
            }

            switch (action)
            {
                case "resume":
                    gameRef.StateManager.SetState(GameState.Playing);
                    break;
                case "restart":
                    var playingState = gameRef.StateManager.GetState("Playing") as PlayingState;
                    playingState?.Restart();
                    gameRef.StateManager.SetState(GameState.Playing);
                    break;
                case "quit":
                    gameRef.Exit();
                    break;
            }

            previousKeyboardState = currentKeyboardState;
        }

        public override void Draw()
        {
            // Draw the playing state first
            var playingState = gameRef.StateManager.GetState("Playing") as PlayingState;
            playingState?.Draw();

            // Draw pause menu on top
            spriteBatch.Begin();
            pauseScreen.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Enter()
        {
            previousKeyboardState = Keyboard.GetState();
        }

        public override void Exit() { }
    }
}