using Microsoft.Xna.Framework;

namespace myGame.Animations
{
    public class AnimationFrame
    {
        public AnimationFrame(Rectangle sourceRectangle)
        {
            SourceRectangle = sourceRectangle;
        }

        public Rectangle SourceRectangle { get; set; }
    }
}
