using OpenTK.Input;
using OpenTK;
namespace Tareas_grafica;

public class VistaCamara
{
    // Propiedades de la cámara
    public Vector3 Posicion { get; private set; }
    public Vector3 Objetivo { get; private set; }
    public Vector3 Arriba { get; private set; }
    public float Distancia { get; private set; }
    public float AnguloX { get; private set; }
    public float AnguloY { get; private set; }

    private Vector2 _lastMousePos;
    public float MouseSensitivity { get; set; } = 4;
    public float ScrollSensitivity { get; set; } = 1;

    // Matrices
    public Matrix4 Vista;
    public Matrix4 Proyeccion;

    public VistaCamara(float anchoVentana, float altoVentana)
    {
        Distancia = 8.0f;
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

    public void ProcesarMouse(MouseState mouse, MouseState lastMouse, float deltaTime)
    {
        // para rotar cuando el botón izquierdo está presionado
        if (mouse.IsButtonDown(MouseButton.Left))
        {
            // calcula el delta de movimiento (invertido)
            var deltaX = lastMouse.X - mouse.X;
            var deltaY = lastMouse.Y - mouse.Y;

            // aplica rotación con suavizado por deltaTime
            Rotar(deltaY * MouseSensitivity * deltaTime,
                  deltaX * MouseSensitivity * deltaTime);
        }

        // zoom con mouse wheel
        var scrollDelta = mouse.ScrollWheelValue - lastMouse.ScrollWheelValue;
        if (scrollDelta != 0)
        {
            AcercarAlejar(-scrollDelta * ScrollSensitivity);
        }
    }
}
