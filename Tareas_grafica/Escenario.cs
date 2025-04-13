using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareas_grafica;

public class Escenario
{
    public List<Objeto> objetos;

    public Color4 ColorDeFondo { get; set; }
    
    public Escenario(Color4 colorDeFondo)
    {
        this.objetos = [];
        ColorDeFondo = colorDeFondo;
    }

    public void AgregarObjeto(Objeto objeto)
    {
        this.objetos.Add(objeto);
    }

    public void Dibujar()
    {
        foreach(var objeto in this.objetos)
            objeto.Dibujar();
    }
}
