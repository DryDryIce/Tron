using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    public class NodoMapa
    {
        public int X { get; set; }
        public int Y { get; set; }
        public NodoMapa Norte { get; set; }
        public NodoMapa Sur { get; set; }
        public NodoMapa Este { get; set; }
        public NodoMapa Oeste { get; set; }

        // Otros atributos y métodos
    }

    public class Mapa
    {
        public int Ancho { get; private set; }
        public int Alto { get; private set; }
        private NodoMapa[,] grid;
        private List<Item> items;  // Lista de ítems en el mapa
        private List<Poder> poderes;  // Lista de poderes en el mapa

        public Mapa(int ancho, int alto)
        {
            Ancho = ancho;
            Alto = alto;
            grid = new NodoMapa[ancho, alto];
            items = new List<Item>();
            poderes = new List<Poder>();

            for (int i = 0; i < ancho; i++)
            {
                for (int j = 0; j < alto; j++)
                {
                    grid[i, j] = new NodoMapa { X = i, Y = j };

                    // Configurar referencias Norte, Sur, Este, Oeste
                    if (i > 0)
                        grid[i, j].Oeste = grid[i - 1, j];
                    if (i < ancho - 1)
                        grid[i, j].Este = grid[i + 1, j];
                    if (j > 0)
                        grid[i, j].Norte = grid[i, j - 1];
                    if (j < alto - 1)
                        grid[i, j].Sur = grid[i, j + 1];
                }
            }
        }

        public NodoMapa ObtenerNodo(int x, int y)
        {
            return grid[x, y];
        }

        public void ColocarItem(Item item)
        {
            items.Add(item);
        }

        public void ColocarPoder(Poder poder)
        {
            poderes.Add(poder);
        }

        public Item ObtenerItemEnPosicion(int x, int y)
        {
            return items.FirstOrDefault(item => item.X == x && item.Y == y);
        }

        public Poder ObtenerPoderEnPosicion(int x, int y)
        {
            return poderes.FirstOrDefault(poder => poder.X == x && poder.Y == y);
        }

        // Propiedades públicas para acceder a los ítems y poderes
        public List<Item> Items
        {
            get { return items; }
        }

        public List<Poder> Poderes
        {
            get { return poderes; }
        }
    }

}