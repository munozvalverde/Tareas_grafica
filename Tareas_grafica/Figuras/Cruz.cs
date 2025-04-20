using OpenTK.Graphics;

namespace Tareas_grafica.Figuras;

public class Cruz : Objeto
{
    public Cruz(Vertice posicion, Color4 color) : base([], posicion)
    {
        List<Vertice> bloqueHorizontal = [
            new Vertice(-0.5f, 0.1f, 0.1f),
            new Vertice(0.5f, 0.1f, 0.1f),
            new Vertice(0.5f, -0.1f, 0.1f),
            new Vertice(-0.5f, -0.1f, 0.1f),
            new Vertice(-0.5f, 0.1f, -0.1f),
            new Vertice(0.5f, 0.1f, -0.1f),
            new Vertice(0.5f, -0.1f, -0.1f),
            new Vertice(-0.5f, -0.1f, -0.1f)
        ];

        this.AgregarParte("bloqueHorizontal", Utils.CrearBloque(bloqueHorizontal, color));

        List<Vertice> bloqueVertical = [
            new Vertice(-0.1f, 0.5f, 0.1f),
            new Vertice(0.1f, 0.5f, 0.1f),
            new Vertice(0.1f, -0.5f, 0.1f),
            new Vertice(-0.1f, -0.5f, 0.1f),
            new Vertice(-0.1f, 0.5f, -0.1f),
            new Vertice(0.1f, 0.5f, -0.1f),
            new Vertice(0.1f, -0.5f, -0.1f),
            new Vertice(-0.1f, -0.5f, -0.1f)
        ];

        this.AgregarParte("bloqueVertical", Utils.CrearBloque(bloqueVertical, color));

        this.Trasladar(posicion.X, posicion.Y, posicion.Z);



    }
}
