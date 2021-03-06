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
            _player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0, 30, 30);

            _player.OnOverlap += (player, obj) =>
            {
                if (obj is Marker)
                {
                    _objects.Remove(obj);
                    _marker = null;
                }
                else if (obj is Enemy)
                {
                    var tempObj = obj as Enemy;

                    _score++;
                    ScoreLabel.Text = $"Счет: {_score}";

                    tempObj.Destroy(tempObj);
                }
            };

            _objects.Add(_player);
        }

        private void InitBlackZone()
        {
            _blackZone = new BlackZone(
                0 - (pbMain.Width / 3) / 2, pbMain.Height / 2, 0, pbMain.Height, pbMain.Width / 3
                );

            _blackZone.OnOverlap += (bz, obj) =>
            {
                _blackZone.AddNewObject(obj);
            };

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
            int size = 45 + rnd.Next(16);

            var newEnemy = new Enemy(
                15 + rnd.Next(pbMain.Width - 15), 15 + rnd.Next(pbMain.Height - 15), 0, 
                size, size
                );

            newEnemy.OnDestroy += (enemy) =>
            {
                _objects.Remove(enemy);
                _objects.Add(GenerateEnemy());
            };

            return newEnemy;
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            EnemiesDestruction();
            CheckOverlaps(g);
            RenderObjects(g);
        }

        private void EnemiesDestruction()
        {
            foreach (var obj in _objects.ToArray())
            {
                if (obj is Enemy)
                {
                    var enemy = obj as Enemy;

                    enemy.Destruction();
                }
            }
        }

        private void CheckOverlaps(Graphics g)
        {
            foreach (var obj in _objects.ToArray())
            {
                if (obj != _player && _player.Overlaps(obj, g))
                {
                    _player.Overlap(obj);
                }

                if (obj != _blackZone && _blackZone.Overlaps(obj, g))
                {
                    _blackZone.Overlap(obj);
                }
            }
        }

        private void RenderObjects(Graphics g)
        {
            g.Clear(Color.White);
            
            foreach (var obj in _objects)
            {
                obj.Render(g);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _player.MoveTo(_marker);

            if (!_blackZone.IsOnScreen(pbMain)) _blackZone.ToStart();
            else _blackZone.Move();

            _blackZone.UpdateObjsInZone();

            pbMain.Invalidate();
        }

        private void pbMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_marker != null)
                _objects.Remove(_marker);

            _marker = new Marker(e.X, e.Y, 0, 20, 20);
            _objects.Add(_marker);
        }
    }
}
