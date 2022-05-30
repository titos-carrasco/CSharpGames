using System;
using System.Drawing;
using rcr.lge;

public class Tortuga : Sprite
{
    private readonly LittleGameEngine lge;
    private char dir;
    private float velocity;

    public Tortuga(String iname, char dir, int x, int y, float velocity = 120) :
     base(iname, new PointF(x, y))
    {
        // acceso a LGE
        lge = LittleGameEngine.GetInstance();

        // mis atributos
        SetTag("tortuga");
        EnableCollider(true, true);
        this.dir = dir;
        this.velocity = velocity;
    }

    public override void OnUpdate(float dt)
    {
        SizeF cam = lge.GetCameraSize();

        float x = GetX();
        float y = GetY();
        float width = GetWidth();
        //float Height = GetHeight();

        if (dir == 'R')
        {
            x = x + velocity * dt;
            if (x > cam.Width)
                x = 0 - width;
        }
        else
        {
            x = x - velocity * dt;
            if (x + width < 0)
                x = cam.Width - 1;
        }

        SetPosition(x, y);
    }

    public override void OnCollision(float dt, GameObject[] gobjs)
    {
    }

}
