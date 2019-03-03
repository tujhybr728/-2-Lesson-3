using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroid_0000
{
    class BigObj : BaseObj
    {
        
        // здесь находятся большие объекты с которыми игрок не может контактировать (или может?)
        public BigObj(Point pos, Point dir, Size size) 
            : base(pos,dir,size)
        {

        }

        Image star = Image.FromFile("Star.png");
        public override void Draw()
        {
            //Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y, Size.Width-1, Size.Height);
            Game.Buffer.Graphics.DrawImageUnscaled(star, new Point(Pos.X,Pos.Y));
        }
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}
