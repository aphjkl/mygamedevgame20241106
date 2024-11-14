using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myGame.Animation
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
