using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using myGame.GameStates;

namespace myGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameStateManager stateManager;
        private SpriteFont font;
        private KeyboardState previousKeyboardState;

        public GameStateManager StateManager => stateManager;

        public Game1()
{
    _graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;
    stateManager = new GameStateManager(this);
}

        protected override void Initialize()
        {
            myGame.TileMap.Tiles.Content = Content;
            
            previousKeyboardState = Keyboard.GetState();
            base.Initialize();

            stateManager.AddState(GameState.Menu.ToString(), new MenuState(this));
            stateManager.AddState(GameState.Playing.ToString(), new PlayingState(this));
            stateManager.AddState(GameState.GameOver.ToString(), new GameOverState(this));
            stateManager.AddState(GameState.Pause.ToString(), new PauseState(this));
            stateManager.AddState(GameState.Win.ToString(), new WinState(this));
            
            stateManager.SwitchState(GameState.Menu.ToString());
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("gameFont");
            Services.AddService(font);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Exit();
            }

            if (stateManager.CurrentState == GameState.Playing && 
                currentKeyboardState.IsKeyDown(Keys.Escape) && 
                previousKeyboardState.IsKeyUp(Keys.Escape))
            {
                stateManager.SetState(GameState.Pause);
            }

            stateManager.Update(gameTime);
            base.Update(gameTime);

            previousKeyboardState = currentKeyboardState;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            stateManager.Draw();
            base.Draw(gameTime);
        }
    }
}
