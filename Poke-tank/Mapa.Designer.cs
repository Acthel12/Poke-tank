namespace Poke_tank
{
    partial class Mapa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mapa));
            botonSorpresaFlavio = new Button();
            botonFinalizar = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // botonSorpresaFlavio
            // 
            botonSorpresaFlavio.Location = new Point(1031, 449);
            botonSorpresaFlavio.Margin = new Padding(3, 2, 3, 2);
            botonSorpresaFlavio.Name = "botonSorpresaFlavio";
            botonSorpresaFlavio.Size = new Size(4, 4);
            botonSorpresaFlavio.TabIndex = 3;
            botonSorpresaFlavio.UseVisualStyleBackColor = true;
            botonSorpresaFlavio.Click += botonSorpresaFlavio_Click;
            // 
            // botonFinalizar
            // 
            botonFinalizar.BackColor = Color.Firebrick;
            botonFinalizar.FlatStyle = FlatStyle.Flat;
            botonFinalizar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            botonFinalizar.ForeColor = Color.White;
            botonFinalizar.Location = new Point(892, 22);
            botonFinalizar.Margin = new Padding(3, 2, 3, 2);
            botonFinalizar.Name = "botonFinalizar";
            botonFinalizar.Size = new Size(150, 30);
            botonFinalizar.TabIndex = 4;
            botonFinalizar.Text = "Finalizar aventura";
            botonFinalizar.UseVisualStyleBackColor = false;
            botonFinalizar.Click += botonFinalizar_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Firebrick;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(552, 22);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(150, 30);
            button1.TabIndex = 5;
            button1.Text = "Prueba puntuacion";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Mapa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.MAPA_FLAVIO_ADVENTURES;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1104, 505);
            Controls.Add(button1);
            Controls.Add(botonFinalizar);
            Controls.Add(botonSorpresaFlavio);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "Mapa";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Flavio's Tank Adventure";
            FormClosed += Mapa_FormClosed;
            Load += Mapa_Load;
            Paint += DrawGame;
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ResumeLayout(false);
        }

        #endregion
        private Button botonSorpresaFlavio;
        private Button botonFinalizar;
        private Button button1;
    }
}
