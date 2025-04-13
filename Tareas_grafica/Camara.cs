using OpenTK.Input;
using OpenTK;
namespace Tareas_grafica;

public class Camara
{
    // Propiedades de la cámara
    public Vector3 Posicion { get; private set; }
    public Vector3 Objetivo { get; private set; }
    public Vector3 Arriba { get; private set; }
    public float Distancia { get; private set; }
    public float AnguloX { get; private set; }
    public float AnguloY { get; private set; }

    private Vector2 _lastMousePos;
    public float MouseSensitivity { get; set; } = 5;
    public float ScrollSensitivity { get; set; } = 1;

    // Matrices
    public Matrix4 Vista;
    public Matrix4 Proyeccion;

    public Camara(float anchoVentana, float altoVentana)
    {
        Distancia = 25.0f;
        AnguloX = 0.0f;
        AnguloY = 0.0f;
        Objetivo = Vector3.Zero;
        Arriba = Vector3.UnitY;

        ActualizarMatrices(anchoVentana, altoVentana);
    }

    public void ActualizarMatrices(float anchoVentana, float altoVentana)
    {
        // Actualizar posición de la cámara (esférica a cartesiana)
        Posicion = new Vector3(
            (float)(Distancia * Math.Sin(MathHelper.DegreesToRadians(AnguloY)) *
                    Math.Cos(MathHelper.DegreesToRadians(AnguloX))),
            (float)(Distancia * Math.Sin(MathHelper.DegreesToRadians(AnguloX))),
            (float)(Distancia * Math.Cos(MathHelper.DegreesToRadians(AnguloY)) *
                    Math.Cos(MathHelper.DegreesToRadians(AnguloX)))
        );

        // Matriz de vista
        Vista = Matrix4.LookAt(Posicion, Objetivo, Arriba);

        // Matriz de proyección
        float aspectRatio = anchoVentana / altoVentana;
        Proyeccion = Matrix4.CreatePerspectiveFieldOfView(
            MathHelper.PiOver4, aspectRatio, 0.1f, 100.0f);
    }

    public void Rotar(float deltaAnguloX, float deltaAnguloY)
    {
        AnguloX += deltaAnguloX;
        AnguloY += deltaAnguloY;
    }

    public void AcercarAlejar(float deltaDistancia)
    {
        Distancia += deltaDistancia;
    }

    public void ProcesarInput(KeyboardState keyboard)
    {
        if (keyboard[Key.Up]) Rotar(1.0f, 0);
        if (keyboard[Key.Down]) Rotar(-1.0f, 0);
        if (keyboard[Key.Right]) Rotar(0, -1.0f);
        if (keyboard[Key.Left]) Rotar(0, 1.0f);
        if (keyboard[Key.F]) AcercarAlejar(-0.1f);
        if (keyboard[Key.B]) AcercarAlejar(0.1f);
    }

    public void ProcesarMouse(MouseState mouse, MouseState lastMouse, float deltaTime)
    {
        // Rotación solo cuando el botón izquierdo está presionado
        if (mouse.IsButtonDown(MouseButton.Left))
        {
            // Calcula el delta de movimiento (invertido para dirección correcta)
            var deltaX = lastMouse.X - mouse.X;  // Invertido para dirección natural
            var deltaY = lastMouse.Y - mouse.Y;  // Invertido para dirección natural

            // Aplica rotación con suavizado por deltaTime
            Rotar(deltaY * MouseSensitivity * deltaTime,
                  deltaX * MouseSensitivity * deltaTime);
        }

        // Zoom con rueda del mouse (más sensible)
        var scrollDelta = mouse.ScrollWheelValue - lastMouse.ScrollWheelValue;
        if (scrollDelta != 0)
        {
            AcercarAlejar(-scrollDelta * ScrollSensitivity);
        }
    }
}
