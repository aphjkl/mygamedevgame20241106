using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using myGame.Input;
using myGame.TileMap;
using myGame.Camera;
using myGame.GameStates;
using System.Collections.Generic;
using myGame.GameObjects;

using myGame.UI;

namespace myGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
       // private GameStateManager stateManager;  // Add this line

        private Texture2D texture;
        Hero hero;
        Map map;
        private Camera2D camera;
        private List<Enemy> enemies;
        private Texture2D enemyTexture;
        private GameState currentState = GameState.StartScreen;
        private StartScreen startScreen;
        private SpriteFont font;
        private GameOverScreen gameOverScreen;
        private GameStateManager stateManager;

        public GameStateManager StateManager => stateManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 480;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            stateManager = new GameStateManager();  // Initialize state manager
        }

        protected override void Initialize()
        {
            
            map = new Map();
            enemies = new List<Enemy>();
            
            // Initialize camera with both viewport and world bounds
            camera = new Camera2D(
                new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight),
                new Rectangle(0, 0, 1920, 1080) // Set this to your map's actual dimensions
            );


            stateManager.SetState(GameState.StartScreen);  // Set initial state

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            texture = Content.Load<Texture2D>("goldenCat");
            Tiles.Content = Content;

            // Load a larger map
            int[,] mapData = new int[,]
            {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            };
            
            map.LoadMap(mapData, 64);
            InitializeGameObjects();

            enemyTexture = Content.Load<Texture2D>("spriteEnemy-1");
            enemies.Add(new Enemy(enemyTexture, new Vector2(300, 300)));

            font = Content.Load<SpriteFont>("gameFont"); // You'll need to create this
            Services.AddService(font); // Add font to game services
            startScreen = new StartScreen(GraphicsDevice, font);
            gameOverScreen = new GameOverScreen(GraphicsDevice, font);
        }

        private void InitializeGameObjects()
        {

            hero = new Hero(texture,new KeyboardReader());
        }

        protected override void Update(GameTime gameTime)
        {
            HandleExitInput();

            switch (stateManager.CurrentState)
            {
                case GameState.StartScreen:
                    UpdateStartScreen(gameTime);
                    break;
                case GameState.GameOver:
                    UpdateGameOver(gameTime);
                    break;
                case GameState.Playing:
                    UpdatePlaying(gameTime);
                    break;
            }

            

            base.Update(gameTime);
        }

        private void UpdateStartScreen(GameTime gameTime)
        {
            startScreen.Update(gameTime);
            if (startScreen.HandleInput(Mouse.GetState()))
            {
                stateManager.SetState(GameState.Playing);
            }
        }

        private void UpdateGameOver(GameTime gameTime)
        {
            gameOverScreen.Update(gameTime);
            string action = gameOverScreen.HandleInput(Mouse.GetState());
            if (action == "replay")
            {
                stateManager.SetState(GameState.Playing);
                ResetGame();
            }
            else if (action == "quit")
            {
                Exit();
            }
        }

        private void UpdatePlaying(GameTime gameTime)
        {
            hero.Update(gameTime);
            
            foreach (CollisionTiles tile in map.Tiles)
            {
                hero.Collision(tile.Rectangle, map.Width, map.Height);
            }

            camera.Follow(hero.Position);
            camera.UpdateMatrix();

            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);
                if (enemy.CheckPlayerInRange(hero.Position))
                {
                    hero.TakeDamage(gameTime);
                }
            }

            if (hero.Health <= 0)
            {
                stateManager.SetState(GameState.GameOver);
            }
        }

        private void ResetGame()
        {
            // Reset hero, enemies, etc.
            InitializeGameObjects();
            enemies.Clear();
            enemies.Add(new Enemy(enemyTexture, new Vector2(300, 300)));
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

           
            switch (stateManager.CurrentState)
    {
        case GameState.StartScreen:
            _spriteBatch.Begin();
            startScreen.Draw(_spriteBatch);
            _spriteBatch.End();
            break;

        case GameState.GameOver:
            _spriteBatch.Begin();
            gameOverScreen.Draw(_spriteBatch);
            _spriteBatch.End();
            break;

        case GameState.Playing:
            _spriteBatch.Begin(transformMatrix: camera.Transform);
            map.Draw(_spriteBatch);
            hero.Draw(_spriteBatch);
            foreach (var enemy in enemies)
            {
                enemy.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            break;
    }

            base.Draw(gameTime);
        }

        private void HandleExitInput()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
        }
    }
}
