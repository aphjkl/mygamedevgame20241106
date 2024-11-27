using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using myGame.Input;
using myGame.TileMap;
using myGame.Camera;
using System;
using System.Collections.Generic;
using myGame.GameObjects;

namespace myGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D texture;
        Hero hero;
        Map map;
        private Camera2D camera;
        private List<Enemy> enemies;
        private Texture2D enemyTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 480;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Create map first
            map = new Map();
            
            // Initialize camera with both viewport and world bounds
            camera = new Camera2D(
                new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight),
                new Rectangle(0, 0, 1920, 1080) // Set this to your map's actual dimensions
            );

            enemies = new List<Enemy>();

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
        }

        private void InitializeGameObjects()
        {

            hero = new Hero(texture,new KeyboardReader());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            hero.Update(gameTime);
            
            // Check collision with all tiles
            foreach (CollisionTiles tile in map.Tiles)
            {
                hero.Collision(tile.Rectangle, map.Width, map.Height);
            }

            // Update camera to follow hero
            camera.Follow(hero.Position);
            camera.UpdateMatrix();

            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: camera.Transform);
            
            map.Draw(_spriteBatch);
            hero.Draw(_spriteBatch);
            
            foreach (var enemy in enemies)
            {
                enemy.Draw(_spriteBatch);
            }
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
