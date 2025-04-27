using OpenTK.Graphics;

namespace Tareas_grafica;

public class Escenario
{
    public Dictionary<String, Objeto> Objetos { get; set; } = new Dictionary<string, Objeto>();
    public Vertice CentroEscenario { get; set; } = new Vertice();
    public Color4 ColorDeFondo { get; set; } = Color4.Black;

    public Escenario() { }
    public Escenario(Color4 colorDeFondo)
    {
        
        ColorDeFondo = colorDeFondo;
        CentroEscenario = new Vertice(0, 0, 0);

    }

    public void AgregarObjeto(String id, Objeto objeto)
    {
        Objetos[id] = objeto;
    }

    public void Dibujar()
    {
        foreach(var objeto in Objetos.Values)
            objeto.Dibujar();
    }

    public void Rotar(float anguloX, float anguloY, float anguloZ)
    {
        foreach (var obj in Objetos.Values)
            foreach (var parte in obj.Partes.Values)
                foreach (var cara in parte.Caras.Values)
                {
                    cara.SetCentro(CentroEscenario);
                    cara.Rotar(anguloX, anguloY, anguloZ);
                }
    }

    public void Trasladar(float deltaX, float deltaY, float deltaZ)
    {
        foreach (var obj in Objetos.Values)
            obj.Trasladar(deltaX, deltaY, deltaZ);
    }

    public void Escalar(float factor)
    {
        foreach (var obj in Objetos.Values)
            foreach (var parte in obj.Partes.Values)
                foreach (var cara in parte.Caras.Values)
                {
                    cara.SetCentro(CentroEscenario);
                    cara.Escalar(factor);
                }
    }

}
