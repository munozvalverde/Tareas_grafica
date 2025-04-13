using OpenTK.Graphics;
namespace Tareas_grafica;

public class U : Objeto
{
    /*public U(Vertice posicion, Color4 color) : base([], posicion)
    {
        List<Vertice> columnaIzquierda = [
            new Vertice(-0.5f, 0.6f, 0.0f),   // Arriba-frontal
            new Vertice(-0.3f, 0.6f, 0.0f),   // Arriba-trasero
            new Vertice(-0.3f, -0.3f, 0.0f),  // Abajo-trasero
            new Vertice(-0.5f, -0.3f, 0.0f),  // Abajo-frontal
            new Vertice(-0.5f, 0.6f, -0.2f),  // Arriba-frontal (profundidad)
            new Vertice(-0.3f, 0.6f, -0.2f),  // Arriba-trasero (profundidad)
            new Vertice(-0.3f, -0.3f, -0.2f), // Abajo-trasero (profundidad)
            new Vertice(-0.5f, -0.3f, -0.2f)  // Abajo-frontal (profundidad)
];

        this.Partes.Add(Utils.CrearBloque3D(columnaIzquierda, color));

        List<Vertice> columnaDerecha = [
            new Vertice(0.3f, 0.6f, 0.0f),    // Arriba-frontal
            new Vertice(0.5f, 0.6f, 0.0f),    // Arriba-trasero
            new Vertice(0.5f, -0.3f, 0.0f),   // Abajo-trasero
            new Vertice(0.3f, -0.3f, 0.0f),   // Abajo-frontal
            new Vertice(0.3f, 0.6f, -0.2f),  // Arriba-frontal (profundidad)
            new Vertice(0.5f, 0.6f, -0.2f),   // Arriba-trasero (profundidad)
            new Vertice(0.5f, -0.3f, -0.2f),  // Abajo-trasero (profundidad)
            new Vertice(0.3f, -0.3f, -0.2f)   // Abajo-frontal (profundidad)
        ];

        this.Partes.Add(Utils.CrearBloque3D(columnaDerecha, color));

        List<Vertice> base = [
            new Vertice(-0.3f, -0.1f, 0.0f),  // Izquierda-frontal
            new Vertice(0.3f, -0.1f, 0.0f),   // Derecha-frontal
            new Vertice(0.3f, -0.3f, 0.0f),   // Derecha-abajo-frontal
            new Vertice(-0.3f, -0.3f, 0.0f),  // Izquierda-abajo-frontal
            new Vertice(-0.3f, -0.1f, -0.2f), // Izquierda-trasero
            new Vertice(0.3f, -0.1f, -0.2f),  // Derecha-trasero
            new Vertice(0.3f, -0.3f, -0.2f),  // Derecha-abajo-trasero
            new Vertice(-0.3f, -0.3f, -0.2f)  // Izquierda-abajo-trasero
        ];

        this.Partes.Add(Utils.CrearBloque3D(base, color));
    }*/
    

    public U(Vertice posicion, Color4 color) : base([], posicion)
    {
        string rutaArchivo = "U_vertices.txt";
        var bloques = Utils.CargarVerticesDesdeArchivo(rutaArchivo);

        foreach (var bloque in bloques)
        {
            this.Partes.Add(Utils.CrearBloque3D(bloque, color));
        }
    }



}

