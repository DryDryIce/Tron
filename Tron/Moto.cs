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
        // Atributos principales
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int Velocidad { get; set; }  
        public int Combustible { get; set; }
        public bool Viva { get; set; }
        public Direccion DireccionActual { get; set; }
        public ListaEstela Estela { get; private set; }
        public Queue<Item> Items { get; private set; }
        public Stack<Poder> Poderes { get; private set; }
        public Stack<Poder> PoderesTemp { get; private set; }

        public bool EscudoActivo;

        public Moto(int xInicial, int yInicial)
        {
            PosX = xInicial;
            PosY = yInicial;
            Velocidad = 1;
            Combustible = 100;
            DireccionActual = Direccion.Arriba;
            Estela = new ListaEstela();
            Items = new Queue<Item>();
            Poderes = new Stack<Poder>();
            PoderesTemp = new Stack<Poder>();

            EscudoActivo = false;
            Viva = true;

            // Estela inicial
            Estela.AgregarNodo(PosX - 1, PosY);
            Estela.AgregarNodo(PosX - 2, PosY);
        }
        
        public void UsarItem() // Remueve el Item de la cola y lo utiliza
        {
            if (Items.Count > 0)
            {
                Item item = Items.Dequeue();
                item.AplicarEfecto(this);
            }
        }

        public void UsarPoder() // Remueve el Poder de la pila o Stack y lo utiliza
        {
            if (Poderes.Count > 0)
            {
                Poder poder = Poderes.Pop();
                poder.AplicarEfectoTemporal(this);
            }
        }
        public void RecogerItem(Item item) // Agrega a la cola
        {
            Items.Enqueue(item);
        }

        public void RecogerPoder(Poder poder) // Agrega a la pila o Stack
        {
            Poderes.Push(poder);
        }
        public async void ActivarEscudo(int duracion) // Activa y desactiva el Escudo
        {
            EscudoActivo = true;
            await Task.Delay(duracion * 1000); // Espera en segundos
            EscudoActivo = false;
        }
        public async void ActivarHiperVel(int duracion)
        {
            Velocidad = 4;
            await Task.Delay(duracion * 1000); // Causa Hipervelocidad por un tiempo
            Velocidad = 1;
        }
        public void MoverMoto(Mapa mapa, int anchoPictureBox, int altoPictureBox)
        {
            if (!Viva || Combustible <= 0)
                return;

            int nuevaPosX = PosX;
            int nuevaPosY = PosY;

            switch (DireccionActual) // Switch de direccion interno del jugador
            {
                case Direccion.Arriba:
                    nuevaPosY -= Velocidad;
                    break;
                case Direccion.Abajo:
                    nuevaPosY += Velocidad;
                    break;
                case Direccion.Izquierda:
                    nuevaPosX -= Velocidad;
                    break;
                case Direccion.Derecha:
                    nuevaPosX += Velocidad;
                    break;
            }

            // Verificar colisiones con la estela
            if (VerificarColision(nuevaPosX, nuevaPosY))
            {
                Morir();
                return;
            }

            // Verificar si la nueva posición está dentro de los límites del mapa
            if (nuevaPosX >= 0 && nuevaPosX < anchoPictureBox / 20 && nuevaPosY >= 0 && nuevaPosY < altoPictureBox / 20)
            {
                Estela.MoverEstela(PosX, PosY);
                PosX = nuevaPosX;
                PosY = nuevaPosY;
                Combustible -= 1;
            }
            else
            {
                Morir();  // Si sale del área del mapa
            }

            // Verificar si hay ítems o poderes en la nueva posición
            Item item = mapa.ObtenerItemEnPosicion(nuevaPosX, nuevaPosY);
            if (item != null)
            {
                RecogerItem(item);
                UsarItem();
                mapa.EliminarItem(item);
            }

            Poder poder = mapa.ObtenerPoderEnPosicion(nuevaPosX, nuevaPosY);
            if (poder != null)
            {
                RecogerPoder(poder);
                mapa.EliminarPoder(poder);            
            }
        }
        public void Morir()
        {
            // Implementa la lógica para cuando la moto "muere"
            Viva = false;
            Console.WriteLine("Moto destruida");

        }

        private bool VerificarColision(int nuevaPosX, int nuevaPosY)
        {
            // Verificar si la nueva posición está ocupada
            NodoEstela actual = Estela.Cabeza;
            while (actual != null)
            {
                if (actual.PosX == nuevaPosX && actual.PosY == nuevaPosY)
                {
                    return true;
                }
                actual = actual.Siguiente;
            }

            return false;
        }


    }
    public class Bot : Moto // Hereda los atributos de la clase Moto
    {
        private Random random;

        public Bot(int xInicial, int yInicial) : base(xInicial, yInicial)
        {
            random = new Random();
        }
        public async Task IniciarCambiosDeDireccionAsync()
        {
            while (Viva) // Bucle infinito mientras este vivo
            {
                CambiarDireccion();
                await Task.Delay(800); 
            }
        }
        public void MoverAutomáticamente(Mapa mapa, int anchoPictureBox, int altoPictureBox)
        {
            // Mover la moto en la dirección actual, pasando el mapa y las dimensiones del pictureBox
            base.MoverMoto(mapa, anchoPictureBox, altoPictureBox);

            // Verificar si hay ítems o poderes en la nueva posición y usarlos
            Item item = mapa.ObtenerItemEnPosicion(PosX, PosY);
            if (item != null)
            {
                RecogerItem(item);
                UsarItem();
                mapa.EliminarItem(item);
            }

            Poder poder = mapa.ObtenerPoderEnPosicion(PosX, PosY);
            if (poder != null)
            {
                RecogerPoder(poder);
                UsarItem();
                mapa.EliminarPoder(poder);
            }
        }
        private void CambiarDireccion()
        {
            // Cambiar la dirección de forma aleatoria
            int direccion = random.Next(4);
            switch (direccion)
            {
                case 0:
                    if (DireccionActual != Direccion.Abajo)
                        DireccionActual = Direccion.Arriba;
                        break;
                case 1:
                    if (DireccionActual != Direccion.Arriba)
                        DireccionActual = Direccion.Abajo;
                        break;
                case 2:
                    if (DireccionActual != Direccion.Derecha)
                        DireccionActual = Direccion.Izquierda;
                        break;
                case 3:
                    if (DireccionActual != Direccion.Izquierda)
                        DireccionActual = Direccion.Derecha;
                        break;
            }
        }
        public void MorirBot(Mapa mapa)
        {
            // Lógica para soltar ítems del bot
            SoltarInventario(mapa);
            Viva = false;  // El bot ya no está vivo
            Console.WriteLine("Bot destruido y soltó su inventario");
        }

        private void SoltarInventario(Mapa mapa)
        {
            // Soltar ítems en posiciones aleatorias del mapa
            while (Items.Count > 0)
            {
                Item item = Items.Dequeue();  // Sacar el ítem de la cola
                int xAleatorio = random.Next(0, mapa.Ancho);
                int yAleatorio = random.Next(0, mapa.Alto);
                item.X = xAleatorio;  // Asignar nueva posición aleatoria
                item.Y = yAleatorio;
                mapa.ColocarItem(item);  // Colocar el ítem en el mapa
            }

            // Soltar poderes en posiciones aleatorias del mapa
            while (Poderes.Count > 0)
            {
                Poder poder = Poderes.Pop();  // Sacar el poder de la pila
                int xAleatorio = random.Next(0, mapa.Ancho);
                int yAleatorio = random.Next(0, mapa.Alto);
                poder.X = xAleatorio;  // Asignar nueva posición aleatoria
                poder.Y = yAleatorio;
                mapa.ColocarPoder(poder);  // Colocar el poder en el mapa
            }
        }
    }

}
