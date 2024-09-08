using System;
using System.Windows.Forms;

namespace Tron
{
    public partial class Form1 : Form
    {
        private Moto jugador;
        private Mapa mapa;
        private List<Bot> bots;
        private System.Windows.Forms.Timer timer;  // Explicitly specify the correct Timer


        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            GameScreen.PreviewKeyDown += new PreviewKeyDownEventHandler(GameScreen_PreviewKeyDown);
            GameScreen.SizeMode = PictureBoxSizeMode.AutoSize;
            mapa = new Mapa(104,87);
            jugador = new Moto(52, 43); // Initial position of the moto 
            InicializarBots(); // Crear e inicializar bots // Configurar y empezar el temporizador para mover la moto y bots
            Poder escudo = new Poder("Escudo", 10, 5, 5);
            mapa.ColocarPoder(escudo);
            ConfigurarTimer(); // Set up and start the timer to move the moto
            this.Focus();
        }
        private async void InicializarBots()
        {
            bots = new List<Bot>();
            // Crear varios bots y añadirlos a la lista
            bots.Add(new Bot(20, 20));
            bots.Add(new Bot(84, 67));
            bots.Add(new Bot(20, 67));
            bots.Add(new Bot(84, 20));

            // Iniciar el cambio de dirección asíncrono para cada bot
            foreach (var bot in bots)
            {
                _ = bot.IniciarCambiosDeDireccionAsync();
            }
        }
        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (jugador.DireccionActual != Direccion.Abajo)
                        jugador.DireccionActual = Direccion.Arriba;
                    break;
                case Keys.Down:
                    if (jugador.DireccionActual != Direccion.Arriba)
                        jugador.DireccionActual = Direccion.Abajo;
                    break;
                case Keys.Left:
                    if (jugador.DireccionActual != Direccion.Derecha)
                        jugador.DireccionActual = Direccion.Izquierda;
                    break;
                case Keys.Right:
                    if (jugador.DireccionActual != Direccion.Izquierda)
                        jugador.DireccionActual = Direccion.Derecha;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (jugador.DireccionActual != Direccion.Abajo) // Evita que se mueva en la dirección opuesta
                        jugador.DireccionActual = Direccion.Arriba;
                    break;
                case Keys.Down:
                    if (jugador.DireccionActual != Direccion.Arriba)
                        jugador.DireccionActual = Direccion.Abajo;
                    break;
                case Keys.Left:
                    if (jugador.DireccionActual != Direccion.Derecha)
                        jugador.DireccionActual = Direccion.Izquierda;
                    break;
                case Keys.Right:
                    if (jugador.DireccionActual != Direccion.Izquierda)
                        jugador.DireccionActual = Direccion.Derecha;
                    break;
            }
        }
        private void ConfigurarTimer()
        {
            timer = new System.Windows.Forms.Timer();  // Explicitly specify the correct Timer
            timer.Interval = 100; // Interval in milliseconds (adjust as needed)
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            jugador.MoverMoto(mapa, GameScreen.Width, GameScreen.Height);

            foreach (var bot in bots)
            {
                bot.MoverAutomáticamente(mapa, GameScreen.Width, GameScreen.Height);

                // Verificar colisión entre el jugador y el bot
                if (jugador.PosX == bot.PosX && jugador.PosY == bot.PosY)
                {
                    jugador.Morir();
                    bot.Morir();
                }

                // Verificar si el bot colisiona con otro bot (opcional)
                foreach (var otroBot in bots)
                {
                    if (bot != otroBot && bot.PosX == otroBot.PosX && bot.PosY == otroBot.PosY)
                    {
                        bot.Morir();
                        otroBot.Morir();
                    }
                }
            }
            ActualizarInterfaz(); // Update the graphical interface with the new position
        }

        private void ActualizarInterfaz()
        {
            // Crear un Bitmap del mismo tamaño que el PictureBox
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
                    g.FillRectangle(Brushes.Purple, jugador.PosX * anchoNodo, jugador.PosY * altoNodo, anchoNodo, altoNodo);
                    // Dibujar la estela del jugador
                    NodoEstela actual = jugador.Estela.Cabeza;
                    while (actual != null)
                    {
                        g.FillRectangle(Brushes.MediumPurple, actual.PosX * anchoNodo, actual.PosY * altoNodo, anchoNodo, altoNodo);
                        actual = actual.Siguiente;
                    }
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
            GameScreen.Image = mapaBitmap;
            // Asignar el Bitmap dibujado al PictureBox
            GameScreen.Image = mapaBitmap;
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
