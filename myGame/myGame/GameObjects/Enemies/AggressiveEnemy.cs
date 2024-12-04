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

        public AggressiveEnemy(Texture2D texture, Vector2 startPosition, float moveSpeed = 3f, float detectionRange = 150f) 
            : base(texture, startPosition, moveSpeed)
        {
            this.detectionRange = detectionRange;
            this.rectangle = new Rectangle(
                (int)position.X + 10,
                (int)position.Y,
                64,
                60
            );
        }

        protected override void UpdateBehavior(GameTime gameTime)
        {
            if (targetPosition.HasValue)
            {
                Vector2 direction = targetPosition.Value - position;
                if (direction.Length() > 0)
                {
                    direction.Normalize();
                    float newX = position.X + direction.X * moveSpeed;
                    position.X = newX;
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
            
            rectangle.X = (int)position.X + 10;
            rectangle.Y = (int)position.Y;
            
            animation.Update(gameTime);
        }

        protected override void InitializeAnimation()
        {
            animation = new Animatie();
            // Use different sprite frames for aggressive enemy
            animation.AddFrame(new AnimationFrame(new Rectangle(87, 1, 84, 91)));
            animation.AddFrame(new AnimationFrame(new Rectangle(173, 1, 84, 91)));
            animation.AddFrame(new AnimationFrame(new Rectangle(259, 1, 84, 91)));
            animation.AddFrame(new AnimationFrame(new Rectangle(345, 1, 84, 91)));
        }

        public void SetTarget(Vector2 target)
        {
            float distance = Vector2.Distance(position, target);
            targetPosition = distance <= detectionRange ? target : null;
        }

        public override bool CheckPlayerInRange(Vector2 playerPosition)
        {
            if (isDying || isAttacking) return false;

            float verticalDistance = Math.Abs(position.Y - playerPosition.Y);
            float horizontalDistance = Math.Abs(position.X - playerPosition.X);
            
            // Set target for chasing regardless of damage range
            SetTarget(playerPosition);
            
            // Only deal damage when very close (about half the detection range)
            if (verticalDistance <= 30 && horizontalDistance <= detectionRange * 0.5f)
            {
                return true;
            }
            
            return false;
        }
    }
} 