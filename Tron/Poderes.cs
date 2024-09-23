using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    public class Poder
    {
        public string Nombre { get; set; }
        public int Duracion { get; set; }  // Duración del poder en ticks de Timer
        public int X { get; set; }         // Posición X en el mapa
        public int Y { get; set; }         // Posición Y en el mapa

        public Poder(string nombre, int duracion, int x, int y)
        {
            Nombre = nombre;
            Duracion = duracion;
            X = x;
            Y = y;
        }

        public void AplicarEfectoTemporal(Moto moto)
        {
            switch (Nombre)
            {
                case "Escudo":
                    moto.ActivarEscudo(Duracion);
                    break;
                case "HiperVelocidad":
                    moto.ActivarHiperVel(Duracion);
                    break;
            }
        }
    }
}