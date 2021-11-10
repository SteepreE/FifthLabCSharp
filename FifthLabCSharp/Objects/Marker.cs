using System.Drawing.Drawing2D;
using System.Drawing;

class Marker : BaseObject
{
    public Marker(float x, float y, float angle, int height, int width, Color color) :
        base(x, y, angle, height, width, color)
    {

    }

    public override void Render(Graphics g)
    {
        g.Transform = GetTMatrix();

        g.DrawEllipse(new Pen(_color, 2), 
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