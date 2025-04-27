using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Tareas_grafica.Serializacion;
using Tareas_grafica.Figuras;

namespace Tareas_grafica;

public class Game : GameWindow
{
    private VistaCamara camara = null!;
    private Escenario escenario;
    //private Eje eje { get; set; } = new Eje(0.5f, 0.05f);
    private MouseState _lastMouseState;
    string objetoSeleccionado = "u1";
    string parteSeleccionada = "Vertical1";

    public Game() : base(800, 600, GraphicsMode.Default, "Tareas")
    {
        this.escenario = new Escenario(Color4.Black);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        camara = new VistaCamara(Width, Height);
        GL.Enable(EnableCap.DepthTest);
        GL.ClearColor(escenario.ColorDeFondo);
        
        Serializador serializador = new Serializador();

        U u1 = new U(new Vertice(0, 0, 0), Color4.Red);
        U u2 = new U(new Vertice(1, -2, 1), Color4.Blue);
        Cubo cubo1 = new Cubo(new Vertice(-2, 0.5f, -1.5f), Color4.Green);
        Cruz cruz1 = new Cruz(new Vertice(2, 1, 0), Color4.Yellow);
        escenario.AgregarObjeto("u1", u1);
        escenario.AgregarObjeto("u2", u2);
        escenario.AgregarObjeto("cubo1", cubo1);
        escenario.AgregarObjeto("cruz1", cruz1);

        //serializador.Guardar(cubo1, "cubo1");

        //serializador.Guardar(escenario, "escenario3");
        //Objeto? cubo2 = serializador.Cargar<Objeto>("cubo1");
        //escenario = serializador.Cargar<Escenario>("escenario1");
        //escenario.AgregarObjeto("cubo2", cubo2);
        //serializador.Guardar(escenario, "escenario4");
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
        //eje.Dibujar();
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

        camara.ProcesarMouse(mouse, _lastMouseState, (float)e.Time);
        camara.ActualizarMatrices(Width, Height);
        _lastMouseState = mouse;

        // para aplicar transformaciones

        // transformaciones para el escenario

        if (keyboard.IsKeyDown(Key.P))
            escenario.Escalar(1.01f);
        if (keyboard.IsKeyDown(Key.O))
            escenario.Escalar(0.99f);

        if (keyboard.IsKeyDown(Key.Left))
            escenario.Rotar(0, -1, 0);
        if (keyboard.IsKeyDown(Key.Right))
            escenario.Rotar(0, 1, 0);
        if (keyboard.IsKeyDown(Key.Up))
            escenario.Rotar(-1, 0, 0);
        if (keyboard.IsKeyDown(Key.Down))
            escenario.Rotar(1, 0, 0);

        // transformaciones para los objetos

        
        if (keyboard.IsKeyDown(Key.Keypad1)) objetoSeleccionado = "u1";
        if (keyboard.IsKeyDown(Key.Keypad2)) objetoSeleccionado = "cubo1";
        if (keyboard.IsKeyDown(Key.Keypad3)) objetoSeleccionado = "cruz1";
        if (keyboard.IsKeyDown(Key.Keypad4)) objetoSeleccionado = "u2";
        
        if (escenario.Objetos.ContainsKey(objetoSeleccionado))
        {
            var obj = escenario.Objetos[objetoSeleccionado];

            // traslación objeto 
            if (keyboard.IsKeyDown(Key.A)) obj.Trasladar(0.1f, 0, 0);
            if (keyboard.IsKeyDown(Key.S)) obj.Trasladar(-0.1f, 0, 0);
            if (keyboard.IsKeyDown(Key.D)) obj.Trasladar(0, 0.1f, 0);
            if (keyboard.IsKeyDown(Key.F)) obj.Trasladar(0, -0.1f, 0);
            if (keyboard.IsKeyDown(Key.G)) obj.Trasladar(0, 0, 0.1f);
            if (keyboard.IsKeyDown(Key.H)) obj.Trasladar(0, 0, -0.1f);

            // rotación objeto
            if (keyboard.IsKeyDown(Key.E)) obj.Rotar(0, 15, 0); // horizontal derecha
            if (keyboard.IsKeyDown(Key.R)) obj.Rotar(0, -15f,0); // horizontal izquierda
            if (keyboard.IsKeyDown(Key.T)) obj.Rotar(15, 0, 0); // vertical arriba
            if (keyboard.IsKeyDown(Key.Y)) obj.Rotar(-15f, 0, 0); // vertical abajo
            if (keyboard.IsKeyDown(Key.U)) obj.Rotar(0, 0, 15); // z +
            if (keyboard.IsKeyDown(Key.I)) obj.Rotar(0, 0, -15f); // z -


            // escalado objeto
            if (keyboard.IsKeyDown(Key.J)) obj.Escalar(1.01f);
            if (keyboard.IsKeyDown(Key.K)) obj.Escalar(0.99f);
            
            
            
            if (keyboard.IsKeyDown(Key.Keypad7)) parteSeleccionada = "Vertical1";
            if (keyboard.IsKeyDown(Key.Keypad8)) parteSeleccionada = "Horizontal1";
            if (keyboard.IsKeyDown(Key.Keypad9)) parteSeleccionada = "Vertical2";

            // traslacion partes
             
            if (obj.Partes.ContainsKey(parteSeleccionada))
            {
                var part = obj.Partes[parteSeleccionada];

                // traslación parte 
                if (keyboard.IsKeyDown(Key.Number1)) part.Trasladar(0.1f, 0, 0);
                if (keyboard.IsKeyDown(Key.Number2)) part.Trasladar(-0.1f, 0, 0);
                if (keyboard.IsKeyDown(Key.Number3)) part.Trasladar(0, 0.1f, 0);
                if (keyboard.IsKeyDown(Key.Number4)) part.Trasladar(0, -0.1f, 0);
                if (keyboard.IsKeyDown(Key.Number5)) part.Trasladar(0, 0, 0.1f);
                if (keyboard.IsKeyDown(Key.Number6)) part.Trasladar(0, 0, -0.1f);

                // rotación parte
   
                if (keyboard.IsKeyDown(Key.Z)) part.Rotar(15, 0, 0); // horizontal derecha
                if (keyboard.IsKeyDown(Key.X)) part.Rotar(-15f, 0, 0); // horizontal izquierda
                if (keyboard.IsKeyDown(Key.C)) part.Rotar(0, 15, 0); // vertical arriba
                if (keyboard.IsKeyDown(Key.V)) part.Rotar(0, -15f, 0); // vertical abajo
                if (keyboard.IsKeyDown(Key.B)) part.Rotar(0, 0, 15); // z +
                if (keyboard.IsKeyDown(Key.N)) part.Rotar(0, 0, -15f); // z -

                // escalado parte
                if (keyboard.IsKeyDown(Key.Number8)) part.Escalar(1.01f);
                if (keyboard.IsKeyDown(Key.Number9)) part.Escalar(0.99f);
                        
             }
        }
    }
}
