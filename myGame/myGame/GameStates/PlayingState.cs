using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.Camera;
using myGame.GameObjects;
using myGame.GameObjects.Enemies;
using myGame.Input;
using myGame.TileMap;
using myGame.UI;
using System.Collections.Generic;

namespace myGame.GameStates
{
    public class PlayingState : BaseGameState
    {
        private Hero hero;
        private Map map;
        private List<BaseEnemy> enemies;
        private Camera2D camera;
        private HealthDisplay healthDisplay;
        private EnemyFactory enemyFactory;

        public PlayingState(Game1 game) : base(game)
        {
            // Initialize camera
            camera = new Camera2D(
                new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height),
                new Rectangle(0, 0, 1920, 1080)
            );

            // Initialize game objects
            InitializeMap();
            enemyFactory = new EnemyFactory(game);
            
            hero = new Hero(game.Content.Load<Texture2D>("goldenCat"), new KeyboardReader());
            enemies = new List<BaseEnemy>();
            SpawnEnemies();

            healthDisplay = new HealthDisplay(
                game.Content.Load<Texture2D>("heart-icon123"),
                new Vector2(game.GraphicsDevice.Viewport.Width - 150, 20)
            );
        }

        private void InitializeMap()
        {
            map = new Map();
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
        }

        private void SpawnEnemies()
        {
            enemies.Clear();
            float groundY = 5 * 64 - 30;
            enemies.Add(enemyFactory.CreateEnemy("patrol", new Vector2(300, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("aggressive", new Vector2(500, groundY)));
        }

        public override void Draw()
        {
            spriteBatch.Begin(transformMatrix: camera.Transform);
            map.Draw(spriteBatch);
            hero.Draw(spriteBatch);
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            spriteBatch.End();

            // Draw UI without camera transform
            spriteBatch.Begin();
            healthDisplay.Draw(spriteBatch, hero.Health);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            hero.Update(gameTime);
            
            // Check collision with all tiles
            foreach (CollisionTiles tile in map.Tiles)
            {
                hero.Collision(tile.Rectangle, map.Width, map.Height);
            }

            camera.Follow(hero.Position);
            camera.UpdateMatrix();

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                var enemy = enemies[i];
                enemy.Update(gameTime);

                // Check for successful jump collision
                bool enemyKilled = hero.CheckEnemyCollision(enemy);
                if (enemyKilled || enemy.IsDying)
                {
                    enemies.RemoveAt(i);
                    if (enemyKilled)
                        hero.MakeInvulnerable(0.5f);
                    continue;
                }

                // Only check for damage if hero isn't invulnerable
                if (!hero.IsInvulnerable && enemy.CheckPlayerInRange(hero.Position))
                {
                    hero.TakeDamage(gameTime);
                }
            }

            // Add enemy collision with tiles
            foreach (var enemy in enemies)
            {
                foreach (CollisionTiles tile in map.Tiles)
                {
                    if (enemy.Bounds.Intersects(tile.Rectangle))
                    {
                        // Keep enemies on top of tiles
                        enemy.Position = new Vector2(enemy.Position.X, tile.Rectangle.Top - enemy.Bounds.Height);
                    }
                }
            }

            if (hero.Health <= 0)
            {
                gameRef.StateManager.SetState(GameState.GameOver);
            }
        }

        public override void Enter()
        {
            // Reset game state when entering
            hero.Reset();
            
            // Clear and recreate enemies list
            SpawnEnemies();
        }

        public override void Exit()
        {
            // Cleanup if needed
        }
    }
}