using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Enemy : BaseObject
{
    public Action<Enemy> OnDestroy;

    public Enemy(float x, float y, float angle, int height, int width) :
    base(x, y, angle, height, width)
    {
        
    }

    public override void Render(Graphics g)
    {
        g.Transform = GetTMatrix();

        var color = (_isInBlackZone) ? Color.White : Color.Green;

        g.DrawEllipse(new Pen(Color.Black, 2),
            0 - _width / 2, 0 - _height / 2,
            _width, _height
            );

        g.FillEllipse(new SolidBrush(color),
            0 - _width / 2, 0 - _height / 2,
            _width, _height
        );
    }

    public void Destruction()
    {
        _height-=0.4f;
        _width -=0.4f;

        if (_height <= 0 && _width <= 0)
        {
            Destroy(this);
        }
    }

    public void Destroy(Enemy enemy)
    {
        OnDestroy?.Invoke(enemy);
    }

    protected override GraphicsPath GetGraphicsPath()
    {
        var path = base.GetGraphicsPath();

        path.AddEllipse(
            0 - _width / 2, 0 - _height / 2,
            _width, _height
            );

        return path;
    }
}
