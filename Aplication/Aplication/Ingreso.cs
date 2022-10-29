using NPOI.SS.Formula.Functions;
using Rhino.Mocks.Constraints;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aplication.Conexion;

namespace Aplication
{
    public partial class Ingreso : Form
    {
        ClsConexion cn = new ClsConexion();

        public Ingreso()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonIngresar.Enabled = false;
        }

        private void numdpi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar >= 32 && e.KeyChar <= 47 ) || (e.KeyChar >= 58 && e.KeyChar <= 255 ))
            {
                MessageBox.Show("Solo Números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void nacimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 46) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo Números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }        

        private void nacimiento_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fecha = monthCalendar.SelectionStart;
                String dpi = numdpi.Text;
                Boolean value = cn.login("SELECT * FROM ProyectoFinal.Tb_Personas WHERE PersonaNacimiento = '" + fecha + "' AND PersonaDPI = '" + dpi + "';");
                if (value)
                {
                    DataTable dt = new DataTable();
                    String sqll = "SELECT " +
		                            "p.*, " +
                                    "CONCAT( " +
                                        "ISNULL(p.PersonaNombre1, ''), ' ', " +
                                        "ISNULL(p.PersonaNombre2, ''), ' '," +
                                        "ISNULL(p.PersonaNombre3, ''), ' '," +
                                        "ISNULL(p.PersonaApellido1, ''), ' '," +
                                        "ISNULL(p.PersonaApellido2, ''), ' '," +
                                        "ISNULL(p.PersonaApellidoCasado, '')" +
                                    ") AS PersonaNombreCompleto," +
                                    "d.departamentoNombre AS PersonaDepartamentoShow," +
                                    "m.municipioNombre AS PersonaMunicipioShow, " +
                                    "CASE "+
                                        "WHEN e.EmpadronamientoPersona IS NULL THEN 'NO ESTA EMPADRONADO'  " +
                                        "WHEN e.EmpadronamientoPersona IS NOT NULL THEN 'YA ESTA EMPADRONADO' " +
                                    "END AS EmpadronamientoPersonaShow, " +
                                    "CASE  " +
                                        "WHEN e.EmpadronamientoPersona IS NULL THEN 0  " +
                                        "WHEN e.EmpadronamientoPersona IS NOT NULL THEN 1 " +
                                    "END AS EmpadronamientoPersonaValue " +
                                "FROM " +
                                    "ProyectoFinal.Tb_Personas AS p " +
                                    "INNER JOIN ProyectoFinal.Tb_Departamentos AS d " +
                                        "ON d.departamentoCodigo = p.PersonaDepartamento " +
                                    "INNER JOIN ProyectoFinal.Tb_Municipios AS m " +
                                        "ON m.municipioCodigo = p.PersonaMunicipio " + 
                                    "LEFT JOIN ProyectoFinal.Tb_Empadronamiento AS e "+
                                        "ON e.EmpadronamientoPersona = p.PersonaCodigo "+
                                "WHERE " +
                                    "p.PersonaDPI = '"+dpi+"' " +
                                    "AND p.PersonaNacimiento = '"+fecha+"'";
                    dt = cn.PersonaObtener(sqll);
                    DatosPersonales formDatosPersonales = new DatosPersonales();
                    formDatosPersonales.textBoxNombreCompleto.Text = dt.Rows[0]["PersonaNombreCompleto"].ToString();
                    formDatosPersonales.textBoxDPI2.Text = dt.Rows[0]["PersonaDPI"].ToString();
                    formDatosPersonales.textBoxNacimiento.Text = dt.Rows[0]["PersonaNacimiento"].ToString();
                    formDatosPersonales.textBoxDireccion2.Text = dt.Rows[0]["PersonaDireccion"].ToString();
                    formDatosPersonales.textBoxDepartamento.Text = dt.Rows[0]["PersonaDepartamentoShow"].ToString();
                    formDatosPersonales.textBoxMunicipio.Text = dt.Rows[0]["PersonaMunicipioShow"].ToString();
                    formDatosPersonales.textBoxEmpadronado.Text = dt.Rows[0]["EmpadronamientoPersonaShow"].ToString();

                    formDatosPersonales.textBoxNombreCompleto.Enabled = false;
                    formDatosPersonales.textBoxDPI2.Enabled = false;
                    formDatosPersonales.textBoxNacimiento.Enabled = false;
                    formDatosPersonales.textBoxDireccion2.Enabled = false;
                    formDatosPersonales.textBoxDepartamento.Enabled = false;
                    formDatosPersonales.textBoxMunicipio.Enabled = false;
                    formDatosPersonales.textBoxEmpadronado.Enabled = false;

                    int validarButtonEmpadronamiento = int.Parse(dt.Rows[0]["EmpadronamientoPersonaValue"].ToString());

                    if (validarButtonEmpadronamiento == 1)
                    {
                        formDatosPersonales.buttonEmpadronar.Enabled =  false;
                        formDatosPersonales.buttonLugarVotacion.Enabled = true;                        

                    } else
                    {
                        formDatosPersonales.buttonEmpadronar.Enabled = true;
                        formDatosPersonales.buttonLugarVotacion.Enabled = false;
                    }
                    


                    /*byte[] imgFoto = (byte[])dt.Rows[0]["PersonaFoto"];
                    byte[] imgFirma = (byte[])dt.Rows[0]["PersonaFirma"];


                    MemoryStream ms = new MemoryStream(imgFoto);
                    formDatosPersonales.pictureBoxFoto2.Image = Image.FromStream(ms);*/

                    /*System.IO.MemoryStream sm = new System.IO.MemoryStream(imgFirma);
                    formDatosPersonales.pictureBoxFirma2.Image = Image.FromStream(sm);*/

                    formDatosPersonales.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Datos incorrectos");
                }

            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
            
        }

        private void buttonRegresar_Click(object sender, EventArgs e)
        {

            Form formInicio = new Inicio();
            formInicio.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
