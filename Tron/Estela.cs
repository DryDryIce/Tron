using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    public class NodoEstela
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public NodoEstela Siguiente { get; set; }

        public NodoEstela(int x, int y)
        {
            PosX = x;
            PosY = y;
            Siguiente = null;
        }
    }

    public class ListaEstela
    {
        public NodoEstela Cabeza { get; private set; }

        public void AgregarNodo(int x, int y)
        {
            NodoEstela nuevoNodo = new NodoEstela(x, y);
            if (Cabeza == null)
            {
                Cabeza = nuevoNodo;
            }
            else
            {
                NodoEstela actual = Cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevoNodo;
            }
        }

        public void MoverEstela(int nuevaPosX, int nuevaPosY)
        {
            if (Cabeza != null)
            {
                NodoEstela actual = Cabeza;
                while (actual.Siguiente != null)
                {
                    actual.PosX = actual.Siguiente.PosX;
                    actual.PosY = actual.Siguiente.PosY;
                    actual = actual.Siguiente;
                }
                actual.PosX = nuevaPosX;
                actual.PosY = nuevaPosY;
            }
        }
    }

}
