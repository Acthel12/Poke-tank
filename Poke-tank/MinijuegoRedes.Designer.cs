namespace Poke_tank
{
    partial class MinijuegoRedes
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
            cursorTimer = new System.Windows.Forms.Timer(components);
            timerGlobal = new System.Windows.Forms.Timer(components);
            estudiantesTimer = new System.Windows.Forms.Timer(components);
            animSalidaTimer = new System.Windows.Forms.Timer(components);
            mazoTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // cursorTimer
            // 
            cursorTimer.Enabled = true;
            cursorTimer.Interval = 10;
            // 
            // timerGlobal
            // 
            timerGlobal.Enabled = true;
            timerGlobal.Interval = 1000;
            // 
            // estudiantesTimer
            // 
            estudiantesTimer.Interval = 500;
            // 
            // animSalidaTimer
            // 
            animSalidaTimer.Interval = 2000;
            // 
            // mazoTimer
            // 
            mazoTimer.Interval = 300;
            // 
            // MinijuegoRedes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 673);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MinijuegoRedes";
            Text = "MinijuegoRedes";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer cursorTimer;
        private System.Windows.Forms.Timer timerGlobal;
        private System.Windows.Forms.Timer estudiantesTimer;
        private System.Windows.Forms.Timer animSalidaTimer;
        private System.Windows.Forms.Timer mazoTimer;
    }
}