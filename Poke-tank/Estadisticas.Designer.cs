namespace Poke_tank
{
    partial class Estadisticas
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
            labelNombre = new Label();
            labelVida = new Label();
            labelAtaque = new Label();
            labelDinero = new Label();
            labelDerrotados = new Label();
            SuspendLayout();
            // 
            // labelNombre
            // 
            labelNombre.AutoSize = true;
            labelNombre.BackColor = Color.Transparent;
            labelNombre.ForeColor = SystemColors.Control;
            labelNombre.Location = new Point(322, 393);
            labelNombre.Name = "labelNombre";
            labelNombre.Size = new Size(64, 20);
            labelNombre.TabIndex = 0;
            labelNombre.Text = "Nombre";
            // 
            // labelVida
            // 
            labelVida.AutoSize = true;
            labelVida.BackColor = Color.Transparent;
            labelVida.ForeColor = SystemColors.Control;
            labelVida.Location = new Point(285, 430);
            labelVida.Name = "labelVida";
            labelVida.Size = new Size(39, 20);
            labelVida.TabIndex = 1;
            labelVida.Text = "Vida";
            // 
            // labelAtaque
            // 
            labelAtaque.AutoSize = true;
            labelAtaque.BackColor = Color.Transparent;
            labelAtaque.ForeColor = SystemColors.Control;
            labelAtaque.Location = new Point(311, 464);
            labelAtaque.Name = "labelAtaque";
            labelAtaque.Size = new Size(57, 20);
            labelAtaque.TabIndex = 2;
            labelAtaque.Text = "Ataque";
            // 
            // labelDinero
            // 
            labelDinero.AutoSize = true;
            labelDinero.BackColor = Color.Transparent;
            labelDinero.ForeColor = SystemColors.Control;
            labelDinero.Location = new Point(306, 498);
            labelDinero.Name = "labelDinero";
            labelDinero.Size = new Size(54, 20);
            labelDinero.TabIndex = 3;
            labelDinero.Text = "Dinero";
            // 
            // labelDerrotados
            // 
            labelDerrotados.AutoSize = true;
            labelDerrotados.BackColor = Color.Transparent;
            labelDerrotados.ForeColor = SystemColors.Control;
            labelDerrotados.Location = new Point(354, 535);
            labelDerrotados.Name = "labelDerrotados";
            labelDerrotados.Size = new Size(84, 20);
            labelDerrotados.TabIndex = 4;
            labelDerrotados.Text = "Derrotados";
            // 
            // Estadisticas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.estadisticas;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(592, 824);
            Controls.Add(labelDerrotados);
            Controls.Add(labelDinero);
            Controls.Add(labelAtaque);
            Controls.Add(labelVida);
            Controls.Add(labelNombre);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Estadisticas";
            Text = "Estadisticas";
            Load += Estadisticas_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelNombre;
        private Label labelVida;
        private Label labelAtaque;
        private Label labelDinero;
        private Label labelDerrotados;
    }
}