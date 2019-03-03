using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroid_0000
{
    class Healing : BaseObj
    {

        // благодоря этому классу игрок будет жить дольше
        
        public Healing(Point pos, Point dir, Size size)
            : base(pos, dir, size)
        {

        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.Gray, Pos.X, Pos.Y, Size.Width, Size.Height);
            Game.Buffer.Graphics.FillEllipse(Brushes.Green, Pos.X, Pos.Y, Size.Width - 1, Size.Height - 2); // рисуем астероиды (врагов)
        }
        public override void Update() // блок обновления малых олбъектов
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width - 20) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height - 20) Dir.Y = -Dir.Y;
        }
    }
}
