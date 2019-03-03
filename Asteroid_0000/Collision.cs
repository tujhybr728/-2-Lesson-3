using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroid_0000
{
    interface Collision
    {
        // интерфейс для обработки коллизии
        bool Collision(Collision obj);
        Rectangle Rect { get; }
    }
}
