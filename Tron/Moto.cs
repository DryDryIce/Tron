using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    public enum Direccion
    {
        Arriba,
        Abajo,
        Izquierda,
        Derecha
    }

    public class Moto
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int Velocidad { get; set; }  // Ajustado para permitir cambios temporales
        public int Combustible { get; set; }
        public bool Viva { get; private set; }
        public Direccion DireccionActual { get; set; }
        public ListaEstela Estela { get; private set; }
        public Queue<Item> Items { get; private set; }
        public Stack<Poder> Poderes { get; private set; }
        private bool EscudoActivo;

        public Moto(int xInicial, int yInicial)
        {
            PosX = xInicial;
            PosY = yInicial;
            Velocidad = new Random().Next(1, 11);
            Combustible = 100;
            DireccionActual = Direccion.Derecha;
            Estela = new ListaEstela();
            Items = new Queue<Item>();
            Poderes = new Stack<Poder>();
            EscudoActivo = false;
            Viva = true;  // Inicialmente la moto está viva

            // Estela inicial
            Estela.AgregarNodo(PosX - 1, PosY);
            Estela.AgregarNodo(PosX - 2, PosY);
        }

        public void UsarItem()
        {
            if (Items.Count > 0)
            {
                Item item = Items.Dequeue();
                item.AplicarEfecto(this);
            }
        }

        public void UsarPoder()
        {
            if (Poderes.Count > 0)
            {
                Poder poder = Poderes.Pop();
                poder.AplicarEfectoTemporal(this);
            }
        }

        public void ActivarEscudo(int duracion)
        {
            EscudoActivo = true;
            // Aquí deberías iniciar un temporizador o lógica para desactivar el escudo después de la duración
        }
        public void MoverMoto(Mapa mapa, int anchoPictureBox, int altoPictureBox)
        {
            if (!Viva || Combustible <= 0)
                return;

            int nuevaPosX = PosX;
            int nuevaPosY = PosY;

            switch (DireccionActual)
            {
                case Direccion.Arriba:
                    nuevaPosY -= 1;
                    break;
                case Direccion.Abajo:
                    nuevaPosY += 1;
                    break;
                case Direccion.Izquierda:
                    nuevaPosX -= 1;
                    break;
                case Direccion.Derecha:
                    nuevaPosX += 1;
                    break;
            }

            // Verificar si la nueva posición está dentro de los límites del mapa
            if (nuevaPosX >= 0 && nuevaPosX < anchoPictureBox / 10 && nuevaPosY >= 0 && nuevaPosY < altoPictureBox / 10)
            {
                Estela.MoverEstela(PosX, PosY);
                PosX = nuevaPosX;
                PosY = nuevaPosY;
                Combustible -= Velocidad / 5;
            }
            else
            {
                Morir();
                return;
                // Podrías detener el movimiento, destruir la moto, etc.
            }
            Item item = mapa.ObtenerItemEnPosicion(nuevaPosX, nuevaPosY);
            if (item != null)
            {
                RecogerItem(item);
            }

            Poder poder = mapa.ObtenerPoderEnPosicion(nuevaPosX, nuevaPosY);
            if (poder != null)
            {
                RecogerPoder(poder);
            }
        }
        public void Morir()
        {
            // Implementa la lógica para cuando la moto "muere"
            Viva = false;
            Console.WriteLine("Moto destruida");
            // Aquí puedes marcar la moto como inactiva, quitarla del mapa o manejar cualquier otra lógica
        }
        public void RecogerItem(Item item)
        {
            Items.Enqueue(item);
        }

        public void RecogerPoder(Poder poder)
        {
            Poderes.Push(poder);
        }

        private bool VerificarColision(int nuevaPosX, int nuevaPosY)
        {
            // Implementar la lógica de colisión: verificar si la nueva posición está ocupada
            NodoEstela actual = Estela.Cabeza;
            while (actual != null)
            {
                if (actual.PosX == nuevaPosX && actual.PosY == nuevaPosY)
                {
                    return true;
                }
                actual = actual.Siguiente;
            }
            // Agregar otras verificaciones de colisión si es necesario
            return false;
        }

    }
    public class Bot : Moto
    {
        private Random random;

        public Bot(int xInicial, int yInicial) : base(xInicial, yInicial)
        {
            random = new Random();
        }
        public async Task IniciarCambiosDeDireccionAsync()
        {
            while (true) // Bucle infinito, puedes agregar una condición para detenerlo
            {
                CambiarDireccion();
                await Task.Delay(1000); // Esperar 5 segundos entre cada cambio de dirección
            }
        }
        public void MoverAutomáticamente(Mapa mapa, int anchoPictureBox, int altoPictureBox)
        {
            // Mover la moto en la dirección actual, pasando el mapa y las dimensiones del PictureBox
            base.MoverMoto(mapa, anchoPictureBox, altoPictureBox);

            // Verificar si hay ítems o poderes en la nueva posición y usarlos
            Item item = mapa.ObtenerItemEnPosicion(PosX, PosY);
            if (item != null)
            {
                RecogerItem(item);
                UsarItem();
            }

            Poder poder = mapa.ObtenerPoderEnPosicion(PosX, PosY);
            if (poder != null)
            {
                RecogerPoder(poder);
                UsarPoder();
            }
        }


        private void CambiarDireccion()
        {
            // Cambiar la dirección de forma aleatoria
            int direccion = random.Next(4);
            switch (direccion)
            {
                case 0:
                    DireccionActual = Direccion.Arriba;
                    break;
                case 1:
                    DireccionActual = Direccion.Abajo;
                    break;
                case 2:
                    DireccionActual = Direccion.Izquierda;
                    break;
                case 3:
                    DireccionActual = Direccion.Derecha;
                    break;
            }
        }
    }

}
