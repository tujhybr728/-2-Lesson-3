using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroid_0000
{
    class Game
    {
        //здесь всё что используется в данном классе
        #region Value
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        private static Random rnd = new Random(); // нужна для рандомного появления объектов и нестатичности первого объекта
        private static Image background = Image.FromFile(@"Stars.jpg");
        private static Timer timer = new Timer { Interval = 100 };
        private static int Record;
       

        #endregion

        // Здесь находиться всё что связано с иницииализацией
        #region InitInfo

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }



        static Game()
        {

        }



        public static void Init(Form form)
        {
            
                        
            Graphics g;         // Графическое устройство для вывода графики
            
            _context = BufferedGraphicsManager.Current;     // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            g = form.CreateGraphics();      // Создаем объект (поверхность рисования) и связываем его с формой                                         
            Width = form.ClientSize.Width;   // Запоминаем размеры формы
            Height = form.ClientSize.Height; 
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));// Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            form.KeyDown += Form_KeyDown;
            Load();            // загрузка объектов
            timer.Start(); //таймер задержки
            timer.Tick += Timer_Tick;
            Player.MessageDie += Finish;
        }

        #endregion
        //здесь находиться всё что связано с загрузкой и инициализацией данных
        #region Load



        public static Enemy[] _aster;    // массив маленьких объектов (взаимодействие)
        public static BigObj[] _bigobjs; // генерация массива объектов (круги)
        public static Player _player = new Player(new Point(100,100),new Point (0,0),new Size(20,20));
        public static Bullet _bullet;
        public static Healing[] _healing;
        public static void Load()
        {            
            int rndsize;
            _aster = new Enemy[values.maxobjenemy];
            _bigobjs = new BigObj[values.maxbigobj];
            _bullet = new Bullet(new Point(0, 0), new Point(0, 0), new Size(0, 0));
            _healing = new Healing[values.maxobjeheal];
            for (int i = 0; i < _aster.Length; i++)
            {
                rndsize = rnd.Next(values.minsize, values.maxxize); // нужна для разнообразия размера объектов
                _aster[i] = new Enemy(new Point(rnd.Next(100, 500), rnd.Next(100, 500)), new Point(1+rnd.Next(values.maxspeed),
                    rnd.Next(values.maxspeed)), new Size(20, 20));
            }
                        
            for (int i = 0; i < _bigobjs.Length; i++)
            {
                 rndsize = rnd.Next(values.sizebigobj/2,values.sizebigobj); // нужна для разнообразия размера объектов
                _bigobjs[i] = new BigObj(new Point(rnd.Next(0, 500), rnd.Next(0, 500)), new Point(5+rnd.Next(values.maxspeed),
                    rnd.Next(values.maxspeed)), new Size(rndsize, rndsize));
            }
            for (int i = 0; i < _healing.Length; i++)
            {
                _healing[i] = new Healing(new Point(rnd.Next(100, 500), rnd.Next(100, 500)), new Point(4 + rnd.Next(values.maxspeed),
                     rnd.Next(values.maxspeed)), new Size(20, 20));
            }

        }

        #endregion

        // Здесь находиться всё что требует обновления
        #region UbdateInfo 

        public static void Update() // метод обновления
        {
            
            foreach (BigObj obj in _bigobjs) // обновление положения больших объектов
                obj.Update();
            foreach (Healing h in _healing) // обновление положения хилящих сфер
                h.Update();
            foreach (Enemy obj in _aster) // обновление положения астероидов
            {
                obj.Update();
                if (obj.Collision(_bullet))
                { System.Media.SystemSounds.Hand.Play(); }
            }
            _bullet.Update();
            for (var i = 0; i < _healing.Length; i++)          //  в этом месте игрок хилиться
                if (_player.Collision(_healing[i]) && _player.Energy<100)
                    _player.EnergyLow(-values.power);
            
            for (var i = 0; i < _aster.Length; i++)
            {
                if (_aster[i] == null) continue;
                _aster[i].Update();
                if (_bullet != null && _bullet.Collision(_aster[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    Record = Record + 100;                    
                    continue;
                }
                if (_player.Collision(_aster[i]))
                {
                    _player.EnergyLow(values.power);               
                    System.Media.SystemSounds.Beep.Play();                    
                    continue;
                }
                if (_player.Energy == 0)
                {
                    Buffer.Graphics.DrawImage(background, new Point(-(Width / 2), -(Height / 2)));
                    _player.Die();
                }
                _player.Update();
                
            }
        }

        public static void Draw() //отоброжение объектов
        {
            Buffer.Graphics.DrawImage(background, new Point(-(Width / 2), -(Height / 2)));
            foreach (BigObj obj in _bigobjs) // сначала большие объекты
                obj.Draw();
            foreach (Enemy a in _aster) // затем маленькие
                a.Draw();
            foreach (Healing h in _healing) // затем маленькие
                h.Draw();
            _player.Draw();
            _bullet.Draw();
            if (_player != null)
                Buffer.Graphics.DrawString("Energy:" + _player.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            Buffer.Graphics.DrawString("Record:  " + Record, SystemFonts.DefaultFont, Brushes.White, 200, 0);        
            // счётчик попаданий по астероидам
            Buffer.Render();
        }

        #endregion

        // Здесь находиться всё что связано с игроком
        #region Player
        public static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) _bullet = new Bullet(new Point(_player.Rect.X + 10, _player.Rect.Y + 4), new Point(4, 0), new Size(4, 4));
            if (e.KeyCode == Keys.W) _player.Up(10);
            if (e.KeyCode == Keys.S) _player.Down(10);
            if (e.KeyCode == Keys.A) _player.Left(10);
            if (e.KeyCode == Keys.D) _player.Rigth(10);
        }

        

        public static void Finish()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Graphics.DrawString("Record: " + Record, new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 200);
            Buffer.Render();
        }
        #endregion

    }
}