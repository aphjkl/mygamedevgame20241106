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
        private const int PORTAL_HEIGHT = 250;
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
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0 },
                { 0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            };
            map.LoadMap(mapData, 64);

            Vector2 portalPosition = new Vector2(1700, 6 * 64 - PORTAL_HEIGHT);
            levelPortal = new LevelPortal(gameRef.Content.Load<Texture2D>("castle-1"), portalPosition);

            SpawnEnemiesLevel1();
        }

        private void LoadLevel2()
        {
            int[,] mapData = new int[,]
            {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,1,1,0 },
                { 0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            };
            map.LoadMap(mapData, 64);

            Vector2 portalPosition = new Vector2(1700, 6 * 64 - PORTAL_HEIGHT);
            levelPortal = new LevelPortal(gameRef.Content.Load<Texture2D>("castle-1"), portalPosition);

            SpawnEnemiesLevel2();
        }

        private void LoadLevel3()
        {
            int[,] mapData = new int[,]
            {
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,1,0,0,0,0,1,1,0,0,0,0,0,0 },
                { 0,0,0,0,1,1,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            };
            map.LoadMap(mapData, 64);

            Vector2 portalPosition = new Vector2(1650, 3 * 64+20 - PORTAL_HEIGHT);
            levelPortal = new LevelPortal(gameRef.Content.Load<Texture2D>("castle-1"), portalPosition);

            
            SpawnEnemiesLevel3();
        }

        private void SpawnEnemiesLevel1()
        {
            float groundY = 10 * 64 - 347;  // Ground level (293)
            float platformY = 6 * 64 - 347;  // Platform level

            enemies.Add(enemyFactory.CreateEnemy("crocodile", new Vector2(400, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("patrol", new Vector2(800, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("crocodile", new Vector2(1200, groundY)));
        }

        private void SpawnEnemiesLevel2()
        {
            float groundY = 10 * 64 - 347;
            float platformY1 = 7 * 64 - 347;  // Platform at row 7
            float platformY2 = 6 * 64 - 347;  // Platform at row 6

            // Ground-only enemies
            enemies.Add(enemyFactory.CreateEnemy("aggressive", new Vector2(300, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("aggressive", new Vector2(1500, groundY)));
            
            // Platform enemies
            enemies.Add(enemyFactory.CreateEnemy("patrol", new Vector2(700, platformY1)));
            enemies.Add(enemyFactory.CreateEnemy("crocodile", new Vector2(1400, platformY1)));
        }

        private void SpawnEnemiesLevel3()
        {
            float groundY = 10 * 64 - 347;
            float platformY1 = 4 * 64 - 347;  
            float platformY2 = 7 * 64 - 347;  
            // Ground-only enemies
            enemies.Add(enemyFactory.CreateEnemy("aggressive", new Vector2(300, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("aggressive", new Vector2(900, groundY)));
            enemies.Add(enemyFactory.CreateEnemy("aggressive", new Vector2(1500, groundY)));
            
            // Platform enemies (matching actual platform locations in mapData)
            enemies.Add(enemyFactory.CreateEnemy("patrol", new Vector2(800, platformY1)));     
            enemies.Add(enemyFactory.CreateEnemy("crocodile", new Vector2(1400, platformY2))); 
        }
    }
} 