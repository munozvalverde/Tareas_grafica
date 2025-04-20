using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareas_grafica.Figuras;
public class Cubo : Objeto
{
    public Cubo(Vertice posicion, Color4 color) : base([], posicion)
    {
        List<Vertice> cubo = [
            new Vertice(-0.5f, 0.5f, 0.5f),   // 0 - Frente arriba izquierda
            new Vertice(0.5f, 0.5f, 0.5f),    // 1 - Frente arriba derecha
            new Vertice(0.5f, -0.5f, 0.5f),   // 2 - Frente abajo derecha
            new Vertice(-0.5f, -0.5f, 0.5f),  // 3 - Frente abajo izquierda
            new Vertice(-0.5f, 0.5f, -0.5f),  // 4 - Atrás arriba izquierda
            new Vertice(0.5f, 0.5f, -0.5f),   // 5 - Atrás arriba derecha
            new Vertice(0.5f, -0.5f, -0.5f),  // 6 - Atrás abajo derecha
            new Vertice(-0.5f, -0.5f, -0.5f)  // 7 - Atrás abajo izquierda
        ];

        this.AgregarParte("Cubo", Utils.CrearBloque(cubo, color));
        this.Trasladar(posicion.X, posicion.Y, posicion.Z);
    }
}
