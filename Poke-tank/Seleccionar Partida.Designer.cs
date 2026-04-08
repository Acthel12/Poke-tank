namespace Poke_tank
{
    partial class Seleccionar_Partida
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Seleccionar_Partida));
            dgvPartidas = new DataGridView();
            btnCargar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPartidas).BeginInit();
            SuspendLayout();
            // 
            // dgvPartidas
            // 
            dgvPartidas.AllowUserToAddRows = false;
            dgvPartidas.AllowUserToDeleteRows = false;
            dgvPartidas.AllowUserToResizeColumns = false;
            dgvPartidas.AllowUserToResizeRows = false;
            dgvPartidas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPartidas.Location = new Point(40, 120);
            dgvPartidas.Name = "dgvPartidas";
            dgvPartidas.ReadOnly = true;
            dgvPartidas.RowHeadersWidth = 51;
            dgvPartidas.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvPartidas.Size = new Size(785, 319);
            dgvPartidas.TabIndex = 0;
            // 
            // btnCargar
            // 
            btnCargar.BackColor = Color.FromArgb(2, 144, 69);
            btnCargar.FlatStyle = FlatStyle.Popup;
            btnCargar.ForeColor = Color.White;
            btnCargar.Location = new Point(40, 581);
            btnCargar.Name = "btnCargar";
            btnCargar.Size = new Size(160, 47);
            btnCargar.TabIndex = 2;
            btnCargar.Text = "Cargar partida ";
            btnCargar.UseVisualStyleBackColor = false;
            btnCargar.Click += bntCargar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(170, 21, 27);
            btnEliminar.FlatStyle = FlatStyle.Popup;
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(665, 581);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(160, 47);
            btnEliminar.TabIndex = 3;
            btnEliminar.Text = "Eiminar Partida ";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += bntEliminar_Click;
            // 
            // Seleccionar_Partida
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo_partidas;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(860, 673);
            Controls.Add(btnEliminar);
            Controls.Add(btnCargar);
            Controls.Add(dgvPartidas);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Seleccionar_Partida";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Seleccionar Partida";
            Load += Seleccionar_Partida_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPartidas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvPartidas;
        private Button btnCargar;
        private Button btnEliminar;
    }
}