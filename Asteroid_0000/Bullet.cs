using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroid_0000
{
    class Bullet : BaseObj
    {

        // здесь находятся то чем атакует игрок

        
        Image bulletico = Image.FromFile("bulletico.png");
        public Bullet(Point pos, Point dir, Size size) 
            : base(pos,dir,size)
        {

        }
        public override void Draw()
        {
            //Game.Buffer.Graphics.DrawImageUnscaled(bulletico, new Point(Pos.X, Pos.Y));
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X + values.speedbulet;
            if (Pos.X > Game.Width) Pos.X = 0;
            if (Pos.Y > Game.Height) Pos.Y = 0;
        }
    }
}
