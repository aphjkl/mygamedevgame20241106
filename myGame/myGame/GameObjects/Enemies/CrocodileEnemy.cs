using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.Animations;

namespace myGame.GameObjects.Enemies
{
    public class CrocodileEnemy : BaseEnemy
    {
        public CrocodileEnemy(Texture2D texture, Vector2 startPosition, float attackRange = 60f)
            : base(texture, startPosition, 0f, attackRange)
        {
            this.rectangle = new Rectangle(
                (int)position.X,
                (int)position.Y,
                86,
                67
            );
        }

        protected override void InitializeAnimation()
        {
            animation = new Animatie();
            animation.AddFrame(new AnimationFrame(new Rectangle(1, 70, 86, 67)));
        }

        protected override void InitializeAttackAnimation()
        {
            animation = new Animatie();
            animation.AddFrame(new AnimationFrame(new Rectangle(89, 70, 86, 67)));
            animation.AddFrame(new AnimationFrame(new Rectangle(177, 70, 86, 67)));
            animation.AddFrame(new AnimationFrame(new Rectangle(89, 70, 86, 67)));
            animation.AddFrame(new AnimationFrame(new Rectangle(265, 70, 86, 67)));
            animation.AddFrame(new AnimationFrame(new Rectangle(353, 70, 86, 67)));
        }
    }
}
