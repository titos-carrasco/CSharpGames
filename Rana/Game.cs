using System;
using System.Drawing;
using System.Windows.Forms;
using rcr.lge;

class Game : IEvents
{
    private readonly LittleGameEngine lge;

    public Game(String resourceDir)
    {
        Size winSize = new Size(544, 416);

        lge = new LittleGameEngine(winSize, "La Rana", Color.Black);
        lge.SetOnMainUpdate(this);
        lge.ShowColliders(Color.Red);

        // cargamos los recursos que usaremos
        lge.LoadImage("fondo", resourceDir + "/world.png", false, false);
        lge.LoadImage("rana-idle", resourceDir + "/rana-idle.png", false, false);
        lge.LoadImage("rana-dead", resourceDir + "/rana-dead.png", false, false);
        lge.LoadImage("rana-up", resourceDir + "/rana-up*.png", false, false);
        lge.LoadImage("rana-down", resourceDir + "/rana-up*.png", 1, false, true);
        lge.LoadImage("rana-left", resourceDir + "/rana-left*.png", false, false);
        lge.LoadImage("rana-right", resourceDir + "/rana-left*.png", 1, true, false);
        lge.LoadImage("auto-azul", resourceDir + "/auto_azul.png", false, false);
        lge.LoadImage("auto-rojo", resourceDir + "/auto_rojo.png", 1 , true, false);
        lge.LoadImage("tortuga", resourceDir + "/tortuga_*.png", false, false);
        lge.LoadImage("tronco-largo", resourceDir + "/tronco_largo.png", false, false);

        // el fondo
        Sprite fondo = new Sprite("fondo", new PointF(0, 0));
        lge.AddGObject(fondo, 0);

        // el hogar
        for(int i=0; i<5; i++){
            GameObject home = new GameObject(new PointF(42 + i * 108, 22), new Size(30, 30));
            home.EnableCollider(true);
            home.SetTag("home-");
            lge.AddGObject(home, 1);
        }

        // las tortugas
        Tortuga tortuga = new Tortuga("tortuga", 'R', 10, 60, 80);
        lge.AddGObject(tortuga, 1);

        // los troncos
        Tronco tronco = new Tronco("tronco-largo", 'R', 80, 100, 100);
        lge.AddGObject(tronco, 1);

        tronco = new Tronco("tronco-largo", 'R', 200, 172);
        lge.AddGObject(tronco, 1);

        // el agua
        GameObject agua = new GameObject( new PointF(0, 53), new SizeF(544, 144));
        agua.EnableCollider(true);
        agua.SetTag("agua");
        lge.AddGObject(agua, 1);

        // los vehiculos
        Vehiculo car = new Vehiculo("auto-azul", 'R', 10, 310);
        lge.AddGObject(car, 1);
        car = new Vehiculo("auto-rojo", 'L', 190, 238, 100);
        lge.AddGObject(car, 1);

        // la rana
        Rana rana = new Rana(255, 380);
        lge.AddGObject(rana, 1);
    }

    public void OnMainUpdate(float dt)
    {
        // abortamos con la tecla Escape
        if (lge.KeyPressed(Keys.Escape))
            lge.Quit();
    }

    // main loop
    public void Run(int fps)
    {
        lge.Run(fps);
    }

    // show time
    public static void Main(String[] args)
    {
        Game game = new Game(@"C:\Users\rcarrascor\Documents\MyProjects\CSharpGames\Rana\resources");
        game.Run(60);
        Console.WriteLine("Eso es todo!!!");
    }
}
