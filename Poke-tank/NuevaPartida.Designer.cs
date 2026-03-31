using System.Security.Cryptography.Xml;

namespace Poke_tank
{
    partial class NuevaPartida
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NuevaPartida));
            textBoxNombre = new TextBox();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            radioButtonFacil = new RadioButton();
            groupBox1 = new GroupBox();
            radioButtonDificil = new RadioButton();
            radioButtonNormal = new RadioButton();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxNombre
            // 
            textBoxNombre.Font = new Font("Microsoft New Tai Lue", 12F);
            textBoxNombre.Location = new Point(67, 92);
            textBoxNombre.Margin = new Padding(3, 4, 3, 4);
            textBoxNombre.Name = "textBoxNombre";
            textBoxNombre.Size = new Size(387, 34);
            textBoxNombre.TabIndex = 3;
            textBoxNombre.Text = "Comandante Flavio";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Microsoft New Tai Lue", 12F, FontStyle.Bold);
            label1.Location = new Point(67, 52);
            label1.Name = "label1";
            label1.Size = new Size(208, 27);
            label1.TabIndex = 4;
            label1.Text = "Introduce tu nombre";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Microsoft New Tai Lue", 12F, FontStyle.Bold);
            label2.Location = new Point(67, 365);
            label2.Name = "label2";
            label2.Size = new Size(205, 27);
            label2.TabIndex = 4;
            label2.Text = "Ingresa La dificultad";
            // 
            // button1
            // 
            button1.BackColor = Color.DodgerBlue;
            button1.Font = new Font("Microsoft New Tai Lue", 20F);
            button1.Location = new Point(657, 429);
            button1.Name = "button1";
            button1.Size = new Size(156, 67);
            button1.TabIndex = 5;
            button1.Text = "Crear";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // radioButtonFacil
            // 
            radioButtonFacil.AutoSize = true;
            radioButtonFacil.Checked = true;
            radioButtonFacil.Location = new Point(6, 26);
            radioButtonFacil.Name = "radioButtonFacil";
            radioButtonFacil.Size = new Size(59, 24);
            radioButtonFacil.TabIndex = 6;
            radioButtonFacil.TabStop = true;
            radioButtonFacil.Text = "Facil";
            radioButtonFacil.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Transparent;
            groupBox1.Controls.Add(radioButtonDificil);
            groupBox1.Controls.Add(radioButtonNormal);
            groupBox1.Controls.Add(radioButtonFacil);
            groupBox1.Location = new Point(146, 395);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(318, 166);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            // 
            // radioButtonDificil
            // 
            radioButtonDificil.AutoSize = true;
            radioButtonDificil.Location = new Point(6, 107);
            radioButtonDificil.Name = "radioButtonDificil";
            radioButtonDificil.Size = new Size(69, 24);
            radioButtonDificil.TabIndex = 9;
            radioButtonDificil.TabStop = true;
            radioButtonDificil.Text = "Dificil";
            radioButtonDificil.UseVisualStyleBackColor = true;
            // 
            // radioButtonNormal
            // 
            radioButtonNormal.AutoSize = true;
            radioButtonNormal.Location = new Point(6, 67);
            radioButtonNormal.Name = "radioButtonNormal";
            radioButtonNormal.Size = new Size(80, 24);
            radioButtonNormal.TabIndex = 8;
            radioButtonNormal.TabStop = true;
            radioButtonNormal.Text = "Normal";
            radioButtonNormal.UseVisualStyleBackColor = true;
            // 
            // NuevaPartida
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.wmremove_transformed;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(914, 600);
            Controls.Add(label2);
            Controls.Add(groupBox1);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(textBoxNombre);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "NuevaPartida";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Nueva Partida";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBoxNombre;
        private Label label1;
        private Label label2;
        private Button button1;
        private RadioButton radioButtonFacil;
        private GroupBox groupBox1;
        private RadioButton radioButtonDificil;
        private RadioButton radioButtonNormal;
    }
}