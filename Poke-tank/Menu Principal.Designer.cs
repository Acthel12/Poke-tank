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
            pictureBox1 = new PictureBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.def_GIF_FONDO_MENU_PPAL_FLAVIO_ADVENTURES;
            pictureBox1.Location = new Point(-35, -62);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1296, 853);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.BackColor = Color.DodgerBlue;
            button1.FlatStyle = FlatStyle.Popup;
            button1.ForeColor = Color.White;
            button1.Location = new Point(516, 371);
            button1.Name = "button1";
            button1.Size = new Size(160, 47);
            button1.TabIndex = 1;
            button1.Text = "Iniciar nueva partida";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.DodgerBlue;
            button2.FlatStyle = FlatStyle.Popup;
            button2.ForeColor = Color.White;
            button2.Location = new Point(516, 442);
            button2.Name = "button2";
            button2.Size = new Size(160, 47);
            button2.TabIndex = 2;
            button2.Text = "no se";
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.DodgerBlue;
            button3.FlatStyle = FlatStyle.Popup;
            button3.ForeColor = Color.White;
            button3.Location = new Point(516, 523);
            button3.Name = "button3";
            button3.Size = new Size(160, 47);
            button3.TabIndex = 3;
            button3.Text = "Salir";
            button3.UseVisualStyleBackColor = false;
            // 
            // Menu_Principal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(1207, 680);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Menu_Principal";
            Text = "Flavio's Tank Adventures";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}