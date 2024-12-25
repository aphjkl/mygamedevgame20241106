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

            switch (action)
            {
                case "restart":
                    var playingState = gameRef.StateManager.GetState("Playing") as PlayingState;
                    playingState?.Restart();
                    gameRef.StateManager.SetState(GameState.Playing);
                    break;
                case "quit":
                    gameRef.Exit();
                    break;
            }
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }
    }
}