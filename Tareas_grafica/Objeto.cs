using OpenTK.Graphics.OpenGL;
namespace Tareas_grafica;

public class Objeto
{
    public List<Parte> Partes { get; private set; }

    public Vertice Posicion { get;  set; }

    public Objeto(List<Parte> partes, Vertice posicion)
    {
        Partes = partes;
        Posicion = posicion;
    }

    public void Dibujar()
    {
        GL.PushMatrix();
        GL.Translate(Posicion.X, Posicion.Y, Posicion.Z);
        foreach (var parte in Partes)
        {
            parte.Dibujar();
        }
        GL.PopMatrix();
    }
}
