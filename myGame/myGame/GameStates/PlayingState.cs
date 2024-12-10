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
using myGame.Managers;

namespace myGame.GameStates
{
    public class PlayingState : BaseGameState
    {
        private Hero hero;
        private Camera2D camera;
        private HealthDisplay healthDisplay;
        private LevelManager levelManager;
        private bool isInitialized = false;

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

                // Initialize hero and level manager
                hero = new Hero(gameRef.Content.Load<Texture2D>("goldenCat"), new KeyboardReader());
                levelManager = new LevelManager(gameRef);
                levelManager.InitializeLevel(1);

                // Create health display
                Vector2 healthPosition = new Vector2(10, 10);
                healthDisplay = new HealthDisplay(
                    gameRef.Content.Load<Texture2D>("heart-icon123"),
                    healthPosition,
                    3
                );
                
                isInitialized = true;
            }
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
            if (levelManager.CurrentLevel < LevelManager.MAX_LEVELS && levelManager.LevelPortal != null)
            {
                levelManager.LevelPortal.Update(gameTime, hero.Bounds);
                if (levelManager.LevelPortal.IsActivated)
                {
                    levelManager.InitializeLevel(levelManager.CurrentLevel + 1);
                    hero.Reset();
                    camera.Follow(hero.Position);
                }
            }

            if (levelManager.CurrentLevel == LevelManager.MAX_LEVELS && levelManager.LevelPortal != null)
            {
                levelManager.LevelPortal.Update(gameTime, hero.Bounds);
                if (levelManager.LevelPortal.IsActivated)
                {
                    gameRef.StateManager.SetState(GameState.Win);
                    return;
                }
            }

            // Check collision with all tiles
            // foreach (CollisionTiles tile in levelManager.Map.Tiles)
            // {
            //     hero.Collision(tile.Rectangle, levelManager.Map.Width, levelManager.Map.Height);
            // }
            foreach (var tile in levelManager.Map.CollisionTiles)
            {
                hero.Collision(tile.Rectangle, levelManager.Map.Width, levelManager.Map.Height);
            }
            camera.Follow(hero.Position);
            camera.UpdateMatrix();

            // Update and check enemies
            for (int i = levelManager.Enemies.Count - 1; i >= 0; i--)
            {
                var enemy = levelManager.Enemies[i];
                enemy.Update(gameTime);

                bool enemyKilled = hero.CheckEnemyCollision(enemy);
                if (enemyKilled || enemy.IsDying)
                {
                    levelManager.Enemies.RemoveAt(i);
                    if (enemyKilled)
                        hero.MakeInvulnerable(0.5f);
                    continue;
                }

                if (!hero.IsInvulnerable && enemy.CheckPlayerInRange(hero.Position))
                {
                    hero.TakeDamage(gameTime);
                }
            }

            if (hero.Health <= 0)
            {
                gameRef.StateManager.SetState(GameState.GameOver);
            }

            if (levelManager.CurrentLevel > LevelManager.MAX_LEVELS)
            {
                gameRef.StateManager.SetState(GameState.Win);
                return;
            }
        }

        public override void Draw()
        {
            if (!isInitialized) return;

            spriteBatch.Begin(transformMatrix: camera.TransformMatrix);
            
            levelManager.Map?.Draw(spriteBatch);
            
            if (levelManager.CurrentLevel < LevelManager.MAX_LEVELS && levelManager.LevelPortal != null)
            {
                levelManager.LevelPortal.Draw(spriteBatch);
            }

            foreach (var enemy in levelManager.Enemies)
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

        public void Restart()
        {
            isInitialized = false;
            InitializeGameState();
            hero.Reset();
            camera.Follow(hero.Position);
        }

        public override void Enter()
        {
            InitializeGameState();
        }

        public override void Exit()
        {
            // Cleanup if needed
        }
    }
}