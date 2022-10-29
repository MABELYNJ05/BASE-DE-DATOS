using Aplication.Conexion;
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
    public partial class DatosPersonales : Form
    {

        ClsConexion cn = new ClsConexion();

        public DatosPersonales()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form atras = new Ingreso();
            atras.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Empadronar formEmpadronar = new Empadronar();
            formEmpadronar.textBoxDPI.Text = this.textBoxDPI2.Text;
            formEmpadronar.textBoxDPI.Enabled = false;
            formEmpadronar.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           Votacion formVotacion = new Votacion();
           
            String sqll = "SELECT " +
                "D.departamentoNombre, " +
                "M.municipioNombre, " +
                "CONCAT('# ',ME.MesaNumero) AS mesa, " +
                "CONCAT('# ',L.LibroNumero) AS libro, " +
                "CONCAT('# ',H.HojaNumero) AS hoja, " +
                "CONCAT('# ',LI.LineaNumero) AS linea " +
            "FROM " +
                "[ProyectoFinal].[Tb_Personas] AS P " +
                "INNER JOIN  [ProyectoFinal].[Tb_Empadronamiento] AS E " +
                    "ON E.EmpadronamientoPersona = P.PersonaCodigo  " +
                "INNER JOIN [ProyectoFinal].[Tb_EmisionVoto] AS V " +
                    "ON V.EmisionVotoEmpadronamiento = E.EmpadronamientoCodigo " +
                "INNER JOIN [ProyectoFinal].[Tb_Departamentos] AS D " +
                    "ON D.departamentoCodigo = E.EmpadronamientoDepartamento  " +
                "INNER JOIN [ProyectoFinal].[Tb_Municipios] AS M " +
                    "ON M.municipioCodigo = E.EmpadronamientoMunicipio " +
                "INNER JOIN [ProyectoFinal].[Tb_Mesas] AS ME " +
                    "ON ME.MesaCodigo = V.EmisionVotoMesa " +
                "INNER JOIN [ProyectoFinal].[Tb_Libros] AS L " +
                    "ON L.LibroCodigo = V.EmisionVotoLibro " +
                "INNER JOIN [ProyectoFinal].[Tb_Hojas] AS H " +
                    "ON H.HojaCodigo = V.EmisionVotoHoja " +
                "INNER JOIN [ProyectoFinal].[Tb_Lineas] AS LI  " +
                    "ON LI.LineaCodigo = V.EmisionVotoLinea " +
            "WHERE P.PersonaDPI = '"+textBoxDPI2.Text+"'";
            DataTable dt = new DataTable();
            dt = cn.PersonaObtener(sqll);

            formVotacion.textBoxDepartamento.Text = dt.Rows[0]["departamentoNombre"].ToString();
            formVotacion.textBoxMunicipio.Text = dt.Rows[0]["municipioNombre"].ToString();
            formVotacion.textBoxMesa.Text = dt.Rows[0]["mesa"].ToString();
            formVotacion.textBoxLibro.Text = dt.Rows[0]["libro"].ToString();
            formVotacion.textBoxHoja.Text = dt.Rows[0]["hoja"].ToString();
            formVotacion.textBoxLinea.Text = dt.Rows[0]["linea"].ToString();

            formVotacion.textBoxDepartamento.Enabled = false;
            formVotacion.textBoxMunicipio.Enabled = false;
            formVotacion.textBoxMesa.Enabled = false;
            formVotacion.textBoxLibro.Enabled = false;
            formVotacion.textBoxHoja.Enabled = false;
            formVotacion.textBoxLinea.Enabled = false;          

            formVotacion.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
