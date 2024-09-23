namespace Tron
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            GameScreen = new PictureBox();
            MostrarCombustible = new TextBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            MostrarPoderes = new TextBox();
            MostrarSeleccion = new TextBox();
            textBox3 = new TextBox();
            MostrarPoderesTemp = new TextBox();
            ((System.ComponentModel.ISupportInitialize)GameScreen).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = SystemColors.GradientActiveCaption;
            button1.ForeColor = SystemColors.ActiveCaptionText;
            button1.Location = new Point(1120, 21);
            button1.Name = "button1";
            button1.Size = new Size(133, 48);
            button1.TabIndex = 0;
            button1.Text = "Pause";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // GameScreen
            // 
            GameScreen.BackColor = SystemColors.ActiveCaptionText;
            GameScreen.Location = new Point(12, 12);
            GameScreen.Name = "GameScreen";
            GameScreen.Size = new Size(1040, 860);
            GameScreen.TabIndex = 1;
            GameScreen.TabStop = false;
            // 
            // MostrarCombustible
            // 
            MostrarCombustible.BackColor = SystemColors.WindowFrame;
            MostrarCombustible.Font = new Font("Segoe UI", 16F);
            MostrarCombustible.Location = new Point(1058, 150);
            MostrarCombustible.Name = "MostrarCombustible";
            MostrarCombustible.Size = new Size(121, 50);
            MostrarCombustible.TabIndex = 2;
            MostrarCombustible.TextChanged += MostrarCombustible_TextChanged;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.WindowFrame;
            textBox1.Font = new Font("Segoe UI", 16F);
            textBox1.Location = new Point(1058, 94);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(192, 50);
            textBox1.TabIndex = 3;
            textBox1.Text = "Combustible";
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.WindowFrame;
            textBox2.Font = new Font("Segoe UI", 14F);
            textBox2.Location = new Point(1058, 206);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(258, 45);
            textBox2.TabIndex = 4;
            textBox2.Text = "Poder Seleccionado";
            // 
            // MostrarPoderes
            // 
            MostrarPoderes.BackColor = SystemColors.WindowFrame;
            MostrarPoderes.Font = new Font("Segoe UI", 14F);
            MostrarPoderes.Location = new Point(1058, 359);
            MostrarPoderes.Name = "MostrarPoderes";
            MostrarPoderes.Size = new Size(258, 45);
            MostrarPoderes.TabIndex = 5;
            // 
            // MostrarSeleccion
            // 
            MostrarSeleccion.BackColor = SystemColors.WindowFrame;
            MostrarSeleccion.Font = new Font("Segoe UI", 14F);
            MostrarSeleccion.Location = new Point(1058, 257);
            MostrarSeleccion.Name = "MostrarSeleccion";
            MostrarSeleccion.Size = new Size(258, 45);
            MostrarSeleccion.TabIndex = 6;
            // 
            // textBox3
            // 
            textBox3.BackColor = SystemColors.WindowFrame;
            textBox3.Font = new Font("Segoe UI", 14F);
            textBox3.Location = new Point(1058, 308);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(258, 45);
            textBox3.TabIndex = 7;
            textBox3.Text = "En Reserva";
            // 
            // MostrarPoderesTemp
            // 
            MostrarPoderesTemp.BackColor = SystemColors.WindowFrame;
            MostrarPoderesTemp.Font = new Font("Segoe UI", 14F);
            MostrarPoderesTemp.Location = new Point(1058, 410);
            MostrarPoderesTemp.Name = "MostrarPoderesTemp";
            MostrarPoderesTemp.Size = new Size(258, 45);
            MostrarPoderesTemp.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1328, 894);
            Controls.Add(MostrarPoderesTemp);
            Controls.Add(textBox3);
            Controls.Add(MostrarSeleccion);
            Controls.Add(MostrarPoderes);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(MostrarCombustible);
            Controls.Add(GameScreen);
            Controls.Add(button1);
            Name = "Form1";
            Text = "TRON C#";
            ((System.ComponentModel.ISupportInitialize)GameScreen).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private PictureBox GameScreen;
        private TextBox MostrarCombustible;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox MostrarPoderes;
        private TextBox MostrarSeleccion;
        private TextBox textBox3;
        private TextBox MostrarPoderesTemp;
    }
}
