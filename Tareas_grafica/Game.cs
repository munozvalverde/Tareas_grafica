using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Tareas_grafica.Serializacion;
using Tareas_grafica.Figuras;

namespace Tareas_grafica;

public class Game : GameWindow
{
    private Camara camara = null!;
    private Escenario escenario;
    private MouseState _lastMouseState;

    public Game() : base(800, 600, GraphicsMode.Default, "Tareas")
    {
        this.escenario = new Escenario(Color4.Black);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        camara = new Camara(Width, Height);
        GL.Enable(EnableCap.DepthTest);
        GL.ClearColor(escenario.ColorDeFondo);

        Serializador serializador = new Serializador();

        U u1 = new U(new Vertice(0,0,0), Color4.Red);
        U u2 = new U(new Vertice(1, -2, 1), Color4.Blue);
        Cubo cubo1 = new Cubo(new Vertice(-2, 0.5f, -1.5f), Color4.ForestGreen);
        Cruz cruz1 = new Cruz(new Vertice(2,1,0), Color4.Yellow);
        escenario.AgregarObjeto("u1", u1);
        escenario.AgregarObjeto("u2",u2);
        escenario.AgregarObjeto("cubo1", cubo1);
        escenario.AgregarObjeto("cruz1", cruz1);

        //serializador.Guardar(escenario, "escenario3");
        //Escenario? escenario1 = serializador.Cargar<Escenario>("escenario1");
        //escenario = escenario1 ?? throw new InvalidOperationException("Error al cargar el archivo json");
        //escenario.Rotar(45, 0, 0);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadMatrix(ref camara.Proyeccion);
        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadMatrix(ref camara.Vista);
        escenario.Dibujar();
        //escenario.Objetos["u1"].Partes["Base"].Rotar(1, 0, 0);
        //escenario.Objetos["u1"].Partes["Bloque_Derecho"].Rotar(0, 1, 0);
        //escenario.Objetos["u1"].Partes["Bloque_Izquierdo"].Rotar(0, 1, 0);
        //escenario.Objetos["u2"].Rotar(1, 0, 0);
        
        SwapBuffers();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, Width, Height);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);
        var mouse = Mouse.GetState();
        var keyboard = Keyboard.GetState();

        //camara.ProcesarInput(Keyboard.GetState());
        camara.ProcesarMouse(mouse, _lastMouseState, (float)e.Time);
        camara.ActualizarMatrices(Width, Height);
        _lastMouseState = mouse;

        // para aplicar transformaciones a los objetos

        if (keyboard.IsKeyDown(Key.P))
            escenario.Rotar(45, 0, 0);

        if (keyboard.IsKeyDown(Key.Left))
            escenario.Rotar(0, -1, 0);
        if (keyboard.IsKeyDown(Key.Right))
            escenario.Rotar(0, 1, 0);
        if (keyboard.IsKeyDown(Key.Up))
            escenario.Rotar(-1, 0, 0);
        if (keyboard.IsKeyDown(Key.Down))
            escenario.Rotar(1, 0, 0);

        if (keyboard.IsKeyDown(Key.S))
            escenario.Objetos["u1"].Trasladar(0, -0.1f, 0);
        if (keyboard.IsKeyDown(Key.A))
            escenario.Objetos["u1"].Trasladar(0, 0.1f, 0);
        if (keyboard.IsKeyDown(Key.D))
            escenario.Objetos["u1"].Trasladar(-0.1f, 0, 0);
        if (keyboard.IsKeyDown(Key.F))
            escenario.Objetos["u1"].Trasladar(0.1f, 0, 0);

        if (keyboard.IsKeyDown(Key.R))
            escenario.Objetos["cubo1"].Rotar(0, 1, 0);
                
        if (keyboard.IsKeyDown(Key.N))
            escenario.Objetos["u2"].Escalar(1.01f);
        if (keyboard.IsKeyDown(Key.M))
            escenario.Objetos["u2"].Escalar(0.99f);

        if (keyboard.IsKeyDown(Key.K))
            escenario.Objetos["cruz1"].Trasladar(0, 0, 0.1f);
        if (keyboard.IsKeyDown(Key.L))
            escenario.Objetos["cruz1"].Trasladar(0, 0, -0.1f);
    }
}
