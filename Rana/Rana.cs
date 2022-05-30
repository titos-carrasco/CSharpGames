using System;
using System.Drawing;
using System.Windows.Forms;
using rcr.lge;

public class Rana : Sprite
{
    private readonly LittleGameEngine lge;
    private float x0, y0, pixels;
    private int tdead;
    private bool moving, alive;

    public Rana(int x, int y) :
        base("rana-idle", new PointF(x, y))
    {

        // acceso a LGE
        lge = LittleGameEngine.GetInstance();

        // mis atributos
        x0 = GetX();
        y0 = GetY();
        EnableCollider(true, true);
        SizeF cam = lge.GetCameraSize();

        SetBounds(new RectangleF(new PointF(3, 20), new SizeF(cam.Width - 6, cam.Height - 22)));
        start();
    }

    private void start()
    {
        moving = false;
        alive = true;
        tdead = 0;
        SetPosition(x0, y0);
        SetImage("rana-up", 0);
    }

    public override void OnUpdate(float dt)
    {
        if (!alive)
        {
            int t = DateTime.Now.Second;
            if (t - tdead > 3)
                start();
            return;
        }

        pixels = 36;

        float x = GetX();
        float y = GetY();

        if (!moving)
        {
            if (lge.KeyPressed(Keys.Up))
            {
                SetPosition(x, y - pixels);
                SetImage("rana-up", 1);
                moving = true;
            }
            else if (lge.KeyPressed(Keys.Down))
            {
                SetPosition(x, y + pixels);
                SetImage("rana-down", 1);
                moving = true;
            }
            else if (lge.KeyPressed(Keys.Left))
            {
                SetPosition(x - pixels, y);
                SetImage("rana-left", 1);
                moving = true;
            }
            else if (lge.KeyPressed(Keys.Right))
            {
                SetPosition(x + pixels, y);
                SetImage("rana-right", 1);
                moving = true;
            }
        }
        else if (!lge.KeyPressed(Keys.Up) &&
                 !lge.KeyPressed(Keys.Down) &&
                 !lge.KeyPressed(Keys.Left) &&
                 !lge.KeyPressed(Keys.Right))
        {
            NextImage();
            moving = false;
        }
    }

    public override void OnCollision(float dt, GameObject[] gobjs)
    {
        if (!alive)
            return;

        foreach (GameObject gobj in gobjs)
        {
            tag = gobj.GetTag();
            if (tag.Equals("agua") || tag.Equals("auto"))
            {
                SetImage("rana-dead");
                tdead = DateTime.Now.Second;
                alive = false;
                return;
            }
            if (tag.Equals("tronco"))
                return;
        }
    }

}
