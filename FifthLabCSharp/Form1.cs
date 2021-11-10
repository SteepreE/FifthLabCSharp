using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FifthLabCSharp
{
    public partial class Form1 : Form
    {
        public static Random rnd = new Random();

        private List<BaseObject> _objects = new List<BaseObject>();

        private Player _player;
        private Marker _marker;
        private BlackZone _blackZone;

        private int _score = 0;

        public Form1()
        {
            InitializeComponent();
            InitBlackZone();
            InitPlayer();
            InitEnemies();
        }

        private void InitPlayer()
        {
            _player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0, 30, 30, Color.DeepPink);

            _player.OnOverlap += (player, obj) =>
            {
                if (obj is Marker)
                {
                    _objects.Remove(obj);
                    _marker = null;
                }
                else if (obj is Enemy)
                {
                    _objects.Remove(obj);

                    _score++;
                    ScoreLabel.Text = $"Счет: {_score}";

                    _objects.Add(GenerateEnemy());
                }
            };

            _objects.Add(_player);
        }

        private void InitBlackZone()
        {
            _blackZone = new BlackZone(
                0 - (pbMain.Width / 3) / 2, pbMain.Height / 2, 0, pbMain.Height, pbMain.Width / 3,
                Color.Black
                );

            _objects.Add(_blackZone);
        }

        private void InitEnemies()
        {
            for (int i=0; i<3; i++)
            {
                _objects.Add(GenerateEnemy());
            }
        }

        private Enemy GenerateEnemy()
        {
            return new Enemy(
                rnd.Next(pbMain.Width - 15), 15 + rnd.Next(pbMain.Height - 15), 0, 30, 30, Color.Green
                );
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            foreach (var obj in _objects.ToArray())
            {
                if (!(obj is Player) && _player.Overlaps(obj, g))
                {
                    _player.Overlap(obj);
                }

                obj.Render(g);
            }
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (_marker!= null)
                _objects.Remove(_marker);

            _marker = new Marker(e.X, e.Y, 0, 20, 20, Color.Red);
            _objects.Add(_marker);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _player.MoveTo(_marker);
            _blackZone.Move();

            pbMain.Invalidate();
        }
    }
}
