using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.interfaces;
using myGame.Animations;

using myGame.Animations;

namespace myGame.GameObjects.Enemies
{

    public class EnemyFactory
    {
        private Game1 gameRef;

        public EnemyFactory(Game1 game)
        {
            gameRef = game;
        }

        public BaseEnemy CreateEnemy(string enemyType, Vector2 position)
        {
            return enemyType.ToLower() switch
            {
                "patrol" => new PatrollingEnemy(
                    gameRef.Content.Load<Texture2D>("spriteEnemy-1"),
                    position
                ),
                "aggressive" => new AggressiveEnemy(
                    gameRef.Content.Load<Texture2D>("spriteEnemy-2"),
                    position,
                    moveSpeed: 3f
                ),
                _ => throw new ArgumentException("Unknown enemy type")
            };
        }
    }
}