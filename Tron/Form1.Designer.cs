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
            ItemAndPower = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)GameScreen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ItemAndPower).BeginInit();
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
            button1.Text = "Start Game";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // GameScreen
            // 
            GameScreen.BackColor = SystemColors.ActiveCaptionText;
            GameScreen.Location = new Point(12, 12);
            GameScreen.Name = "GameScreen";
            GameScreen.Size = new Size(1040, 870);
            GameScreen.TabIndex = 1;
            GameScreen.TabStop = false;
            // 
            // ItemAndPower
            // 
            ItemAndPower.BackColor = SystemColors.Menu;
            ItemAndPower.Location = new Point(1071, 86);
            ItemAndPower.Name = "ItemAndPower";
            ItemAndPower.Size = new Size(245, 796);
            ItemAndPower.TabIndex = 2;
            ItemAndPower.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1328, 894);
            Controls.Add(ItemAndPower);
            Controls.Add(GameScreen);
            Controls.Add(button1);
            Name = "Form1";
            Text = "TRON C#";
            ((System.ComponentModel.ISupportInitialize)GameScreen).EndInit();
            ((System.ComponentModel.ISupportInitialize)ItemAndPower).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private PictureBox GameScreen;
        private PictureBox ItemAndPower;
    }
}
