using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Enemy : BaseObject
{
    public Enemy(float x, float y, float angle, int height, int width) :
    base(x, y, angle, height, width)
    {

    }

    public override void Render(Graphics g)
    {
        g.Transform = GetTMatrix();

        g.DrawEllipse(new Pen(Color.Black, 2),
            0 - _width / 2, 0 - _height / 2,
            _width, _height
            );

        g.FillEllipse(new SolidBrush(Color.Green),
            0 - _width / 2, 0 - _height / 2,
            _width, _height
        );
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
