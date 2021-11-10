using System.Drawing.Drawing2D;
using System.Drawing;
using System;

abstract class BaseObject
{
    protected float _x;
    protected float _y;
    protected float _angle;

    protected int _height;
    protected int _width;

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

    public float getX()
    {
        return _x;
    }

    public float getY()
    {
        return _y;
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

    public virtual void Overlap(BaseObject obj)
    {
        if (this.OnOverlap != null)
        {
            this.OnOverlap(this, obj);
        }
    }

    public virtual void Render(Graphics g)
    {

    }

}
