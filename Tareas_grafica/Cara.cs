using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
namespace Tareas_grafica;

public class Cara
{
    public List<Vertice> Vertices { get; private set; }
    private Color4 color;

    public Cara(List<Vertice> vertices, Color4 color = default)
    {
        this.Vertices = vertices;
        this.color = color == default ? Color4.White : color;
    }

    public void Dibujar()
    {
        GL.Begin(PrimitiveType.Polygon);
        GL.Color4(this.color);

        foreach (var vertice in this.Vertices)
            GL.Vertex3(vertice.X, vertice.Y, vertice.Z);

        GL.End();
    }
}
