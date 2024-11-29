/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.Camera;
using myGame.GameObjects;
using myGame.Input;
using myGame.TileMap;
using System.Collections.Generic;

namespace myGame.GameStates
{
    public class PlayingState : BaseGameState
    {
        private Hero hero;
        private Map map;
        private List<Enemy> enemies;
        private Camera2D camera;

        public PlayingState(Game1 game) : base(game)
        {
            // Initialize camera
            camera = new Camera2D(game.GraphicsDevice.Viewport);
            
            // Initialize map
            map = new Map(game.GraphicsDevice);
            
            // Initialize hero at a specific position
            hero = new Hero(game.Content.Load<Texture2D>("goldenCat"), new KeyboardReader())
            {
                hero.Position = new Vector2(100, 300) // Set initial position
            };
            
            // Initialize enemies
            enemies = new List<Enemy>();
            enemies.Add(new Enemy(game.Content.Load<Texture2D>("spriteEnemy-1"), new Vector2(300, 300)));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Begin SpriteBatch with camera transform
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null, null, null, null,
                camera.Transform);

            map?.Draw(spriteBatch);
            hero?.Draw(spriteBatch);
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Enter()
        {
            // Initialize game state
        }

        public override void Exit()
        {
            // Cleanup game state
        }

        public override void Update(GameTime gameTime)
        {
            hero?.Update(gameTime);
            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);
            }

            // Update camera to follow hero 
            if (hero != null)
            {
                camera.Follow(hero.Position);
            }
        }
    }
} */