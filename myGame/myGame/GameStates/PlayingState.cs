using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.Camera;
using myGame.GameObjects;
using myGame.GameObjects.Enemies;
using myGame.Input;
using myGame.TileMap;
using myGame.UI;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

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
        private bool isInitialized = false;
        private LevelPortal levelPortal;
        private int currentLevel = 1;
        private const int MAX_LEVELS = 2;

        public PlayingState(Game1 game) : base(game)
        {
        }

        private void InitializeGameState()
        {
            if (!isInitialized)
            {
                // Initialize camera
                camera = new Camera2D(
                    new Rectangle(0, 0, gameRef.GraphicsDevice.Viewport.Width, gameRef.GraphicsDevice.Viewport.Height),
                    new Rectangle(0, 0, 1920, 1080)
                );

                // Initialize game objects
                enemies = new List<BaseEnemy>();
                InitializeMap();
                enemyFactory = new EnemyFactory(gameRef);
                hero = new Hero(gameRef.Content.Load<Texture2D>("goldenCat"), new KeyboardReader());

                // Create health display with exactly 3 hearts
                Vector2 healthPosition = new Vector2(10, 10);
                healthDisplay = new HealthDisplay(
                    gameRef.Content.Load<Texture2D>("heart-icon123"),
                    healthPosition,
                    3  // Explicitly set to 3 hearts
                );

                // Initialize first level
                currentLevel = 1;
                InitializeLevel(currentLevel);
                
                isInitialized = true;
            }
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

        private void InitializeLevel(int level)
        {
            // Clear existing enemies
            enemies.Clear();

            switch (level)
            {
                case 1:
                    InitializeLevel1();
                    break;
                case 2:
                    InitializeLevel2();
                    break;
            }

            // Reset hero position for new level
            hero.Reset();
            camera.Follow(hero.Position);
        }

        private void InitializeLevel1()
        {
            int[,] mapData = new int[,]
            {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,1,1,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            };
            map.LoadMap(mapData, 64);
            
            // Add portal at end of level
            Vector2 portalPosition = new Vector2(1700, 130);
            levelPortal = new LevelPortal(
                gameRef.Content.Load<Texture2D>("castle-1"),
                portalPosition
            );

            SpawnEnemies();
        }

        private void InitializeLevel2()
        {
            int[,] mapData = new int[,]
            {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            };
            map.LoadMap(mapData, 64);
            SpawnEnemiesLevel2();
        }

        private void SpawnEnemiesLevel2()
        {
            float groundY = 5 * 64 - 30;
            enemies.Add(enemyFactory.CreateEnemy("aggressive", new Vector2(300, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("patrol", new Vector2(700, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("aggressive", new Vector2(1000, groundY)));
        }

        public override void Draw()
        {
            if (!isInitialized) return;

            spriteBatch.Begin(transformMatrix: camera.TransformMatrix);
            
            map?.Draw(spriteBatch);
            
            // Draw portal if not in last level
            if (currentLevel < MAX_LEVELS && levelPortal != null)
            {
                levelPortal.Draw(spriteBatch);
            }

            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }

            hero?.Draw(spriteBatch);
            
            spriteBatch.End();

            // UI elements
            spriteBatch.Begin();
            healthDisplay?.Draw(spriteBatch, hero.Health);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (!isInitialized)
            {
                InitializeGameState();
                return;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gameRef.StateManager.SetState(GameState.Pause);
                return;
            }

            hero.Update(gameTime);
            
            // Update portal
            if (currentLevel < MAX_LEVELS && levelPortal != null)
            {
                levelPortal.Update(gameTime, hero.Bounds);                
                if (levelPortal.IsActivated)
                {
                    currentLevel++;
                    InitializeLevel(currentLevel);
                    return;
                }
            }

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
            if (!isInitialized)
            {
                InitializeGameState();
                SpawnEnemies();
            }
        }

        public void Restart()
        {
            isInitialized = false;
            InitializeGameState();
            SpawnEnemies();
            hero.Reset();
            camera.Follow(hero.Position);
        }

        public override void Exit()
        {
            // Cleanup if needed
        }
    }
}