namespace Poke_tank
{
    partial class nivel_3
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
            components = new System.ComponentModel.Container();
            GameTimer = new System.Windows.Forms.Timer(components);
            aviso = new Label();
            timerAviso = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // GameTimer
            // 
            GameTimer.Enabled = true;
            GameTimer.Interval = 20;
            GameTimer.Tick += GameTimer_Tick;
            // 
            // aviso
            // 
            aviso.AutoSize = true;
            aviso.BackColor = Color.Transparent;
            aviso.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            aviso.ForeColor = Color.Red;
            aviso.Location = new Point(11, 7);
            aviso.Name = "aviso";
            aviso.Size = new Size(45, 20);
            aviso.TabIndex = 0;
            aviso.Text = "aviso";
            aviso.Visible = false;
            // 
            // timerAviso
            // 
            timerAviso.Interval = 1500;
            timerAviso.Tick += timerAviso_Tick;
            // 
            // nivel_3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1430, 895);
            Controls.Add(aviso);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "Batalla_Mejorada";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Batalla_Mejorada";
            Paint += Batalla_Paint;
            KeyDown += Batalla_KeyDown;
            KeyUp += Batalla_KeyUp;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer GameTimer;
        private Label aviso;
        private System.Windows.Forms.Timer timerAviso;
    }
}