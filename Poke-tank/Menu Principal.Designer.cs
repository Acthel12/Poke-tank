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
            botonCargarPartida = new Button();
            botonSalir = new Button();
            botonPuntuaciones = new Button();
            ((System.ComponentModel.ISupportInitialize)fondoMenuPrincipal).BeginInit();
            SuspendLayout();
            // 
            // fondoMenuPrincipal
            // 
            fondoMenuPrincipal.Image = Properties.Resources.def_GIF_FONDO_MENU_PPAL_FLAVIO_ADVENTURES;
            fondoMenuPrincipal.Location = new Point(0, -67);
            fondoMenuPrincipal.Name = "fondoMenuPrincipal";
            fondoMenuPrincipal.Size = new Size(1296, 853);
            fondoMenuPrincipal.SizeMode = PictureBoxSizeMode.Zoom;
            fondoMenuPrincipal.TabIndex = 0;
            fondoMenuPrincipal.TabStop = false;
            // 
            // botonIniciarPartida
            // 
            botonIniciarPartida.BackColor = Color.DodgerBlue;
            botonIniciarPartida.FlatStyle = FlatStyle.Popup;
            botonIniciarPartida.Font = new Font("Berlin Sans FB Demi", 12F, FontStyle.Bold);
            botonIniciarPartida.ForeColor = Color.White;
            botonIniciarPartida.Location = new Point(516, 375);
            botonIniciarPartida.Name = "botonIniciarPartida";
            botonIniciarPartida.Size = new Size(239, 47);
            botonIniciarPartida.TabIndex = 1;
            botonIniciarPartida.Text = "Iniciar nueva partida";
            botonIniciarPartida.UseVisualStyleBackColor = false;
            botonIniciarPartida.Click += botonIniciarPartida_Click;
            // 
            // botonCargarPartida
            // 
            botonCargarPartida.BackColor = Color.DodgerBlue;
            botonCargarPartida.FlatStyle = FlatStyle.Popup;
            botonCargarPartida.Font = new Font("Berlin Sans FB Demi", 12F, FontStyle.Bold);
            botonCargarPartida.ForeColor = Color.White;
            botonCargarPartida.Location = new Point(516, 441);
            botonCargarPartida.Name = "botonCargarPartida";
            botonCargarPartida.Size = new Size(239, 47);
            botonCargarPartida.TabIndex = 2;
            botonCargarPartida.Text = "Cargar partida";
            botonCargarPartida.UseVisualStyleBackColor = false;
            botonCargarPartida.Click += botonCargarPartida_Click_1;
            // 
            // botonSalir
            // 
            botonSalir.BackColor = Color.DodgerBlue;
            botonSalir.FlatStyle = FlatStyle.Popup;
            botonSalir.Font = new Font("Berlin Sans FB Demi", 12F, FontStyle.Bold);
            botonSalir.ForeColor = Color.White;
            botonSalir.Location = new Point(516, 586);
            botonSalir.Name = "botonSalir";
            botonSalir.Size = new Size(239, 47);
            botonSalir.TabIndex = 3;
            botonSalir.Text = "Salir";
            botonSalir.UseVisualStyleBackColor = false;
            // 
            // botonPuntuaciones
            // 
            botonPuntuaciones.BackColor = Color.DodgerBlue;
            botonPuntuaciones.FlatStyle = FlatStyle.Popup;
            botonPuntuaciones.Font = new Font("Berlin Sans FB Demi", 12F, FontStyle.Bold);
            botonPuntuaciones.ForeColor = Color.White;
            botonPuntuaciones.Location = new Point(516, 511);
            botonPuntuaciones.Name = "botonPuntuaciones";
            botonPuntuaciones.Size = new Size(239, 47);
            botonPuntuaciones.TabIndex = 4;
            botonPuntuaciones.Text = "Ver puntuaciones";
            botonPuntuaciones.UseVisualStyleBackColor = false;
            // 
            // Menu_Principal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(1262, 673);
            Controls.Add(botonPuntuaciones);
            Controls.Add(botonSalir);
            Controls.Add(botonCargarPartida);
            Controls.Add(botonIniciarPartida);
            Controls.Add(fondoMenuPrincipal);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
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
        private Button botonCargarPartida;
        private Button botonSalir;
        private Button botonPuntuaciones;
    }
}