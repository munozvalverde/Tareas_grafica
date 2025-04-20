using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
namespace Tareas_grafica;

public class Cara
{
    public Dictionary<String, Vertice> Vertices { get; set; } = new Dictionary<string, Vertice>();
    public Color4 Color { get; set; }
    public Vertice Centro { get; set; } = new Vertice();
    public Cara(Dictionary<String, Vertice> vertices, Color4 color = default)
    {
        this.Vertices = vertices;
        this.Color = color == default ? Color4.White : color;
    }

    public Cara() { }

    public void AgregarVertice(String id, Vertice vertice)
    {
        Vertices[id] = vertice;
    }
    public void Dibujar()
    {
        GL.Begin(PrimitiveType.Quads);
        GL.Color4(this.Color);
        foreach (var vertice in this.Vertices.Values)
            GL.Vertex3(vertice.X, vertice.Y, vertice.Z);

        GL.End();
    }

    public void SetCentro(Vertice centro)
    {
        Centro = centro;
    }

    public void Trasladar(float deltaX, float deltaY, float deltaZ)
    {
        // crear la matriz de traslación
        Matrix4 traslacion = Matrix4.CreateTranslation(deltaX, deltaY, deltaZ);

        foreach (var key in Vertices.Keys.ToList())
        {
            var v = Vertices[key];
            var v4 = new Vector4(v.X, v.Y, v.Z, 1); // Convertir a Vector4 para la transformación
            var result = Vector4.Transform(v4, traslacion); // Aplicar la matriz de traslación
            Vertices[key] = new Vertice(result.X, result.Y, result.Z); // Actualizar el vértice
        }
        CalcularCentro();
    }

    public void Escalar(float factor)
    {
        // 1. crear matriz de transformación compuesta
        Vector3 centro = new Vector3(Centro.X, Centro.Y, Centro.Z); // Convertir Centro a Vector3
        Matrix4 transformacion =
            Matrix4.CreateTranslation(-centro) *  // Paso 1: Mover al origen
            Matrix4.CreateScale(factor) *         // Paso 2: Aplicar escalado
            Matrix4.CreateTranslation(centro);    // Paso 3: Volver a posición

        // 2. aplicar a todos los vértices
        foreach (var key in Vertices.Keys.ToList())
        {
            Vertice v = Vertices[key];
            Vector4 verticeTransformado = Vector4.Transform(
                new Vector4(v.X, v.Y, v.Z, 1.0f),
                transformacion
            );

            Vertices[key] = new Vertice(
                verticeTransformado.X,
                verticeTransformado.Y,
                verticeTransformado.Z
            );
        }
    }

    public void Rotar(float angX, float angY, float angZ)
    {
        Vector3 centroVector = new Vector3(Centro.X, Centro.Y, Centro.Z); // Convertir Centro a Vector3

        Matrix4 rotacion =
            Matrix4.CreateTranslation(-centroVector) *
            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(angZ)) *
            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angY)) *
            Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angX)) *
            Matrix4.CreateTranslation(centroVector);

        foreach (var key in Vertices.Keys.ToList())
        {
            var v = Vertices[key];
            var v4 = new Vector4(v.X, v.Y, v.Z, 1);
            var result = Vector4.Transform(v4, rotacion);
            Vertices[key] = new Vertice(result.X, result.Y, result.Z);
        }
    }

    public void CalcularCentro()
    {
        if (Vertices.Count == 0)
            Centro = new Vertice(0, 0, 0);

        float minX = float.MaxValue, minY = float.MaxValue, minZ = float.MaxValue;
        float maxX = float.MinValue, maxY = float.MinValue, maxZ = float.MinValue;

        foreach (var punto in Vertices.Values)
        {
            if (punto.X < minX) minX = punto.X;
            if (punto.X > maxX) maxX = punto.X;

            if (punto.Y < minY) minY = punto.Y;
            if (punto.Y > maxY) maxY = punto.Y;

            if (punto.Z < minZ) minZ = punto.Z;
            if (punto.Z > maxZ) maxZ = punto.Z;
        }

        float centroX = (minX + maxX) / 2;
        float centroY = (minY + maxY) / 2;
        float centroZ = (minZ + maxZ) / 2;

        Centro = new Vertice(centroX, centroY, centroZ);
    }
}