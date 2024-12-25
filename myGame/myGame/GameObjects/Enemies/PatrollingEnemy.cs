using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.Animations;
using System;

namespace myGame.GameObjects.Enemies
{

    public class PatrollingEnemy : BaseEnemy
    {
        private float patrolDistance;

        public PatrollingEnemy(Texture2D texture, Vector2 startPosition, float patrolDistance = 200f, float attackRange = 60f)
            : base(texture, startPosition, 2f, attackRange)
        {
            this.patrolDistance = patrolDistance;
        }

        protected override void InitializeAnimation()
        {
            animation = new Animatie();
            animation.AddFrame(new AnimationFrame(new Rectangle(1, 62, 70, 60)));
            animation.AddFrame(new AnimationFrame(new Rectangle(151, 1, 74, 60)));
            animation.AddFrame(new AnimationFrame(new Rectangle(78, -1, 74, 60)));
            animation.AddFrame(new AnimationFrame(new Rectangle(1, 0, 74, 60)));
            animation.AddFrame(new AnimationFrame(new Rectangle(151, 1, 74, 60)));
        }

        protected override void InitializeAttackAnimation()
        {
            animation = new Animatie();
            animation.AddFrame(new AnimationFrame(new Rectangle(82, 62, 74, 60)));
            animation.AddFrame(new AnimationFrame(new Rectangle(151, 62, 74, 60)));
            animation.AddFrame(new AnimationFrame(new Rectangle(78, 123, 74, 60)));
            animation.AddFrame(new AnimationFrame(new Rectangle(1, 123, 74, 60)));
            animation.AddFrame(new AnimationFrame(new Rectangle(78, 123, 74, 60)));
        }

        protected override void UpdateBehavior(GameTime gameTime)
        {
            if (isAttacking)
            {
                animation.Update(gameTime);
                if (animation.IsAnimationComplete())
                {
                    isAttacking = false;
                    InitializeAnimation();
                }
                return;
            }

            // Basic patrol movement
            if (movingRight)
            {
                position.X += moveSpeed;
                if (position.X >= startX + patrolDistance)
                    movingRight = false;
            }
            else
            {
                position.X -= moveSpeed;
                if (position.X <= startX)
                    movingRight = true;
            }
            animation.Update(gameTime);
        }

        public override bool CheckPlayerInRange(Vector2 playerPosition)
        {
            if (isDying || isAttacking) return false;

            float verticalDistance = Math.Abs(position.Y - playerPosition.Y);
            float horizontalDistance = Math.Abs(position.X - playerPosition.X);

            bool isPlayerInFront = (movingRight && playerPosition.X > position.X) ||
                                  (!movingRight && playerPosition.X < position.X);

            if (horizontalDistance <= attackRange && verticalDistance < 30 && isPlayerInFront)
            {
                isAttacking = true;
                InitializeAttackAnimation();
                return true;
            }
            return false;
        }
    }
}