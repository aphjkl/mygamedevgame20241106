using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using myGame.Animations;
using myGame.Input;
using myGame.interfaces;
using myGame.TileMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using myGame.Components;

namespace myGame.GameObjects
{
    internal class Hero : IGameObject
    {
        private HealthComponent healthComponent;
        private PhysicsComponent physicsComponent;
        private AnimationComponent animationComponent;
        private IInputReader inputReader;
        private Vector2 startPosition = new Vector2(100, 10);
        private bool wasInAir;

        public Vector2 Position => physicsComponent.Position;
        public int Health => healthComponent.Health;
        public bool IsInvulnerable => healthComponent.IsInvulnerable;

        public Hero(Texture2D texture, IInputReader reader)
        {
            inputReader = reader;
            healthComponent = new HealthComponent();
            physicsComponent = new PhysicsComponent(
                startPosition,
                new Rectangle((int)startPosition.X, (int)startPosition.Y, 68, 46)
            );
            animationComponent = new AnimationComponent(texture);

            healthComponent.OnDeath += () => System.Diagnostics.Debug.WriteLine("Hero died!");
        }

        public void Update(GameTime gameTime)
        {
            var direction = inputReader.ReadInput();
            bool isInAir = !physicsComponent.IsGrounded;

            if (direction.Y < 0)
            {
                physicsComponent.Jump();
            }

            if (direction.X != 0)
            {
                physicsComponent.Move(new Vector2(direction.X, 0));
                animationComponent.IsFacingRight = direction.X > 0;
            }

            physicsComponent.Update(gameTime);
            healthComponent.Update(gameTime);
            animationComponent.Update(gameTime, direction.X != 0, isInAir, wasInAir);

            wasInAir = isInAir;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animationComponent.Draw(spriteBatch, physicsComponent.Position);
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            physicsComponent.Collision(newRectangle, xOffset, yOffset);
        }

        public void TakeDamage(GameTime gameTime)
        {
            healthComponent.TakeDamage();
        }

        public void Reset()
        {
            physicsComponent.Reset(startPosition);
            healthComponent.Reset();
        }

        public bool CheckEnemyCollision(Enemy enemy)
        {
            return physicsComponent.IsCollidingWithEnemy(enemy.Bounds, enemy);
        }

        public void MakeInvulnerable(float duration)
        {
            healthComponent.MakeInvulnerable(duration);
        }
    }
}
