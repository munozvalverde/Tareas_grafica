using OpenTK.Graphics;
namespace Tareas_grafica;

public static class Auxiliar
{
    public static Parte CrearBloque(List<Vertice> vertices, Color4 color)
    {
        if (vertices.Count != 8)
            throw new ArgumentException("Son necesarios 8 vértices");

        var caras = new Dictionary<string, Cara>
           {
               // Cara frontal
               { "frontside", new Cara(new Dictionary<string, Vertice>
               {
                   { "v1", vertices[0] },
                   { "v2", vertices[1] },
                   { "v3", vertices[2] },
                   { "v4", vertices[3] }
               }, Color4.Fuchsia) },
               // Cara trasera
               { "backside", new Cara(new Dictionary<string, Vertice>
               {
                   { "v1", vertices[4] },
                   { "v2", vertices[5] },
                   { "v3", vertices[6] },
                   { "v4", vertices[7] }
               }, Color4.Purple) },

               // Cara lateral izquierda
               { "leftside", new Cara(new Dictionary<string, Vertice>
               {
                   { "v1", vertices[0] },
                   { "v2", vertices[4] },
                   { "v3", vertices[7] },
                   { "v4", vertices[3] }
               }, color) },

               // Cara lateral derecha
               { "rightside", new Cara(new Dictionary<string, Vertice>
               {
                   { "v1", vertices[1] },
                   { "v2", vertices[5] },
                   { "v3", vertices[6] },
                   { "v4", vertices[2] }
               }, color) },

               // Cara superior
               { "upside", new Cara(new Dictionary<string, Vertice>
               {
                   { "v1", vertices[0] },
                   { "v2", vertices[4] },
                   { "v3", vertices[5] },
                   { "v4", vertices[1] }
               }, color) },

               // Cara inferior
               { "downside", new Cara(new Dictionary<string, Vertice>
               {
                   { "v1", vertices[3] },
                   { "v2", vertices[7] },
                   { "v3", vertices[6] },
                   { "v4", vertices[2] }
               }, color) }
           };
        return new Parte { Caras = caras };
    }

}
