using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Poke_tank
{
    public partial class Mapa : Form
    {
        public Mapa()
        {
            InitializeComponent();
        }

        private void botonSorpresaFlavio_Click(object sender, EventArgs e)
        {
            Flaviosorpresa from1 = new Flaviosorpresa();
            from1.ShowDialog();
        }

        //cambia el fondo del formulario de la batalla según el nivel seleccionado
        private void CambiarFondo(FormBatalla formulario, System.Drawing.Image imagen)
        {
            if (formulario == null || imagen == null) return;
            formulario.BackgroundImage = imagen;
            formulario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }

        //especificamos las imágenes y las aplicamos al form de batalla
        private void botonNivel1_Click(object sender, EventArgs e)
        {
            FormBatalla from2 = new FormBatalla();
            CambiarFondo(from2, Properties.Resources.fondoNivel1);
            from2.ShowDialog();
        }

        private void botonNivel2_Click(object sender, EventArgs e)
        {
            FormBatalla from3 = new FormBatalla();
            CambiarFondo(from3, Properties.Resources.fondoNivel2);
            from3.ShowDialog();
        }
        private void botonNivel3_Click(object sender, EventArgs e)
        {
            FormBatalla from4 = new FormBatalla();
            CambiarFondo(from4, Properties.Resources.fondoNivel3);
            from4.ShowDialog();
        }
    }
}
