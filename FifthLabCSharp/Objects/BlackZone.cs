using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

class BlackZone : BaseObject
{
    public List<BaseObject> _objsInZone = new List<BaseObject>();
    public List<BaseObject> _lastObjsInZone = new List<BaseObject>();

    public BlackZone(float x, float y, float angle, int height, int width) :
        base(x, y, angle, height, width)
    {
        
    }

    public void Move()
    {
        _x += 1.5f;
    }

    public void ToStart()
    {
        _x = 0 - _width / 2;
    }

    public override void Render(Graphics g)
    {
        g.Transform = GetTMatrix();

        g.FillRectangle(new SolidBrush(Color.Black),
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
