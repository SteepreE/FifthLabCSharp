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

        public Form1()
        {
            InitializeComponent();

            _player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0, 30, 30);
            _marker = new Marker(pbMain.Width / 3, pbMain.Height / 3, 0, 20, 20);

            _objects.Add(_marker);
            _objects.Add(_player);
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            foreach (var obj in _objects)
            {
                obj.Render(g);
            }

            _player.MoveTo(_marker);

            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            _objects.Remove(_marker);

            _marker = new Marker(e.X, e.Y, 0, 20, 20);
            _objects.Add(_marker);
            
            pbMain.Invalidate();
        }
    }
}
