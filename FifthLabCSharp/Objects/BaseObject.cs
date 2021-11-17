using System.Drawing.Drawing2D;
using System.Drawing;
using System;
using System.Windows.Forms;

abstract class BaseObject
{
    protected float _x;
    protected float _y;
    protected float _angle;

    protected float _height;
    protected float _width;

    protected bool _isInBlackZone = false;

    public Action<BaseObject, BaseObject> OnOverlap;

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

    public float GetX()
    {
        return _x;
    }

    public float GetY()
    {
        return _y;
    }

    public float GetWidth()
    {
        return _width;
    }

    public float GetHeight()
    {
        return _height;
    }

    protected virtual GraphicsPath GetGraphicsPath()
    {
        return new GraphicsPath();
    }

    public virtual bool Overlaps(BaseObject obj, Graphics g)
    {
        var path1 = this.GetGraphicsPath();
        var path2 = obj.GetGraphicsPath();

        path1.Transform(this.GetTMatrix());
        path2.Transform(obj.GetTMatrix());

        var region = new Region(path1);
        region.Intersect(path2);

        return !region.IsEmpty(g);
    }

    public bool IsOnScreen(PictureBox pb)
    {
        return (_x - _width / 2) < pb.Width || (_y + _height / 2) < pb.Height;
    }

    public virtual void Overlap(BaseObject obj)
    {
        OnOverlap?.Invoke(this, obj);
    }

    public virtual void Render(Graphics g)
    {

    }

}
