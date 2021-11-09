using System.Drawing;

class Marker : BaseObject
{
    public Marker(float x, float y, float angle, int height, int width) :
        base(x, y, angle, height, width)
    {

    }

    public override void Render(Graphics g)
    {
        g.Transform = GetTMatrix();

        g.DrawEllipse(new Pen(Color.Red, 2), 
            0 - _width / 2, 0 - _height / 2, 
            _width, _height
        );
    }
}