using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.Animations;
using System;


namespace myGame.GameObjects.Enemies
{
    public class AggressiveEnemy : BaseEnemy
    {
        private float detectionRange;
        private Vector2? targetPosition;
        private float patrolDistance = 200f;

        public AggressiveEnemy(Texture2D texture, Vector2 startPosition, float moveSpeed = 3f, float detectionRange = 250f)
            : base(texture, startPosition, moveSpeed)
        {
            this.detectionRange = detectionRange;
            this.rectangle = new Rectangle(
                (int)position.X + 10,
                (int)position.Y - 30,
                64,
                60
            );
        }

        protected override void InitializeAnimation()
        {
            animation = new Animatie();
            animation.AddFrame(new AnimationFrame(new Rectangle(87, 1, 84, 91)));
            animation.AddFrame(new AnimationFrame(new Rectangle(173, 1, 84, 91)));
            animation.AddFrame(new AnimationFrame(new Rectangle(259, 1, 84, 91)));
            animation.AddFrame(new AnimationFrame(new Rectangle(345, 1, 84, 91)));
        }

        protected override void InitializeAttackAnimation()
        {
            animation = new Animatie();
            animation.AddFrame(new AnimationFrame(new Rectangle(1, 94, 84, 91)));
            animation.AddFrame(new AnimationFrame(new Rectangle(86, 92, 84, 91)));
            animation.AddFrame(new AnimationFrame(new Rectangle(1, 94, 84, 91)));
            animation.AddFrame(new AnimationFrame(new Rectangle(173, 94, 84, 91)));
            animation.AddFrame(new AnimationFrame(new Rectangle(259, 94, 84, 91)));
        }

        protected override void UpdateBehavior(GameTime gameTime)
        {
            base.UpdateBehavior(gameTime);

            if (!isAttacking)
            {
                UpdateMovement();
            }
        }

        private void UpdateMovement()
        {
            if (targetPosition.HasValue)
            {
                Vector2 direction = targetPosition.Value - position;
                if (direction.Length() > 0)
                {
                    direction.Normalize();
                    position.X += direction.X * moveSpeed;
                    movingRight = direction.X > 0;
                }
            }
            else
            {
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
            }
        }

        public override bool CheckPlayerInRange(Vector2 playerPosition)
        {
            if (isDying || isAttacking) return false;

            float horizontalDistance = Math.Abs(position.X - playerPosition.X);
            float verticalDistance = Math.Abs(position.Y - playerPosition.Y);

            if (horizontalDistance <= detectionRange && verticalDistance < 30)
            {
                targetPosition = playerPosition;

                if (horizontalDistance <= attackRange)
                {
                    if (!isAttacking)
                    {
                        isAttacking = true;
                        InitializeAttackAnimation();
                        movingRight = playerPosition.X > position.X;
                    }
                    return true;
                }
            }
            else
            {
                targetPosition = null;
            }
            return false;
        }
    }
}