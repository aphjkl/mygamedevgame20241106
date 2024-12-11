using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.GameObjects;
using myGame.GameObjects.Enemies;
using myGame.TileMap;
using System.Collections.Generic;

namespace myGame.Managers
{
    public class LevelManager
    {
        private Map map;
        private List<BaseEnemy> enemies;
        private EnemyFactory enemyFactory;
        private LevelPortal levelPortal;
        private Game1 gameRef;
        private int currentLevel;
        public const int MAX_LEVELS = 3;

        public int CurrentLevel => currentLevel;
        public Map Map => map;
        public List<BaseEnemy> Enemies => enemies;
        public LevelPortal LevelPortal => levelPortal;

        public LevelManager(Game1 game)
        {
            gameRef = game;
            enemies = new List<BaseEnemy>();
            map = new Map();
            enemyFactory = new EnemyFactory(game);
            currentLevel = 1;
        }

        public void InitializeLevel(int level)
        {
            currentLevel = level;
            enemies.Clear();

            switch (level)
            {
                case 1:
                    LoadLevel1();
                    break;
                case 2:
                    LoadLevel2();
                    break;
                case 3:
                    LoadLevel3();
                    break;
            }
        }

        private void LoadLevel1()
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

            Vector2 portalPosition = new Vector2(1700, 130);
            levelPortal = new LevelPortal(
                gameRef.Content.Load<Texture2D>("castle-1"),
                portalPosition
            );

            SpawnEnemiesLevel1();
        }

        private void LoadLevel2()
        {
            int[,] mapData = new int[,]
            {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            };
            map.LoadMap(mapData, 64);
            SpawnEnemiesLevel2();
        }

        private void LoadLevel3()
        {
            int[,] mapData = new int[,]
            {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                
            };
            map.LoadMap(mapData, 64);

            Vector2 portalPosition = new Vector2(1700, 130);
            levelPortal = new LevelPortal(
                gameRef.Content.Load<Texture2D>("castle-1"),
                portalPosition
            );

            SpawnEnemiesLevel3();
        }

        private void SpawnEnemiesLevel1()
        {
            float groundY = 5 * 64 - 30;
            enemies.Add(enemyFactory.CreateEnemy("patrol", new Vector2(300, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("aggressive", new Vector2(500, groundY)));
        }

        private void SpawnEnemiesLevel2()
        {
            float groundY = 5 * 64 - 30;
            enemies.Add(enemyFactory.CreateEnemy("aggressive", new Vector2(300, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("patrol", new Vector2(700, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("aggressive", new Vector2(1000, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("crocodile", new Vector2(1200, groundY)));
//             enemies.Add(new CrocodileEnemy(
//     Game.Content.Load<Texture2D>("crocodile"), 
//     new Vector2(x, y),
//     200f  // Attack range
// ));
        }

        private void SpawnEnemiesLevel3()
        {
            float groundY = 5 * 64 - 30;
            enemies.Add(enemyFactory.CreateEnemy("aggressive", new Vector2(300, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("patrol", new Vector2(500, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("aggressive", new Vector2(700, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("patrol", new Vector2(900, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("aggressive", new Vector2(1100, groundY)));
        }
    }
} 