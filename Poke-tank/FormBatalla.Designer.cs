namespace Poke_tank
{
    partial class FormBatalla
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBatalla));
            pictureBoxEnemigo = new PictureBox();
            pictureBoxJugador = new PictureBox();
            progressBarVidaEnemigo = new ProgressBar();
            labelNombreEnemigo = new Label();
            progressBarVidaJugador = new ProgressBar();
            labelNombreJugador = new Label();
            groupBoxComandos = new GroupBox();
            buttonHuir = new Button();
            buttonReparar = new Button();
            buttonDefensa = new Button();
            buttonDisparar = new Button();
            richTextBoxCombatLog = new RichTextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEnemigo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxJugador).BeginInit();
            groupBoxComandos.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBoxEnemigo
            // 
            pictureBoxEnemigo.BackColor = Color.Transparent;
            pictureBoxEnemigo.Image = Properties.Resources.T72;
            pictureBoxEnemigo.Location = new Point(838, 253);
            pictureBoxEnemigo.Margin = new Padding(3, 4, 3, 4);
            pictureBoxEnemigo.Name = "pictureBoxEnemigo";
            pictureBoxEnemigo.Size = new Size(571, 380);
            pictureBoxEnemigo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxEnemigo.TabIndex = 0;
            pictureBoxEnemigo.TabStop = false;
            // 
            // pictureBoxJugador
            // 
            pictureBoxJugador.BackColor = Color.Transparent;
            pictureBoxJugador.Image = Properties.Resources.M1Abrams_2;
            pictureBoxJugador.Location = new Point(61, 253);
            pictureBoxJugador.Margin = new Padding(3, 4, 3, 4);
            pictureBoxJugador.Name = "pictureBoxJugador";
            pictureBoxJugador.Size = new Size(571, 380);
            pictureBoxJugador.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxJugador.TabIndex = 1;
            pictureBoxJugador.TabStop = false;
            // 
            // progressBarVidaEnemigo
            // 
            progressBarVidaEnemigo.ForeColor = Color.Firebrick;
            progressBarVidaEnemigo.Location = new Point(838, 203);
            progressBarVidaEnemigo.Margin = new Padding(3, 4, 3, 4);
            progressBarVidaEnemigo.Name = "progressBarVidaEnemigo";
            progressBarVidaEnemigo.Size = new Size(571, 31);
            progressBarVidaEnemigo.TabIndex = 2;
            // 
            // labelNombreEnemigo
            // 
            labelNombreEnemigo.AutoSize = true;
            labelNombreEnemigo.Font = new Font("Segoe UI", 12F);
            labelNombreEnemigo.Location = new Point(838, 159);
            labelNombreEnemigo.Name = "labelNombreEnemigo";
            labelNombreEnemigo.Size = new Size(65, 28);
            labelNombreEnemigo.TabIndex = 3;
            labelNombreEnemigo.Text = "label1";
            // 
            // progressBarVidaJugador
            // 
            progressBarVidaJugador.Location = new Point(61, 203);
            progressBarVidaJugador.Margin = new Padding(3, 4, 3, 4);
            progressBarVidaJugador.Name = "progressBarVidaJugador";
            progressBarVidaJugador.Size = new Size(571, 31);
            progressBarVidaJugador.TabIndex = 4;
            // 
            // labelNombreJugador
            // 
            labelNombreJugador.AutoSize = true;
            labelNombreJugador.Font = new Font("Segoe UI", 12F);
            labelNombreJugador.Location = new Point(61, 159);
            labelNombreJugador.Name = "labelNombreJugador";
            labelNombreJugador.Size = new Size(65, 28);
            labelNombreJugador.TabIndex = 5;
            labelNombreJugador.Text = "label1";
            // 
            // groupBoxComandos
            // 
            groupBoxComandos.Controls.Add(buttonHuir);
            groupBoxComandos.Controls.Add(buttonReparar);
            groupBoxComandos.Controls.Add(buttonDefensa);
            groupBoxComandos.Controls.Add(buttonDisparar);
            groupBoxComandos.Location = new Point(61, 651);
            groupBoxComandos.Margin = new Padding(3, 4, 3, 4);
            groupBoxComandos.Name = "groupBoxComandos";
            groupBoxComandos.Padding = new Padding(3, 4, 3, 4);
            groupBoxComandos.Size = new Size(571, 183);
            groupBoxComandos.TabIndex = 6;
            groupBoxComandos.TabStop = false;
            groupBoxComandos.Text = "Comandos";
            // 
            // buttonHuir
            // 
            buttonHuir.Location = new Point(7, 143);
            buttonHuir.Margin = new Padding(3, 4, 3, 4);
            buttonHuir.Name = "buttonHuir";
            buttonHuir.Size = new Size(558, 31);
            buttonHuir.TabIndex = 3;
            buttonHuir.Text = "Huir";
            buttonHuir.UseVisualStyleBackColor = true;
            buttonHuir.Click += buttonHuir_Click;
            // 
            // buttonReparar
            // 
            buttonReparar.Location = new Point(7, 107);
            buttonReparar.Margin = new Padding(3, 4, 3, 4);
            buttonReparar.Name = "buttonReparar";
            buttonReparar.Size = new Size(558, 31);
            buttonReparar.TabIndex = 2;
            buttonReparar.Text = "Reparar";
            buttonReparar.UseVisualStyleBackColor = true;
            buttonReparar.Click += buttonReparar_Click;
            // 
            // buttonDefensa
            // 
            buttonDefensa.Location = new Point(7, 68);
            buttonDefensa.Margin = new Padding(3, 4, 3, 4);
            buttonDefensa.Name = "buttonDefensa";
            buttonDefensa.Size = new Size(558, 31);
            buttonDefensa.TabIndex = 1;
            buttonDefensa.Text = "Defensa";
            buttonDefensa.UseVisualStyleBackColor = true;
            buttonDefensa.Click += buttonDefensa_Click;
            // 
            // buttonDisparar
            // 
            buttonDisparar.Location = new Point(7, 29);
            buttonDisparar.Margin = new Padding(3, 4, 3, 4);
            buttonDisparar.Name = "buttonDisparar";
            buttonDisparar.Size = new Size(558, 31);
            buttonDisparar.TabIndex = 0;
            buttonDisparar.Text = "Disparar";
            buttonDisparar.UseVisualStyleBackColor = true;
            buttonDisparar.Click += buttonDisparar_Click;
            // 
            // richTextBoxCombatLog
            // 
            richTextBoxCombatLog.Location = new Point(838, 651);
            richTextBoxCombatLog.Margin = new Padding(3, 4, 3, 4);
            richTextBoxCombatLog.Name = "richTextBoxCombatLog";
            richTextBoxCombatLog.ReadOnly = true;
            richTextBoxCombatLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBoxCombatLog.Size = new Size(571, 172);
            richTextBoxCombatLog.TabIndex = 7;
            richTextBoxCombatLog.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(519, 33);
            label1.Name = "label1";
            label1.Size = new Size(384, 46);
            label1.TabIndex = 8;
            label1.Text = "¡Derrota a tu enemigo!";
            // 
            // FormBatalla
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondoNivel1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1442, 897);
            Controls.Add(label1);
            Controls.Add(richTextBoxCombatLog);
            Controls.Add(groupBoxComandos);
            Controls.Add(labelNombreJugador);
            Controls.Add(progressBarVidaJugador);
            Controls.Add(labelNombreEnemigo);
            Controls.Add(progressBarVidaEnemigo);
            Controls.Add(pictureBoxJugador);
            Controls.Add(pictureBoxEnemigo);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MaximumSize = new Size(1460, 944);
            MinimumSize = new Size(1460, 944);
            Name = "FormBatalla";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Flavio's Tank Adventures";
            Load += FormBatalla_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxEnemigo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxJugador).EndInit();
            groupBoxComandos.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxEnemigo;
        private PictureBox pictureBoxJugador;
        private ProgressBar progressBarVidaEnemigo;
        private Label labelNombreEnemigo;
        private ProgressBar progressBarVidaJugador;
        private Label labelNombreJugador;
        private GroupBox groupBoxComandos;
        private Button buttonReparar;
        private Button buttonDefensa;
        private Button buttonDisparar;
        private RichTextBox richTextBoxCombatLog;
        private Button buttonHuir;
        private Label label1;
    }
}