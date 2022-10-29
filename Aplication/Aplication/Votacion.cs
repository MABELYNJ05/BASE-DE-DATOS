using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplication
{
    public partial class Votacion : Form
    {
        public Votacion()
        {
            InitializeComponent();
        }

        private void buttonRegresar_Click(object sender, EventArgs e)
        {
            Form formIngreso = new Ingreso();
            formIngreso.Show();
            this.Hide();
        }

        private void buttonMapa_Click(object sender, EventArgs e)
        {

        }
    }
}
