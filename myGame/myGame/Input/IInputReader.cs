using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace myGame.Input
{
    public interface IInputReader
    {
        Vector2 ReadInput();
    }
}
