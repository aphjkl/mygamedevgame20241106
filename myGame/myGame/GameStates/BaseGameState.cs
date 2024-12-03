using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace myGame.GameStates
{
    public abstract class BaseGameState
    {
        protected Game1 gameRef;
        protected SpriteBatch spriteBatch;

        public BaseGameState(Game1 game)
        {
            gameRef = game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw();
        public abstract void Enter();
        public abstract void Exit();
    }
}