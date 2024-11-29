/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.UI;
using System.Collections.Generic;

namespace myGame.GameStates
{
    public class MenuState : BaseGameState
    {
        private StartScreen startScreen;
        private SpriteBatch spriteBatch;

        public MenuState(Game1 game) : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            startScreen = new StartScreen(game.GraphicsDevice, game.Services.GetService<SpriteFont>());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            startScreen.Draw(spriteBatch);
        }

        public override void Enter()
        {
            // Initialize menu state
        }

        public override void Exit()
        {
            // Cleanup menu state
        }

        public override void Update(GameTime gameTime)
        {
            if (startScreen.HandleInput(Microsoft.Xna.Framework.Input.Mouse.GetState()))
            {
                gameRef.StateManager.SwitchState("Playing");
            }
        }
    }
} */