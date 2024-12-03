using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.TileMap;


namespace myGame.Components
{

    public class PhysicsComponent
    {
        private Vector2 position;
        private Vector2 velocity;
        private Rectangle bounds;
        private float gravity = 0.5f;
        private float jumpForce = -12f;
        private float maxFallSpeed = 10f;
        private bool isGrounded;

        public Vector2 Position => position;
        public Rectangle Bounds => bounds;
        public bool IsGrounded => isGrounded;

        public PhysicsComponent(Vector2 startPosition, Rectangle bounds)
        {
            position = startPosition;
            this.bounds = bounds;
            velocity = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
            // Apply gravity when not grounded
            if (!isGrounded)
            {
                velocity.Y += gravity;
                if (velocity.Y > maxFallSpeed)
                    velocity.Y = maxFallSpeed;
            }

            // Apply velocity to position
            position += velocity;

            // Update rectangle position
            bounds.X = (int)position.X;
            bounds.Y = (int)position.Y;

            // Reset isGrounded - will be set true by collision check if needed
            isGrounded = false;
        }

        public void Move(Vector2 direction)
        {
            direction.X *= 4; // Movement speed
            position.X += direction.X;
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
            else if (bounds.TouchBottomOf(newRectangle))
            {
                position.Y = newRectangle.Y + newRectangle.Height;
                bounds.Y = (int)position.Y;
                velocity.Y = 1;
            }

            if (bounds.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - bounds.Width;
                bounds.X = (int)position.X;
            }
            if (bounds.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width;
                bounds.X = (int)position.X;
            }

            // World bounds collision
            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - bounds.Width) position.X = xOffset - bounds.Width;
            if (position.Y < 0)
            {
                position.Y = 0;
                velocity.Y = 0;
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

        public bool IsCollidingWithEnemy(Rectangle enemyBounds, bool isJumping)
        {
            if (bounds.Intersects(enemyBounds))
            {
                // If hero is falling onto enemy from above
                if (isJumping && velocity.Y > 0 && 
                    bounds.Bottom > enemyBounds.Top && 
                    bounds.Bottom < enemyBounds.Top + bounds.Height/2)
                {
                    velocity.Y = jumpForce * 0.7f; // Bounce off enemy
                    return true;
                }
            }
            return false;
        }
    }
}