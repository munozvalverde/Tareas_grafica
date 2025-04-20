using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareas_grafica;

public class Escenario
{
    public Dictionary<String, Objeto> Objetos { get; set; } = new Dictionary<string, Objeto>();
    public Vertice Centro { get; set; } = new Vertice();
    public Color4 ColorDeFondo { get; set; } = Color4.Black;

    public Escenario() { }
    public Escenario(Color4 colorDeFondo)
    {
        
        ColorDeFondo = colorDeFondo;
        Centro = new Vertice(0, 0, 0);

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

    public void Rotar(float angX, float angY, float angZ)
    {
        foreach (var obj in Objetos.Values)
            foreach (var parte in obj.Partes.Values)
                foreach (var cara in parte.Caras.Values)
                {
                    cara.SetCentro(Centro);
                    cara.Rotar(angX, angY, angZ);
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
                    cara.SetCentro(Centro);
                    cara.Escalar(factor);
                }
    }

}
