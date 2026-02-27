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
            botonNivel1.Location = new Point(324, 350);
            botonNivel1.Margin = new Padding(3, 2, 3, 2);
            botonNivel1.Name = "botonNivel1";
            botonNivel1.Size = new Size(85, 39);
            botonNivel1.TabIndex = 0;
            botonNivel1.UseVisualStyleBackColor = false;
            botonNivel1.Click += botonNivel1_Click;
            // 
            // botonNivel2
            // 
            botonNivel2.BackColor = Color.Transparent;
            botonNivel2.BackgroundImage = Properties.Resources.nivel_2;
            botonNivel2.BackgroundImageLayout = ImageLayout.Stretch;
            botonNivel2.Location = new Point(79, 82);
            botonNivel2.Margin = new Padding(3, 2, 3, 2);
            botonNivel2.Name = "botonNivel2";
            botonNivel2.Size = new Size(85, 39);
            botonNivel2.TabIndex = 1;
            botonNivel2.UseVisualStyleBackColor = false;
            botonNivel2.Click += botonNivel2_Click;
            // 
            // botonNivel3
            // 
            botonNivel3.BackColor = Color.Transparent;
            botonNivel3.BackgroundImage = Properties.Resources.nivel_3;
            botonNivel3.BackgroundImageLayout = ImageLayout.Stretch;
            botonNivel3.Location = new Point(396, 52);
            botonNivel3.Margin = new Padding(3, 2, 3, 2);
            botonNivel3.Name = "botonNivel3";
            botonNivel3.Size = new Size(88, 34);
            botonNivel3.TabIndex = 2;
            botonNivel3.UseVisualStyleBackColor = false;
            botonNivel3.Click += botonNivel3_Click;
            // 
            // botonSorpresaFlavio
            // 
            botonSorpresaFlavio.Location = new Point(1017, 409);
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
            // Mapa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.MAPA_FLAVIO_S_ADVENTURES_FONDO;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1104, 505);
            Controls.Add(botonFinalizar);
            Controls.Add(botonSorpresaFlavio);
            Controls.Add(botonNivel3);
            Controls.Add(botonNivel2);
            Controls.Add(botonNivel1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "Mapa";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Flavio's Tank Adventure";
            Load += Mapa_Load;
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
