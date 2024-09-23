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
        public int Efecto { get; set; }  // Valor del efecto 
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
                    moto.Combustible = Math.Min(100, moto.Combustible + Efecto); // Adiciona al combustible existente 
                    break;
                case "Crecimiento":
                    for (int i = 0; i < Efecto; i++)
                    {
                        moto.Estela.AgregarNodo(moto.PosX, moto.PosY); // Agrega nodo a la estela dependiendo del valor ingresado
                    }
                    break;
                case "Mina":
                    moto.Morir(); // Muerte por mina
                    break;
            }
        }
    }
}
