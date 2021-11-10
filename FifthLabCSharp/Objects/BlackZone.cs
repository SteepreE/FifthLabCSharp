using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BlackZone : BaseObject
{
    public BlackZone(float x, float y, float angle, int height, int width, Color color) :
        base(x, y, angle, height, width, color)
    {

    }

    public void Move()
    {
        _x+=1.5f;
    }

    public override void Render(Graphics g)
    {
        g.Transform = GetTMatrix();

        g.FillRectangle(new SolidBrush(_color),
            0 - _width / 2, 0 - _height / 2,
            _width, _height
            );
    }

    protected override GraphicsPath GetGraphicsPath()
    {
        var path = base.GetGraphicsPath();

        path.AddRectangle( 
            new RectangleF(0 - _width / 2, 0 - _height / 2,
            _width, _height)
            );

        return path;
    }
}
