using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Tareas_grafica;

public class Game : GameWindow
{
    private Camara camara;
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

        Serializador ser = new Serializador();

        U u1 = new U(new Vertice(0,0,0), Color4.Red);
        // U u2 = new U(new Vertice(-8, -3, 0), Color4.Blue);
        Cubo cubo1 = new Cubo(new Vertice(-8, 5, 0), Color4.ForestGreen);
        escenario.AgregarObjeto(u1);
        //escenario.AgregarObjeto(u2);
        escenario.AgregarObjeto(cubo1);

        ser.Serializar(u1, "U_vertices");
 
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
        camara.ProcesarInput(Keyboard.GetState());
        camara.ProcesarMouse(mouse, _lastMouseState, (float)e.Time);
        camara.ActualizarMatrices(Width, Height);
        _lastMouseState = mouse;
    }

}
