using System.Drawing.Drawing2D;
using System.Drawing;

abstract class BaseObject
{
    protected float _x;
    protected float _y;
    protected float _angle;

    protected int _height;
    protected int _width;

    public BaseObject(float x, float y, float angle, int height, int width)
    {
        _x = x;
        _y = y;
        _angle = angle;

        _height = height;
        _width = width;
    }

    protected Matrix GetTMatrix()
    {
        var matrix = new Matrix();

        matrix.Translate(_x, _y);
        matrix.Rotate(_angle);

        return matrix;
    }

    public float getX()
    {
        return _x;
    }

    public float getY()
    {
        return _y;
    }

    public virtual void Render(Graphics g)
    {

    }

}
