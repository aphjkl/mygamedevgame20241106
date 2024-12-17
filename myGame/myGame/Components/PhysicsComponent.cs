using Microsoft.Xna.Framework;
using myGame.GameObjects.Enemies;
using myGame.TileMap;
using System;


namespace myGame.Components
{

    public class PhysicsComponent
    {
        private Vector2 position;
        private Vector2 velocity;
        private Rectangle bounds;
        
        // Movement constants
        private const float MAX_SPEED = 8f;
        private const float ACCELERATION = 0.4f;
        private const float GROUND_FRICTION = 0.1f;
        private const float AIR_RESISTANCE = 0.05f;
        private const float BOUNCE_FACTOR = 0.5f;
        
        private const float gravity = 0.5f;
        private const float jumpForce = -12f;
        private const float maxFallSpeed = 10f;
        private bool isGrounded;
        private Rectangle collisionRectangle;

        public Vector2 Position => position;
        public Rectangle Bounds => bounds;
        public bool IsGrounded => isGrounded;
        public Rectangle CollisionRectangle => collisionRectangle;

        public PhysicsComponent(Vector2 startPosition, Rectangle bounds)
        {
            position = startPosition;
            this.bounds = bounds;
            this.collisionRectangle = bounds;
            velocity = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
            // Apply friction/deceleration
            if (isGrounded)
            {
                // More friction on ground
                velocity.X = MathHelper.Lerp(velocity.X, 0, GROUND_FRICTION);
                if (Math.Abs(velocity.X) < 0.1f)
                    velocity.X = 0;
            }
            else
            {
                // Less friction in air
                velocity.X = MathHelper.Lerp(velocity.X, 0, AIR_RESISTANCE);
            }

            // Apply gravity
            if (!isGrounded)
            {
                velocity.Y += gravity;
                if (velocity.Y > maxFallSpeed)
                    velocity.Y = maxFallSpeed;
            }

            // Apply velocity to position
            position += velocity;

            // Update collision bounds
            bounds.X = (int)position.X;
            bounds.Y = (int)position.Y;
            collisionRectangle = new Rectangle(
                (int)position.X,
                (int)position.Y,
                bounds.Width,
                bounds.Height
            );

            isGrounded = false;
        }

        public void Move(Vector2 direction)
        {
            // Apply acceleration in movement direction
            if (direction.X != 0)
            {
                velocity.X += direction.X * ACCELERATION;
                velocity.X = MathHelper.Clamp(velocity.X, -MAX_SPEED, MAX_SPEED);
            }
        }

        public void Jump()
        {
            if (isGrounded)
            {
                velocity.Y = jumpForce;
                isGrounded = false;
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (bounds.TouchTopOf(newRectangle))
            {
                bounds.Y = newRectangle.Y - bounds.Height;
                position.Y = bounds.Y;
                velocity.Y = 0;
                isGrounded = true;
            }
            if (bounds.TouchBottomOf(newRectangle))
            {
                bounds.Y = newRectangle.Y + newRectangle.Height;
                position.Y = bounds.Y;
                velocity.Y = Math.Max(velocity.Y, 0.5f);  // Keep upward momentum but ensure downward movement
            }

            if (bounds.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - bounds.Width;
                bounds.X = (int)position.X;
                velocity.X = -velocity.X * BOUNCE_FACTOR; // Bounce off left wall
            }
            if (bounds.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width;
                bounds.X = (int)position.X;
                velocity.X = -velocity.X * BOUNCE_FACTOR; // Bounce off right wall
            }

            // World bounds collision with bounce
            if (position.X < 0)
            {
                position.X = 0;
                velocity.X = -velocity.X * BOUNCE_FACTOR;
            }
            if (position.X > xOffset - bounds.Width)
            {
                position.X = xOffset - bounds.Width;
                velocity.X = -velocity.X * BOUNCE_FACTOR;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
                velocity.Y = -velocity.Y * BOUNCE_FACTOR;
            }
            if (position.Y > yOffset - bounds.Height)
            {
                position.Y = yOffset - bounds.Height;
                isGrounded = true;
                velocity.Y = 0;
            }
        }

        public void Reset(Vector2 startPosition)
        {
            position = startPosition;
            velocity = Vector2.Zero;
            isGrounded = false;
        }

        public bool IsCollidingWithEnemy(Rectangle enemyBounds, BaseEnemy enemy)
        {
            if (bounds.Intersects(enemyBounds))
            {
                float heroBottom = bounds.Bottom;
                float enemyTop = enemyBounds.Top;
                float heroTop = bounds.Top;
                float enemyBottom = enemyBounds.Bottom;
                float verticalOverlap = heroBottom - enemyTop;
                
                if (velocity.Y > 0 && verticalOverlap <= 15 && heroTop < enemyTop)
                {
                    velocity.Y = jumpForce * 0.7f;
                    enemy.OnBeingJumpedOn();
                    return true;
                }
            }
            return false;
        }
    }
}