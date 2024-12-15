using Microsoft.Xna.Framework;

namespace myGame.GameObjects.Enemies.Configuration
{
    public class EnemyStats
    {
        private Rectangle bounds;
        private Vector2 position;

        public float MoveSpeed { get; set; }
        public float DetectionRange { get; set; }
        public float AttackRange { get; set; }
        public float PatrolDistance { get; set; }

        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                bounds.X = (int)value.X;
                bounds.Y = (int)value.Y;
            }
        }

        public Rectangle Bounds => bounds;

        public void UpdateBounds(int x, int y, int? width = null, int? height = null)
        {
            bounds.X = x;
            bounds.Y = y;
            if (width.HasValue) bounds.Width = width.Value;
            if (height.HasValue) bounds.Height = height.Value;
        }

        public void InitializeBounds(Rectangle initialBounds)
        {
            bounds = initialBounds;
        }
    }
}