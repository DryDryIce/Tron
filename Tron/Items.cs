using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    public class Item
    {
        public string Nombre { get; set; }
        public int Efecto { get; set; }  // Valor del efecto (por ejemplo, cantidad de combustible o crecimiento de la estela)
        public int X { get; set; }
        public int Y { get; set; }

        public Item(string nombre, int efecto, int x, int y)
        {
            Nombre = nombre;
            Efecto = efecto;
            X = x;
            Y = y;
        }

        public void AplicarEfecto(Moto moto)
        {
            switch (Nombre)
            {
                case "Combustible":
                    moto.Combustible = Math.Min(100, moto.Combustible + Efecto);
                    break;
                case "Crecimiento":
                    for (int i = 0; i < Efecto; i++)
                    {
                        moto.Estela.AgregarNodo(moto.PosX, moto.PosY);
                    }
                    break;
                    // Agregar más ítems según sea necesario
            }
        }
    }

    public class ColaItems
    {
        private Queue<Item> cola;

        public ColaItems()
        {
            cola = new Queue<Item>();
        }

        public void AgregarItem(Item item)
        {
            cola.Enqueue(item);
        }

        public Item UsarItem()
        {
            return cola.Count > 0 ? cola.Dequeue() : null;
        }
    }

}
