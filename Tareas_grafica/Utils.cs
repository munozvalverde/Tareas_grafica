using OpenTK.Graphics;
namespace Tareas_grafica;

public static class Utils
{
    public static Parte CrearBloque3D(List<Vertice> vertices, Color4 color)
    {
        if (vertices.Count != 8)
            throw new ArgumentException("Se requieren 8 vértices para un bloque.");

        var caras = new List<Cara>
        {
            // Cara frontal
            new([vertices[0], vertices[1], vertices[2], vertices[3]], color),
            // Cara trasera
            new([vertices[4], vertices[5], vertices[6], vertices[7]], color),
            // Cara lateral izquierda
            new([vertices[0], vertices[4], vertices[7], vertices[3]], color),
            // Cara lateral derecha
            new([vertices[1], vertices[5], vertices[6], vertices[2]], color),
            // Cara superior
            new([vertices[0], vertices[4], vertices[5], vertices[1]], color),
            // Cara inferior
            new([vertices[3], vertices[7], vertices[6], vertices[2]], color)
        };
        return new Parte(caras);
    }

    public static List<List<Vertice>> CargarVerticesDesdeArchivo(string ruta)
    {
        var resultado = new List<List<Vertice>>();
        var bloqueActual = new List<Vertice>();

        foreach (var linea in File.ReadLines(ruta))
        {
            var lineaLimpia = linea.Trim();
            if (string.IsNullOrEmpty(lineaLimpia) || lineaLimpia.StartsWith("#"))
            {
                if (bloqueActual.Count > 0)
                {
                    resultado.Add(new List<Vertice>(bloqueActual));
                    bloqueActual.Clear();
                }
                continue;
            }

            var partes = lineaLimpia.Split(' ');
            if (partes.Length == 3 &&
                float.TryParse(partes[0], out float x) &&
                float.TryParse(partes[1], out float y) &&
                float.TryParse(partes[2], out float z))
            {
                bloqueActual.Add(new Vertice(x, y, z));
            }
        }

        if (bloqueActual.Count > 0)
        {
            resultado.Add(bloqueActual);
        }

        return resultado;
    }



}
