using OpenTK.Graphics;
namespace Tareas_grafica;

public class Cubo : Objeto
{
       public Cubo(Vertice posicion, Color4 color) : base([], posicion)
    {
        string rutaArchivo = "Cubo_vertices.txt";
        var bloques = Utils.CargarVerticesDesdeArchivo(rutaArchivo);

        foreach (var bloque in bloques)
        {
            this.Partes.Add(Utils.CrearBloque3D(bloque, color));
        }
    }



}