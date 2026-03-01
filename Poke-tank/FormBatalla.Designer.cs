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
            buttonAtacar = new Button();
            richTextBoxCombatLog = new RichTextBox();
            label1 = new Label();
            groupBoxAtaques = new GroupBox();
            buttonVolver = new Button();
            buttonHumo = new Button();
            buttonOrugas = new Button();
            buttonDisparo = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEnemigo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxJugador).BeginInit();
            groupBoxComandos.SuspendLayout();
            groupBoxAtaques.SuspendLayout();
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
            progressBarVidaEnemigo.Location = new Point(733, 152);
            progressBarVidaEnemigo.Name = "progressBarVidaEnemigo";
            progressBarVidaEnemigo.Size = new Size(500, 23);
            progressBarVidaEnemigo.TabIndex = 2;
            // 
            // labelNombreEnemigo
            // 
            labelNombreEnemigo.AutoSize = true;
            labelNombreEnemigo.Font = new Font("Segoe UI", 12F);
            labelNombreEnemigo.Location = new Point(733, 119);
            labelNombreEnemigo.Name = "labelNombreEnemigo";
            labelNombreEnemigo.Size = new Size(52, 21);
            labelNombreEnemigo.TabIndex = 3;
            labelNombreEnemigo.Text = "label1";
            // 
            // progressBarVidaJugador
            // 
            progressBarVidaJugador.Location = new Point(53, 152);
            progressBarVidaJugador.Name = "progressBarVidaJugador";
            progressBarVidaJugador.Size = new Size(500, 23);
            progressBarVidaJugador.TabIndex = 4;
            // 
            // labelNombreJugador
            // 
            labelNombreJugador.AutoSize = true;
            labelNombreJugador.Font = new Font("Segoe UI", 12F);
            labelNombreJugador.Location = new Point(53, 119);
            labelNombreJugador.Name = "labelNombreJugador";
            labelNombreJugador.Size = new Size(52, 21);
            labelNombreJugador.TabIndex = 5;
            labelNombreJugador.Text = "label1";
            // 
            // groupBoxComandos
            // 
            groupBoxComandos.Controls.Add(buttonHuir);
            groupBoxComandos.Controls.Add(buttonReparar);
            groupBoxComandos.Controls.Add(buttonDefensa);
            groupBoxComandos.Controls.Add(buttonAtacar);
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
            // buttonAtacar
            // 
            buttonAtacar.Location = new Point(6, 22);
            buttonAtacar.Name = "buttonAtacar";
            buttonAtacar.Size = new Size(488, 23);
            buttonAtacar.TabIndex = 0;
            buttonAtacar.Text = "Atacar";
            buttonAtacar.UseVisualStyleBackColor = true;
            buttonAtacar.Click += buttonAtacar_Click;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(454, 25);
            label1.Name = "label1";
            label1.Size = new Size(311, 37);
            label1.TabIndex = 8;
            label1.Text = "¡Derrota a tu enemigo!";
            // 
            // groupBoxAtaques
            // 
            groupBoxAtaques.Controls.Add(buttonVolver);
            groupBoxAtaques.Controls.Add(buttonHumo);
            groupBoxAtaques.Controls.Add(buttonOrugas);
            groupBoxAtaques.Controls.Add(buttonDisparo);
            groupBoxAtaques.Location = new Point(53, 488);
            groupBoxAtaques.Name = "groupBoxAtaques";
            groupBoxAtaques.Size = new Size(500, 137);
            groupBoxAtaques.TabIndex = 9;
            groupBoxAtaques.TabStop = false;
            groupBoxAtaques.Text = "Ataques";
            groupBoxAtaques.Visible = false;
            // 
            // buttonVolver
            // 
            buttonVolver.Location = new Point(6, 107);
            buttonVolver.Name = "buttonVolver";
            buttonVolver.Size = new Size(488, 23);
            buttonVolver.TabIndex = 3;
            buttonVolver.Text = "Volver";
            buttonVolver.UseVisualStyleBackColor = true;
            buttonVolver.Click += buttonVolver_Click;
            // 
            // buttonHumo
            // 
            buttonHumo.Location = new Point(6, 78);
            buttonHumo.Name = "buttonHumo";
            buttonHumo.Size = new Size(488, 23);
            buttonHumo.TabIndex = 2;
            buttonHumo.Text = "Cortina De Humo";
            buttonHumo.UseVisualStyleBackColor = true;
            buttonHumo.Click += buttonHumo_Click;
            // 
            // buttonOrugas
            // 
            buttonOrugas.Location = new Point(6, 51);
            buttonOrugas.Name = "buttonOrugas";
            buttonOrugas.Size = new Size(488, 23);
            buttonOrugas.TabIndex = 1;
            buttonOrugas.Text = "Ataque a las Orugas";
            buttonOrugas.UseVisualStyleBackColor = true;
            buttonOrugas.Click += buttonOrugas_Click;
            // 
            // buttonDisparo
            // 
            buttonDisparo.Location = new Point(6, 22);
            buttonDisparo.Name = "buttonDisparo";
            buttonDisparo.Size = new Size(488, 23);
            buttonDisparo.TabIndex = 0;
            buttonDisparo.Text = "Disparo";
            buttonDisparo.UseVisualStyleBackColor = true;
            buttonDisparo.Click += buttonDisparo_Click;
            // 
            // FormBatalla
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondoNivel1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1251, 673);
            Controls.Add(groupBoxAtaques);
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
            MaximizeBox = false;
            MaximumSize = new Size(1280, 718);
            MinimumSize = new Size(1196, 554);
            Name = "FormBatalla";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Flavio's Tank Adventures";
            FormClosed += FormBatalla_FormClosed;
            Load += FormBatalla_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxEnemigo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxJugador).EndInit();
            groupBoxComandos.ResumeLayout(false);
            groupBoxAtaques.ResumeLayout(false);
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
        private Button buttonAtacar;
        private RichTextBox richTextBoxCombatLog;
        private Button buttonHuir;
        private Label label1;
        private GroupBox groupBoxAtaques;
        private Button buttonVolver;
        private Button buttonHumo;
        private Button buttonOrugas;
        private Button buttonDisparo;
    }
}