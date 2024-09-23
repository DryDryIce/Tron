using System;
using System.Text;
using System.Windows.Forms;

namespace Tron
{
    public partial class Form1 : Form
    {
        private Moto jugador;
        private Mapa mapa;
        private List<Bot> bots;
        private System.Windows.Forms.Timer timer;
        private bool juegoIniciado = false;

        public Form1()
        {
            InitializeComponent();
            GameScreen.Click += GameScreen_Click;
            GameScreen.TabStop = true;
            GameScreen.Focus();
            GameScreen.PreviewKeyDown += GameScreen_PreviewKeyDown;
            GameScreen.SizeMode = PictureBoxSizeMode.AutoSize;
            mapa = new Mapa(52, 43);
            jugador = new Moto(26, 21);
            InicializarBots();

            ActualizarTextBoxes();

            // Inicializacion de Poderes
            Random random = new Random();
            int xEscudo = random.Next(0, 52); // Coordenadas Random
            int yEscudo = random.Next(0, 42); // Coordenadas Random
            int timeEscudo = random.Next(0, 4); // Duracion
            Poder escudo = new Poder("Escudo", timeEscudo, xEscudo, yEscudo);
            int xHiperVel = random.Next(0, 52);
            int yHiperVel = random.Next(0, 42);
            int timeHiperVel = random.Next(0, 4);
            Poder hipervel = new Poder("HiperVelocidad", timeHiperVel, xHiperVel, yHiperVel);
            int xCombustible = random.Next(0, 52);
            int yCombustible = random.Next(0, 42);
            int addCombustible = random.Next(0, 99);
            Item combustible = new Item("Combustible", addCombustible, xCombustible, yCombustible);
            // Inicializacion de Items
            int xCrecimiento = random.Next(0, 52);
            int yCrecimiento = random.Next(0, 42);
            int addCrecimiento = random.Next(0, 10); // Valor de efecto
            Item crecimiento = new Item("Crecimiento", addCrecimiento, xCrecimiento, yCrecimiento);
            int xMina = random.Next(0, 52);
            int yMina = random.Next(0, 42);
            int addMina = random.Next(0, 10);
            Item mina = new Item("Mina", addMina, xMina, yMina);

            // Colocarlos dentro del mapa y meterlos en la lista del mismo
            mapa.ColocarPoder(escudo);
            mapa.ColocarPoder(hipervel);

            mapa.ColocarItem(combustible);
            mapa.ColocarItem(crecimiento);
            mapa.ColocarItem(mina);

            ConfigurarTimer();
        }
        private void GameScreen_Click(object sender, EventArgs e) // Inicio de juego mediante click en la pictureBox
        {
            if (!juegoIniciado)
            {
                juegoIniciado = true;
                GameScreen.Focus();
                timer.Start();  // Iniciar el temporizador para que comience el juego
                IniciarMovimientoBots();
            }

        }
        private void IniciarMovimientoBots()
        {
            // Iniciar el cambio de dirección asíncrono para cada bot
            foreach (var bot in bots)
            {
                _ = bot.IniciarCambiosDeDireccionAsync();
            }
        }
        private void ActualizarTextBoxes()
        {
            // Actualizar el TextBox del Combustible
            MostrarCombustible.Text = jugador.Combustible.ToString();

            // Actualizar el TextBox de los Poderes
            if (jugador.Poderes.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var poder in jugador.Poderes)
                {
                    sb.AppendLine(poder.Nombre);  // Suponiendo que 'Poder' tiene una propiedad 'Nombre'
                }
                MostrarPoderes.Text = sb.ToString();
            }
            else
            {
                MostrarPoderes.Text = "Vacio";
            }
            if (jugador.PoderesTemp.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var poder in jugador.PoderesTemp)
                {
                    sb.AppendLine(poder.Nombre);
                }
                MostrarPoderesTemp.Text = sb.ToString();
            }
            else
            {
                MostrarPoderesTemp.Text = "Vacio";
            }

            // Actualizar el TextBox del Primer Poder
            if (jugador.Poderes.Count > 0)
            {
                Poder primerPoder = jugador.Poderes.Peek();
                MostrarSeleccion.Text = primerPoder.Nombre; 
            }
            else
            {
                MostrarSeleccion.Text = "No Seleccionado";
            }
        }
        private async void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.Space:
                case Keys.Z:
                case Keys.X:
                    e.IsInputKey = true;  // Marca estas teclas como teclas de entrada
                    break;
            }
            Poder temp;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (jugador.DireccionActual != Direccion.Abajo)
                    {
                        jugador.DireccionActual = Direccion.Arriba;
                    }
                    break;
                case Keys.Down:
                    if (jugador.DireccionActual != Direccion.Arriba)
                    {
                        jugador.DireccionActual = Direccion.Abajo;
                    }
                    break;
                case Keys.Left:
                    if (jugador.DireccionActual != Direccion.Derecha)
                    {
                        jugador.DireccionActual = Direccion.Izquierda;
                    }
                    break;
                case Keys.Right:
                    if (jugador.DireccionActual != Direccion.Izquierda)
                    {
                        jugador.DireccionActual = Direccion.Derecha;
                    }
                    break;
                case Keys.Space:
                    jugador.UsarPoder();
                    break;
                case Keys.Z:
                    if (jugador.Poderes.Count > 0)  // Verificar si hay poderes en la pila
                    {
                        temp = jugador.Poderes.Pop();
                        jugador.PoderesTemp.Push(temp);  // Mover el poder a la pila temporal
                    }
                    break;

                case Keys.X:
                    if (jugador.PoderesTemp.Count > 0)  // Verificar si hay poderes en la pila temporal
                    {
                        temp = jugador.PoderesTemp.Pop();
                        jugador.Poderes.Push(temp);  // Mover el poder de vuelta a la pila original
                    }
                    break;
            }
        }
        private async void InicializarBots()
        {
            bots = new List<Bot>();
            // Crear varios bots y añadirlos a la lista
            bots.Add(new Bot(18, 13));
            bots.Add(new Bot(34, 13));
            bots.Add(new Bot(18, 29));
            bots.Add(new Bot(34, 29));

        }
        private void ConfigurarTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 80; // Temporizador en milisegundos
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!juegoIniciado) return;

            ActualizarTextBoxes();

            jugador.MoverMoto(mapa, GameScreen.Width, GameScreen.Height);  // Mover la moto del jugador en la dirección actual

            foreach (var bot in bots)  // Mover los bots
            {
                bot.MoverAutomáticamente(mapa, GameScreen.Width, GameScreen.Height);

                // Verificar colisión entre el jugador y el bot
                if (jugador.PosX == bot.PosX && jugador.PosY == bot.PosY && jugador.EscudoActivo == false)
                {
                    jugador.Morir();
                    bot.Morir();
                    juegoIniciado = false;
                }

                // Verificar colisión entre los bots 
                foreach (var otroBot in bots)
                {
                    if (bot != otroBot && bot.PosX == otroBot.PosX && bot.PosY == otroBot.PosY)
                    {
                        bot.MorirBot(mapa);
                        otroBot.MorirBot(mapa);
                    }
                }
            }
            // Actualizar la interfaz gráfica para reflejar el nuevo estado del juego
            ActualizarInterfaz();
        }

        private void ActualizarInterfaz()
        {
            Bitmap mapaBitmap = new Bitmap(GameScreen.Width, GameScreen.Height);

            using (Graphics g = Graphics.FromImage(mapaBitmap))
            {
                int anchoNodo = GameScreen.Width / mapa.Ancho;
                int altoNodo = GameScreen.Height / mapa.Alto;

                // Dibujar el grid del mapa
                for (int i = 0; i < mapa.Ancho; i++)
                {
                    for (int j = 0; j < mapa.Alto; j++)
                    {
                        NodoMapa nodo = mapa.ObtenerNodo(i, j);
                        g.DrawRectangle(Pens.Gray, nodo.X * anchoNodo, nodo.Y * altoNodo, anchoNodo, altoNodo);
                    }
                }
                foreach (var item in mapa.Items)
                {
                    g.FillRectangle(Brushes.Red, item.X * anchoNodo, item.Y * altoNodo, anchoNodo, altoNodo);
                }

                // Dibujar poderes en el mapa
                foreach (var poder in mapa.Poderes)
                {
                    g.FillRectangle(Brushes.Yellow, poder.X * anchoNodo, poder.Y * altoNodo, anchoNodo, altoNodo);
                }

                // Dibujar la moto como un rectángulo
                if (jugador.Viva)
                {
                    if (jugador.EscudoActivo == false)
                    {
                        g.FillRectangle(Brushes.Purple, jugador.PosX * anchoNodo, jugador.PosY * altoNodo, anchoNodo, altoNodo);
                        // Dibujar la estela del jugador
                        NodoEstela actual = jugador.Estela.Cabeza;
                        while (actual != null)
                        {
                            g.FillRectangle(Brushes.MediumPurple, actual.PosX * anchoNodo, actual.PosY * altoNodo, anchoNodo, altoNodo);
                            actual = actual.Siguiente;
                        }
                    }
                    if (jugador.EscudoActivo == true)
                    {
                        g.FillRectangle(Brushes.DarkBlue, jugador.PosX * anchoNodo, jugador.PosY * altoNodo, anchoNodo, altoNodo);
                        // Dibujar la estela del jugador
                        NodoEstela actual = jugador.Estela.Cabeza;
                        while (actual != null)
                        {
                            g.FillRectangle(Brushes.Blue, actual.PosX * anchoNodo, actual.PosY * altoNodo, anchoNodo, altoNodo);
                            actual = actual.Siguiente;
                        }
                    }
                }
                if (jugador.Viva == false)
                {
                    juegoIniciado = false;
                }
                if (TodosLosBotsMuertos())
                {
                    juegoIniciado = false;
                }

                // Dibujar los bots si están vivos
                foreach (var bot in bots)
                {
                    if (bot.Viva)
                    {
                        g.FillRectangle(Brushes.DarkRed, bot.PosX * anchoNodo, bot.PosY * altoNodo, anchoNodo, altoNodo);

                        // Dibujar la estela del bot
                        NodoEstela botEstela = bot.Estela.Cabeza;
                        while (botEstela != null)
                        {
                            g.FillRectangle(Brushes.Red, botEstela.PosX * anchoNodo, botEstela.PosY * altoNodo, anchoNodo, altoNodo);
                            botEstela = botEstela.Siguiente;
                        }
                    }
                }
            }
            if (GameScreen.Image != null)
            {
                GameScreen.Image.Dispose(); // Libera el bitmap anterior
            }
            // Asignar el Bitmap dibujado al PictureBox
            GameScreen.Image = mapaBitmap;
        }
        private bool TodosLosBotsMuertos()
        {
            // Verificar si todos los bots están muertos
            return bots.All(bot => !bot.Viva);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            juegoIniciado = false;
        }

        private void MostrarCombustible_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
