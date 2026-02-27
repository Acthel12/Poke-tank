namespace Poke_tank
{
    partial class Menu_Principal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu_Principal));
            fondoMenuPrincipal = new PictureBox();
            botonIniciarPartida = new Button();
            botonSalir = new Button();
            botonPuntuaciones = new Button();
            buttonCargarPartida = new Button();
            ((System.ComponentModel.ISupportInitialize)fondoMenuPrincipal).BeginInit();
            SuspendLayout();
            // 
            // fondoMenuPrincipal
            // 
            fondoMenuPrincipal.Image = Properties.Resources.def_GIF_FONDO_MENU_PPAL_FLAVIO_ADVENTURES;
            fondoMenuPrincipal.Location = new Point(0, -50);
            fondoMenuPrincipal.Margin = new Padding(3, 2, 3, 2);
            fondoMenuPrincipal.Name = "fondoMenuPrincipal";
            fondoMenuPrincipal.Size = new Size(1134, 640);
            fondoMenuPrincipal.SizeMode = PictureBoxSizeMode.Zoom;
            fondoMenuPrincipal.TabIndex = 0;
            fondoMenuPrincipal.TabStop = false;
            // 
            // botonIniciarPartida
            // 
            botonIniciarPartida.BackColor = Color.DodgerBlue;
            botonIniciarPartida.FlatStyle = FlatStyle.Popup;
            botonIniciarPartida.Font = new Font("Microsoft New Tai Lue", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            botonIniciarPartida.ForeColor = Color.White;
            botonIniciarPartida.Location = new Point(452, 269);
            botonIniciarPartida.Margin = new Padding(3, 2, 3, 2);
            botonIniciarPartida.Name = "botonIniciarPartida";
            botonIniciarPartida.Size = new Size(209, 35);
            botonIniciarPartida.TabIndex = 1;
            botonIniciarPartida.Text = "Iniciar nueva partida";
            botonIniciarPartida.UseVisualStyleBackColor = false;
            botonIniciarPartida.Click += botonIniciarPartida_Click;
            // 
            // botonSalir
            // 
            botonSalir.BackColor = Color.DodgerBlue;
            botonSalir.FlatStyle = FlatStyle.Popup;
            botonSalir.Font = new Font("Microsoft New Tai Lue", 12F, FontStyle.Bold);
            botonSalir.ForeColor = Color.White;
            botonSalir.Location = new Point(452, 427);
            botonSalir.Margin = new Padding(3, 2, 3, 2);
            botonSalir.Name = "botonSalir";
            botonSalir.Size = new Size(209, 35);
            botonSalir.TabIndex = 3;
            botonSalir.Text = "Salir";
            botonSalir.UseVisualStyleBackColor = false;
            botonSalir.Click += botonSalir_Click;
            // 
            // botonPuntuaciones
            // 
            botonPuntuaciones.BackColor = Color.DodgerBlue;
            botonPuntuaciones.FlatStyle = FlatStyle.Popup;
            botonPuntuaciones.Font = new Font("Microsoft New Tai Lue", 12F, FontStyle.Bold);
            botonPuntuaciones.ForeColor = Color.White;
            botonPuntuaciones.Location = new Point(452, 373);
            botonPuntuaciones.Margin = new Padding(3, 2, 3, 2);
            botonPuntuaciones.Name = "botonPuntuaciones";
            botonPuntuaciones.Size = new Size(209, 35);
            botonPuntuaciones.TabIndex = 4;
            botonPuntuaciones.Text = "Ver puntuaciones";
            botonPuntuaciones.UseVisualStyleBackColor = false;
            botonPuntuaciones.Click += botonPuntuaciones_Click;
            // 
            // buttonCargarPartida
            // 
            buttonCargarPartida.BackColor = Color.DodgerBlue;
            buttonCargarPartida.FlatStyle = FlatStyle.Popup;
            buttonCargarPartida.Font = new Font("Microsoft New Tai Lue", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonCargarPartida.ForeColor = Color.White;
            buttonCargarPartida.Location = new Point(452, 322);
            buttonCargarPartida.Margin = new Padding(3, 2, 3, 2);
            buttonCargarPartida.Name = "buttonCargarPartida";
            buttonCargarPartida.Size = new Size(209, 35);
            buttonCargarPartida.TabIndex = 5;
            buttonCargarPartida.Text = "Cargar partida";
            buttonCargarPartida.UseVisualStyleBackColor = false;
            buttonCargarPartida.Click += buttonCargarPartida_Click;
            // 
            // Menu_Principal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(1104, 505);
            Controls.Add(buttonCargarPartida);
            Controls.Add(botonPuntuaciones);
            Controls.Add(botonSalir);
            Controls.Add(botonIniciarPartida);
            Controls.Add(fondoMenuPrincipal);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "Menu_Principal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Flavio's Tank Adventures";
            ((System.ComponentModel.ISupportInitialize)fondoMenuPrincipal).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox fondoMenuPrincipal;
        private Button botonIniciarPartida;
        private Button botonSalir;
        private Button botonPuntuaciones;
        private Button buttonCargarPartida;
    }
}