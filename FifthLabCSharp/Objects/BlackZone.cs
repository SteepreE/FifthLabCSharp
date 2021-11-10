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
    public Action<BlackZone, PictureBox> OnScreenLeft;

    public BlackZone(float x, float y, float angle, int height, int width) :
        base(x, y, angle, height, width)
    {
        OnScreenLeft += (blackZone, pbMain) =>
        {
            _x = 0 - _width / 2;
        };
    }

    public void Move()
    {
        _x += 1.5f;
    }

    public void ScreenLeft(PictureBox pb)
    {
        OnScreenLeft?.Invoke(this, pb);
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
