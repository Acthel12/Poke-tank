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
            buttonEstadisticas = new Button();
            SuspendLayout();
            // 
            // botonSorpresaFlavio
            // 
            botonSorpresaFlavio.Location = new Point(1181, 585);
            botonSorpresaFlavio.Name = "botonSorpresaFlavio";
            botonSorpresaFlavio.Size = new Size(5, 5);
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
            botonFinalizar.Location = new Point(1019, 29);
            botonFinalizar.Name = "botonFinalizar";
            botonFinalizar.Size = new Size(171, 40);
            botonFinalizar.TabIndex = 4;
            botonFinalizar.Text = "Finalizar aventura";
            botonFinalizar.UseVisualStyleBackColor = false;
            botonFinalizar.Click += botonFinalizar_Click;
            // 
            // buttonEstadisticas
            // 
            buttonEstadisticas.BackColor = Color.FromArgb(128, 128, 255);
            buttonEstadisticas.FlatStyle = FlatStyle.Flat;
            buttonEstadisticas.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonEstadisticas.ForeColor = Color.White;
            buttonEstadisticas.Location = new Point(786, 29);
            buttonEstadisticas.Name = "buttonEstadisticas";
            buttonEstadisticas.Size = new Size(171, 40);
            buttonEstadisticas.TabIndex = 5;
            buttonEstadisticas.Text = "Estadisticas";
            buttonEstadisticas.UseVisualStyleBackColor = false;
            buttonEstadisticas.Click += buttonEstadisticas_Click;
            // 
            // Mapa
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.MAPA_FLAVIO_ADVENTURES;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1262, 673);
            Controls.Add(buttonEstadisticas);
            Controls.Add(botonFinalizar);
            Controls.Add(botonSorpresaFlavio);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
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
        private Button buttonEstadisticas;
    }
}
