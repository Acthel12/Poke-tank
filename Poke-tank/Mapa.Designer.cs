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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.BackgroundImage = Properties.Resources.nivel_11;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Location = new Point(370, 466);
            button1.Name = "button1";
            button1.Size = new Size(97, 52);
            button1.TabIndex = 0;
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.Transparent;
            button2.BackgroundImage = Properties.Resources.nivel_2;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.Location = new Point(90, 109);
            button2.Name = "button2";
            button2.Size = new Size(97, 52);
            button2.TabIndex = 1;
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.Transparent;
            button3.BackgroundImage = Properties.Resources.nivel_3;
            button3.BackgroundImageLayout = ImageLayout.Stretch;
            button3.Location = new Point(452, 70);
            button3.Name = "button3";
            button3.Size = new Size(101, 46);
            button3.TabIndex = 2;
            button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.Location = new Point(1111, 551);
            button4.Name = "button4";
            button4.Size = new Size(5, 5);
            button4.TabIndex = 3;
            button4.UseVisualStyleBackColor = true;
            // 
            // Mapa
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.MAPA_FLAVIO_S_ADVENTURES_FONDO;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1207, 680);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Mapa";
            Text = "Flavio's Tank Adventure";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}