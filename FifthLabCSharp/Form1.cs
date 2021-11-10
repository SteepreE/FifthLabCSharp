using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FifthLabCSharp
{
    public partial class Form1 : Form
    {
        private List<BaseObject> _objects = new List<BaseObject>();

        private Marker _marker;
        private Player _player;

        private int _score = 0;

        public Form1()
        {
            InitializeComponent();

            _player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0, 30, 30);
            _marker = null;

            _objects.Add(_player);
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            foreach (var obj in _objects.ToArray())
            {
                if (!(obj is Player) && _player.Overlaps(obj, g))
                {
                    if (obj is Marker)
                    {
                        _objects.Remove(_marker);
                        _marker = null;
                    }

                    if (obj is Enemy)
                    {
                        _score++;
                        ScoreLabel.Text = $"Счет: {_score}";
                    }
                }

                obj.Render(g);
            }
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (_marker!= null)
                _objects.Remove(_marker);

            _marker = new Marker(e.X, e.Y, 0, 20, 20);
            _objects.Add(_marker);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _player.MoveTo(_marker);

            pbMain.Invalidate();
        }
    }
}
