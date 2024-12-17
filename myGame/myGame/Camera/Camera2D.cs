using Microsoft.Xna.Framework;

namespace myGame.Camera
{
    public class Camera2D
    {
        private Vector2 position;
        private float zoom;
        private Rectangle viewport;
        private Rectangle worldBounds;
        
        public Matrix Transform { get; private set; }
        public Matrix TransformMatrix { get; private set; }

        public Camera2D(Rectangle viewport, Rectangle worldBounds)
        {
            this.zoom = 1.0f;
            this.viewport = viewport;
            this.worldBounds = worldBounds;
            this.position = Vector2.Zero;
        }

        public void Follow(Vector2 target)
        {
            position.X = target.X - (viewport.Width / 2);
            position.Y = target.Y - (viewport.Height / 2);
            
            position.X = MathHelper.Clamp(position.X, 0, worldBounds.Width - viewport.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, worldBounds.Height - viewport.Height);
        }

        public void UpdateMatrix()
        {
            TransformMatrix = Matrix.CreateTranslation(new Vector3(-position, 0.0f));
        }
    }
} 