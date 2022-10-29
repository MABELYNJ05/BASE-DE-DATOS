namespace Aplication
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form formIngreso = new Ingreso();
            formIngreso.Show ();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form formRegistrarse = new Registrarse();
            formRegistrarse.Show();
            this.Hide();
        }
    }
}