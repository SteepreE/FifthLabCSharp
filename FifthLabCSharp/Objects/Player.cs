using System;
using System.Drawing;
using System.Drawing.Drawing2D;

class Player : BaseObject
{
    private float _vX, _vY;

    public Player(float x, float y, float angle, int height, int width) : 
        base(x, y, angle, height, width)
    {
        _vX = 0;
        _vY = 0;
    }

    public void MoveTo(BaseObject target)
    {
        if (target != null)
        {
            float dx = target.GetX() - _x;
            float dy = target.GetY() - _y;

            float length = (float)Math.Sqrt(dx * dx + dy * dy);

            dx /= length;
            dy /= length;

            _vX += dx * 0.5f;
            _vY += dy * 0.5f;

            _angle = (float)(90 - Math.Atan2(_vX, _vY) * 180 / Math.PI);
        }


        _vX += -_vX * 0.1f;
        _vY += -_vY * 0.1f;

        _x += _vX;
        _y += _vY;
    }

    public override void Render(Graphics g)
    {
        g.Transform = GetTMatrix();

        var color = (_isInBlackZone) ? Color.White : Color.DeepPink;

        g.DrawEllipse(
            new Pen(Color.Black, 2),
            0 - _width / 2, 0 - _height / 2,
            _width, _height
        );

        g.FillEllipse(
            new SolidBrush(color),
            0 - _width / 2, 0 - _height / 2,
            _width, _height
        );

        g.DrawLine(new Pen(Color.Black, 2), 0, 0, 25, 0);
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
