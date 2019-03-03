using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroid_0000
{
    class Enemy : BaseObj 
    {
        // блок малых объектов (астероидов)

        public int Power { get; set; }
        public int heal { get; set; }

        public Enemy(Point pos, Point dir, Size size)
            : base(pos, dir, size)
        {
            Power = 10;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.Gray, Pos.X, Pos.Y, Size.Width, Size.Height);
            Game.Buffer.Graphics.FillEllipse(Brushes.Gray, Pos.X, Pos.Y, Size.Width-1, Size.Height-2); // рисуем астероиды (врагов)
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
