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
            ((System.ComponentModel.ISupportInitialize)pictureBoxEnemigo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxJugador).BeginInit();
            groupBoxComandos.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBoxEnemigo
            // 
            pictureBoxEnemigo.BackColor = Color.Transparent;
            pictureBoxEnemigo.Image = Properties.Resources.T72;
            pictureBoxEnemigo.Location = new Point(733, 190);
            pictureBoxEnemigo.Name = "pictureBoxEnemigo";
            pictureBoxEnemigo.Size = new Size(500, 285);
            pictureBoxEnemigo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxEnemigo.TabIndex = 0;
            pictureBoxEnemigo.TabStop = false;
            // 
            // pictureBoxJugador
            // 
            pictureBoxJugador.BackColor = Color.Transparent;
            pictureBoxJugador.Image = Properties.Resources.M1Abrams_2;
            pictureBoxJugador.Location = new Point(53, 190);
            pictureBoxJugador.Name = "pictureBoxJugador";
            pictureBoxJugador.Size = new Size(500, 285);
            pictureBoxJugador.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxJugador.TabIndex = 1;
            pictureBoxJugador.TabStop = false;
            // 
            // progressBarVidaEnemigo
            // 
            progressBarVidaEnemigo.ForeColor = Color.Firebrick;
            progressBarVidaEnemigo.Location = new Point(733, 161);
            progressBarVidaEnemigo.Name = "progressBarVidaEnemigo";
            progressBarVidaEnemigo.Size = new Size(500, 23);
            progressBarVidaEnemigo.TabIndex = 2;
            // 
            // labelNombreEnemigo
            // 
            labelNombreEnemigo.AutoSize = true;
            labelNombreEnemigo.Location = new Point(733, 143);
            labelNombreEnemigo.Name = "labelNombreEnemigo";
            labelNombreEnemigo.Size = new Size(38, 15);
            labelNombreEnemigo.TabIndex = 3;
            labelNombreEnemigo.Text = "label1";
            // 
            // progressBarVidaJugador
            // 
            progressBarVidaJugador.Location = new Point(53, 161);
            progressBarVidaJugador.Name = "progressBarVidaJugador";
            progressBarVidaJugador.Size = new Size(500, 23);
            progressBarVidaJugador.TabIndex = 4;
            // 
            // labelNombreJugador
            // 
            labelNombreJugador.AutoSize = true;
            labelNombreJugador.Location = new Point(53, 143);
            labelNombreJugador.Name = "labelNombreJugador";
            labelNombreJugador.Size = new Size(38, 15);
            labelNombreJugador.TabIndex = 5;
            labelNombreJugador.Text = "label1";
            // 
            // groupBoxComandos
            // 
            groupBoxComandos.Controls.Add(buttonHuir);
            groupBoxComandos.Controls.Add(buttonReparar);
            groupBoxComandos.Controls.Add(buttonDefensa);
            groupBoxComandos.Controls.Add(buttonDisparar);
            groupBoxComandos.Location = new Point(53, 488);
            groupBoxComandos.Name = "groupBoxComandos";
            groupBoxComandos.Size = new Size(500, 137);
            groupBoxComandos.TabIndex = 6;
            groupBoxComandos.TabStop = false;
            groupBoxComandos.Text = "Comandos";
            // 
            // buttonHuir
            // 
            buttonHuir.Location = new Point(6, 107);
            buttonHuir.Name = "buttonHuir";
            buttonHuir.Size = new Size(488, 23);
            buttonHuir.TabIndex = 3;
            buttonHuir.Text = "Huir";
            buttonHuir.UseVisualStyleBackColor = true;
            buttonHuir.Click += buttonHuir_Click;
            // 
            // buttonReparar
            // 
            buttonReparar.Location = new Point(6, 80);
            buttonReparar.Name = "buttonReparar";
            buttonReparar.Size = new Size(488, 23);
            buttonReparar.TabIndex = 2;
            buttonReparar.Text = "Reparar";
            buttonReparar.UseVisualStyleBackColor = true;
            buttonReparar.Click += buttonReparar_Click;
            // 
            // buttonDefensa
            // 
            buttonDefensa.Location = new Point(6, 51);
            buttonDefensa.Name = "buttonDefensa";
            buttonDefensa.Size = new Size(488, 23);
            buttonDefensa.TabIndex = 1;
            buttonDefensa.Text = "Defensa";
            buttonDefensa.UseVisualStyleBackColor = true;
            buttonDefensa.Click += buttonDefensa_Click;
            // 
            // buttonDisparar
            // 
            buttonDisparar.Location = new Point(6, 22);
            buttonDisparar.Name = "buttonDisparar";
            buttonDisparar.Size = new Size(488, 23);
            buttonDisparar.TabIndex = 0;
            buttonDisparar.Text = "Disparar";
            buttonDisparar.UseVisualStyleBackColor = true;
            buttonDisparar.Click += buttonDisparar_Click;
            // 
            // richTextBoxCombatLog
            // 
            richTextBoxCombatLog.Location = new Point(733, 488);
            richTextBoxCombatLog.Name = "richTextBoxCombatLog";
            richTextBoxCombatLog.ReadOnly = true;
            richTextBoxCombatLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBoxCombatLog.Size = new Size(500, 130);
            richTextBoxCombatLog.TabIndex = 7;
            richTextBoxCombatLog.Text = "";
            // 
            // FormBatalla
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1264, 681);
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
            MaximizeBox = false;
            MaximumSize = new Size(1280, 720);
            MinimumSize = new Size(1280, 720);
            Name = "FormBatalla";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Batalla";
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
    }
}