using Aplication.Conexion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Aplication
{
    public partial class Registrarse : Form
    {
        ClsConexion cn = new ClsConexion();
        String valueDepartamento;

        public Registrarse()
        {
            InitializeComponent();
            comboBoxDepartamentos.SelectedIndexChanged -= comboBoxDepartamentos_SelectedIndexChanged;
            cargaComboBoxDepar();
            comboBoxDepartamentos.SelectedIndexChanged += comboBoxDepartamentos_SelectedIndexChanged;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form atras = new Inicio();
            atras.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                String nombre1 = textBoxNombre1.Text;
                String nombre2 = textBoxNombre2.Text;
                String nombre3 = textBoxNombre3.Text;
                String apellido1 = textBoxApellido1.Text;
                String apellido2 = textBoxApellido2.Text;
                String apellidoCasado = textBoxApellidoCasado.Text;
                String dpi = textBoxDPI.Text;
                String departamento = comboBoxDepartamentos.SelectedValue.ToString();
                String municipio = comboBoxMunicipios.SelectedValue.ToString();
                String direccion = textBoxDireccion.Text;
                DateTime fecha = monthCalendar.SelectionStart;


                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                pictureBoxFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] foto = ms.GetBuffer();

                System.IO.MemoryStream mss = new System.IO.MemoryStream();
                pictureBoxFirma.Image.Save(mss, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] firma = mss.GetBuffer();

                String sqll = "INSERT INTO ProyectoFinal.Tb_Personas " +
                        "(PersonaNombre1, " +
                        "PersonaNombre2, " +
                        "PersonaNombre3, " +
                        "PersonaApellido1, " +
                        "PersonaApellido2, " +
                        "PersonaApellidoCasado, " +
                        "PersonaNacimiento, " +
                        "PersonaDireccion, " +
                        "PersonaDPI, " +
                        "PersonaFoto, " +
                        "PersonaFirma, " +
                        "PersonaDepartamento, " +
                        "PersonaMunicipio" +
                        ") VALUES (" +
                            "'" + nombre1 + "', " +
                            "'" + nombre2 + "', " +
                            "'" + nombre3 + "', " +
                            "'" + apellido1 + "', " +
                            "'" + apellido2 + "', " +
                            "'" + apellidoCasado + "', " +
                            "'" + fecha + "', " +
                            "'" + direccion + "', " +
                            "'" + dpi + "', " +
                            "'" + foto + "', " +
                            "'" + firma + "', " +
                            "" + departamento + ", " +
                            "" + municipio + "); ";

                cn.insertarPersona(sqll);

                MessageBox.Show("¡Operación Exitosa!");
                Form formIngreso = new Ingreso();
                formIngreso.Show();
                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show("¡Operación fallida!");
                Form formInicio = new Inicio();
                formInicio.Show();
                this.Hide();
            }                       

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxDPI_KeyPress(object sender, KeyPressEventArgs e)
        {
    
            if ((e.KeyChar >= 32 && e.KeyChar <= 46) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo Números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            Form atras = new Empadronar();
            atras.Show();
            this.Hide();
        }
                       

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96))
            {
                MessageBox.Show("Solo Letras", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        

        private void comboBoxDepartamentos_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        public void cargaComboBoxDepar()
        {
            var dt = cn.cargarComboDepartamentos();
            DataRow dr = dt.NewRow();
            dr["departamentoNombre"] = "Seleccione un Departamento";
            dt.Rows.InsertAt(dr, 0);

            comboBoxDepartamentos.DisplayMember = "departamentoNombre";
            comboBoxDepartamentos.ValueMember = "departamentoCodigo";
            comboBoxDepartamentos.DataSource = dt;

        }

        public void cargaComboBoxMunicipio(String dep)
        {
            var dt = cn.cargarComboMunicipios(dep);
            DataRow dr = dt.NewRow();
            dr["municipioNombre"] = "Seleccione un Municipio";
            dt.Rows.InsertAt(dr, 0);
            
            comboBoxMunicipios.DisplayMember = "municipioNombre";
            comboBoxMunicipios.ValueMember = "municipioCodigo";
            comboBoxMunicipios.DataSource = dt;
        }

        private void comboBoxDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxDepartamentos.SelectedValue.ToString() != null)
            {
                
                String dep = comboBoxDepartamentos.SelectedValue.ToString();
                cargaComboBoxMunicipio(dep);
                
                
            }
        }

        private void textBoxNombre1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96))
            {
                MessageBox.Show("Solo letras", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void textBoxNombre2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96))
            {
                MessageBox.Show("Solo letras", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textBoxNombre3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96))
            {
                MessageBox.Show("Solo letras", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textBoxApellido1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96))
            {
                MessageBox.Show("Solo letras", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textBoxApellido2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96))
            {
                MessageBox.Show("Solo letras", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textBoxDPI_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo Números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }        

        private void Registrarse_Load(object sender, EventArgs e)
        {

        }

        private void buttonFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                pictureBoxFoto.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void buttonFirma_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                pictureBoxFirma.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void textBoxNombre3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
