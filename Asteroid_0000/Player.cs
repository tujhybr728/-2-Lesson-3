using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroid_0000
{
    class Player : BaseObj
    {
        // Здесь всё что нужно для инициализации игрока
        #region PlayerInfo

        public static event Message MessageDie;
        private int _energy = 100;
        public int Energy => _energy;
        private Image playerico = Image.FromFile("player.png");
        public Player(Point pos, Point dir, Size size) 
            : base(pos,dir,size)
        {
        }
        public void EnergyLow(int n)
        {
            _energy -= n;
        }
        public override void Draw()
        {
            //Game.Buffer.Graphics.DrawImage(playerico, new Point(Pos.X, Pos.Y));
            Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, 20, 20);
        }
        public override void Update()
        {
            //P += new KeyEventHandler(ListenKeyDown);
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
        }

        #endregion


        public void Die()
        {
           MessageDie.Invoke();
        }

        
        //Здесь всё что относиться к движению игрока
        #region PlayerMove
        public void Up(int st)
        {
            Dir.Y = st;
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
            Dir.Y = 0;
        }
        public void Down(int st)
        {
            Dir.Y = st;
            if (Pos.Y < Game.Height-30) Pos.Y = Pos.Y + Dir.Y;
            Dir.Y = 0;
        }
        public void Rigth(int st)
        {
            Dir.X = st;
            if (Pos.X < Game.Width-30) Pos.X = Pos.X + Dir.X;
            Dir.X = 0;
        }
        public void Left(int st)
        {
            Dir.X = st;
            if (Pos.X > 0) Pos.X = Pos.X - Dir.X;
            Dir.X = 0;
        }
        #endregion
    }
}
