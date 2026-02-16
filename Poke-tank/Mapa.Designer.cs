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
            botonNivel1 = new Button();
            botonNivel2 = new Button();
            botonNivel3 = new Button();
            botonSorpresaFlavio = new Button();
            botonFinalizar = new Button();
            SuspendLayout();
            // 
            // botonNivel1
            // 
            botonNivel1.BackColor = Color.Transparent;
            botonNivel1.BackgroundImage = Properties.Resources.nivel_11;
            botonNivel1.BackgroundImageLayout = ImageLayout.Stretch;
            botonNivel1.Location = new Point(370, 466);
            botonNivel1.Name = "botonNivel1";
            botonNivel1.Size = new Size(97, 52);
            botonNivel1.TabIndex = 0;
            botonNivel1.UseVisualStyleBackColor = false;
            // 
            // botonNivel2
            // 
            botonNivel2.BackColor = Color.Transparent;
            botonNivel2.BackgroundImage = Properties.Resources.nivel_2;
            botonNivel2.BackgroundImageLayout = ImageLayout.Stretch;
            botonNivel2.Location = new Point(90, 109);
            botonNivel2.Name = "botonNivel2";
            botonNivel2.Size = new Size(97, 52);
            botonNivel2.TabIndex = 1;
            botonNivel2.UseVisualStyleBackColor = false;
            // 
            // botonNivel3
            // 
            botonNivel3.BackColor = Color.Transparent;
            botonNivel3.BackgroundImage = Properties.Resources.nivel_3;
            botonNivel3.BackgroundImageLayout = ImageLayout.Stretch;
            botonNivel3.Location = new Point(452, 70);
            botonNivel3.Name = "botonNivel3";
            botonNivel3.Size = new Size(101, 46);
            botonNivel3.TabIndex = 2;
            botonNivel3.UseVisualStyleBackColor = false;
            // 
            // botonSorpresaFlavio
            // 
            botonSorpresaFlavio.Location = new Point(1162, 545);
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
            botonFinalizar.Location = new Point(1050, 30); // Movido un poco más al centro
            botonFinalizar.Name = "botonFinalizar";
            botonFinalizar.Size = new Size(140, 40);
            botonFinalizar.TabIndex = 4;
            botonFinalizar.Text = "Finalizar Aventura";
            botonFinalizar.UseVisualStyleBackColor = false;
            botonFinalizar.Click += botonFinalizar_Click;
            // 
            // Mapa
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.MAPA_FLAVIO_S_ADVENTURES_FONDO;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1262, 673);
            Controls.Add(botonFinalizar);
            Controls.Add(botonSorpresaFlavio);
            Controls.Add(botonNivel3);
            Controls.Add(botonNivel2);
            Controls.Add(botonNivel1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Mapa";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Flavio's Tank Adventure";
            // Aseguramos que el botón esté al frente
            botonFinalizar.BringToFront();
            ResumeLayout(false);
        }

        #endregion

        private Button botonNivel1;
        private Button botonNivel2;
        private Button botonNivel3;
        private Button botonSorpresaFlavio;
        private Button botonFinalizar;
    }
}