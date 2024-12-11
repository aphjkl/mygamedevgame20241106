using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myGame.Animations;
using System;

namespace myGame.GameObjects.Enemies
{
    public class CrocodileEnemy : BaseEnemy
    {
        private float attackRange;

        public CrocodileEnemy(Texture2D texture, Vector2 startPosition, float attackRange = 60f) 
            : base(texture, startPosition, 0f)
        {
            this.attackRange = attackRange;
            this.rectangle = new Rectangle(
                (int)position.X,
                (int)position.Y,
                86,
                67
            );
            InitializeAnimation();
        }

        protected override void UpdateBehavior(GameTime gameTime)
        {
            if (isAttacking)
            {
                if (animation.IsAnimationComplete())
                {
                    isAttacking = false;
                    InitializeAnimation();
                }
            }
            animation.Update(gameTime);
        }

        protected override void InitializeAnimation()
        {
            animation = new Animatie();
            
            animation.AddFrame(new AnimationFrame(new Rectangle(1, 70, 86, 67)));
        }

        private void InitializeAttackAnimation()
        {
            animation = new Animatie();
           
            animation.AddFrame(new AnimationFrame(new Rectangle(89, 70, 86, 67)));
            animation.AddFrame(new AnimationFrame(new Rectangle(177, 70, 86, 67)));
            animation.AddFrame(new AnimationFrame(new Rectangle(89, 70, 86, 67)));
            animation.AddFrame(new AnimationFrame(new Rectangle(265, 70, 86, 67)));
            animation.AddFrame(new AnimationFrame(new Rectangle(353, 70, 86, 67)));
        }

        public override bool CheckPlayerInRange(Vector2 playerPosition)
        {
            if (isDying || isAttacking) return false;

            float horizontalDistance = Math.Abs(position.X - playerPosition.X);
            float verticalDistance = Math.Abs(position.Y - playerPosition.Y);
            
            if (horizontalDistance <= attackRange && verticalDistance < 30)
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    InitializeAttackAnimation();
                    movingRight = playerPosition.X > position.X;
                }
                return isAttacking;
            }
            
            return false;
        }
    }
}
