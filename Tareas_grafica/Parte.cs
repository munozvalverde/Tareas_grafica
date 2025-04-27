
namespace Tareas_grafica;

public class Parte
{
    public Dictionary<String, Cara> Caras { get; set; } = new Dictionary<string, Cara>();
    public Vertice CentroParte { get; set; } = new Vertice();
    public Parte(Dictionary<String, Cara> caras)
    {
        Caras = caras;
        CentroParte = CalcularCentroParte();
    }

    public Parte() { }

    public void AgregarCara(String id, Cara cara)
    {
        Caras[id] = cara;
    }

    public void Dibujar()
    {
        foreach (Cara cara in Caras.Values)
            cara.Dibujar();
    }

    public void Rotar(float anguloX, float anguloY, float anguloZ)
    {
        Vertice centro = CalcularCentroParte();
        foreach (var cara in Caras.Values)
        {
            cara.SetCentro(centro);
            cara.Rotar(anguloX, anguloY, anguloZ);
        }
    }

    private Vertice CalcularCentroParte()
    {
        var vertices = Caras.Values.SelectMany(cara => cara.Vertices.Values).ToList();
        return new Vertice(vertices.Average(v => v.X), vertices.Average(v => v.Y), vertices.Average(v => v.Z));
    }


    public void Trasladar(float deltaX, float deltaY, float deltaZ)
    {
        foreach (var cara in Caras.Values)
            cara.Trasladar(deltaX, deltaY, deltaZ);
    }

    public void Escalar(float factor)
    {
        Vertice centro = CalcularCentroParte();
        foreach (var cara in Caras.Values)
        {
            cara.SetCentro(centro);
            cara.Escalar(factor);
        }

    }
}
