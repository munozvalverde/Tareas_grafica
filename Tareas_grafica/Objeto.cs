using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
namespace Tareas_grafica;

public class Objeto
{
    public Dictionary<string, Parte> Partes { get; set; } = new Dictionary<string, Parte>();
    public Vertice Posicion { get; set; } = new Vertice();

    public Objeto(Dictionary<string, Parte> partes, Vertice posicion)
    {
        Partes = partes;
        Posicion = posicion;
    }

    public Objeto() { }

    public void AgregarParte(string id, Parte parte)
    {
        Partes[id] = parte;
    }

    //public void EliminarParte(string id)
    //{
    //    if (Partes.ContainsKey(id))
    //        Partes.Remove(id);
    //}

    public Parte? ObtenerParte(string id)
    {
        if (Partes.ContainsKey(id))
            return Partes[id];
        else
            return null;
    }

    public void Dibujar()
    {
        foreach (var parte in Partes.Values)
        {
            parte.Dibujar();
        }

    }

    public void Rotar(float anguloX, float anguloY, float anguloZ)
    {
        Vertice centroObjeto = CalcularCentroObjeto();
        foreach (var parte in Partes.Values)
            foreach (var cara in parte.Caras.Values)
            {
                cara.SetCentro(centroObjeto);
                cara.Rotar(anguloX, anguloY, anguloZ);
            }
    }
    public void Trasladar(float deltaX, float deltaY, float deltaZ)
    {
        foreach (var parte in Partes.Values)
            parte.Trasladar(deltaX, deltaY, deltaZ);
    }

    private Vertice CalcularCentroObjeto()
    {
        var vertices = Partes.Values.SelectMany(p => p.Caras.Values)
                                    .SelectMany(c => c.Vertices.Values).ToList();
        return new Vertice(vertices.Average(v => v.X), vertices.Average(v => v.Y), vertices.Average(v => v.Z));
    }

    public void Escalar(float factor)
    {
        Vertice centroObjeto = CalcularCentroObjeto();
        foreach (var parte in Partes.Values)
            foreach (var cara in parte.Caras.Values)
            {
                cara.SetCentro(centroObjeto);
                cara.Escalar(factor);
            }
    }
}
