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
    public partial class Empadronar : Form
    {

        ClsConexion cn = new ClsConexion();

        public Empadronar()
        {
            InitializeComponent();
            comboBoxDepartamentos.SelectedIndexChanged -= comboBoxDepartamentos_SelectedIndexChanged;
            cargaComboBoxDepar();
            comboBoxDepartamentos.SelectedIndexChanged += comboBoxDepartamentos_SelectedIndexChanged;
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form formIngreso = new Ingreso();
            formIngreso.Show();
            this.Hide();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                String departamento = comboBoxDepartamentos.SelectedValue.ToString();
                String municipio = comboBoxMunicipios.SelectedValue.ToString();
                String mesa = comboBoxMesas.SelectedValue.ToString();
                String libro = comboBoxLibros.SelectedValue.ToString();
                String hoja = comboBoxHoja.SelectedValue.ToString();
                String linea = comboBoxLínea.SelectedValue.ToString();
                String dpi = textBoxDPI.Text;


                DataTable dt = new DataTable();
                dt = cn.consultaTablaDirecta("SELECT * FROM ProyectoFinal.Tb_Personas WHERE PersonaDPI = '" + dpi + "';");
                int persona = int.Parse(dt.Rows[0]["PersonaCodigo"].ToString());

                String empadronamientoCodigo = cn.EjecutaSQLDirectoInsert("INSERT INTO [ProyectoFinal].[Tb_Empadronamiento] ([EmpadronamientoPersona], [EmpadronamientoDepartamento], [EmpadronamientoMunicipio], [EmpadronamientoFecha]) VALUES (" + persona + ", " + departamento + ", " + municipio + ", GETDATE()) SELECT SCOPE_IDENTITY();");
                cn.EjecutaSQLDirecto("INSERT INTO [ProyectoFinal].[Tb_EmisionVoto] ([EmisionVotoPersona], [EmisionVotoEmpadronamiento], [EmisionVotoLinea], [EmisionVotoHoja], [EmisionVotoLibro], [EmisionVotoMesa]) VALUES (" + persona + ", " + empadronamientoCodigo + ", " + linea + ", " + hoja + ", " + libro + ", " + mesa + ");");

                cn.EjecutaSQLDirecto("UPDATE ProyectoFinal.Tb_Lineas SET LineaEstado = 0 WHERE LineaCodigo = " + linea + ";");

                dt = cn.consultaTablaDirecta("SELECT MIN(L.LineaCodigo) AS LineaCodigo FROM ProyectoFinal.Tb_Lineas AS L WHERE L.LineaHoja = " + hoja + " AND L.LineaEstado = 1;");
                linea = dt.Rows[0]["LineaCodigo"].ToString();
                if (linea == null)
                {
                    cn.EjecutaSQLDirecto("UPDATE ProyectoFinal.Tb_Hojas SET HojaEstado = 0 WHERE HojaCodigo = " + hoja + ";");

                    dt = new DataTable();
                    dt = cn.consultaTablaDirecta("SELECT MIN(H.HojaCodigo) AS HojaCodigo FROM ProyectoFinal.Tb_Hojas AS H WHERE H.HojaLibro = " + libro + " AND H.HojaEstado = 1;");
                    hoja = dt.Rows[0]["HojaCodigo"].ToString();
                    if (hoja == null)
                    {
                        cn.EjecutaSQLDirecto("UPDATE ProyectoFinal.Tb_Libros SET LibroEstado = 0 WHERE LibroCodigo = " + libro + ";");

                        dt = new DataTable();
                        dt = cn.consultaTablaDirecta("SELECT MIN(L.LibroCodigo) AS LibroCodigo FROM ProyectoFinal.Tb_Libros AS L WHERE L.LibroMesa = " + mesa + " AND L.LibroEstado = 1;");
                        libro = dt.Rows[0]["LibroCodigo"].ToString();

                        if (libro == null)
                        {
                            cn.EjecutaSQLDirecto("UPDATE ProyectoFinal.Tb_Mesas SET MesaEstado = 0 WHERE MesaCodigo = " + mesa + ";");

                        }

                    }

                }

                MessageBox.Show("El registro fue exitoso");
                Form formIngreso = new Ingreso();
                formIngreso.Show();
                this.Hide();
            }
            catch (Exception x)
            {

                MessageBox.Show(x.ToString());
            }           

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

        public void cargaComboBoxMesas(String municipio)
        {           
            var dt = cn.cargarComboMesas(municipio);
            DataRow dr = dt.NewRow();
            dr["MesaNumero"] = "Seleccione una Mesa";
            dt.Rows.InsertAt(dr, 0);

            comboBoxMesas.DisplayMember = "MesaNumero";
            comboBoxMesas.ValueMember = "MesaCodigo";
            comboBoxMesas.DataSource = dt;

        }

        public void cargaComboBoxLibros(String mesa)
        {
            var dt = cn.cargarComboLibros(mesa);
            DataRow dr = dt.NewRow();
            dr["LibroNumero"] = "Seleccione un Libro";
            dt.Rows.InsertAt(dr, 0);

            comboBoxLibros.DataSource = dt;
            comboBoxLibros.DisplayMember = "LibroNumero";
            comboBoxLibros.ValueMember = "LibroCodigo";


        }

        public void cargaComboBoxHojas(String libro)
        {
            var dt = cn.cargarComboHojas(libro);
            DataRow dr = dt.NewRow();
            dr["HojaNumero"] = "Seleccione una Hoja";
            dt.Rows.InsertAt(dr, 0);

            comboBoxHoja.DisplayMember = "HojaNumero";
            comboBoxHoja.ValueMember = "HojaCodigo";
            comboBoxHoja.DataSource = dt;

        }

        public void cargaComboBoxLineas(String hoja)
        {
            var dt = cn.cargarComboLineas(hoja);
            DataRow dr = dt.NewRow();
            dr["LineaNumero"] = "Seleccione una Linea";
            dt.Rows.InsertAt(dr, 0);

            comboBoxLínea.DisplayMember = "LineaNumero";
            comboBoxLínea.ValueMember = "LineaCodigo";
            comboBoxLínea.DataSource = dt;
        }

        private void comboBoxMunicipios_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxMesas.SelectedIndexChanged -= comboBoxMesas_SelectedIndexChanged;

            if (comboBoxMunicipios.SelectedValue.ToString() != null)
            {
                String municipio = comboBoxMunicipios.SelectedValue.ToString();
                cargaComboBoxMesas(municipio);
                comboBoxMesas.SelectedIndexChanged += comboBoxMesas_SelectedIndexChanged;



            }

        }

        private void comboBoxDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxMunicipios.SelectedIndexChanged -= comboBoxMunicipios_SelectedIndexChanged;


            if (comboBoxDepartamentos.SelectedValue.ToString() != null)
            {

                String dep = comboBoxDepartamentos.SelectedValue.ToString();
                cargaComboBoxMunicipio(dep);
                comboBoxMunicipios.SelectedIndexChanged += comboBoxMunicipios_SelectedIndexChanged;



            }
        }

        private void comboBoxMesas_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(comboBoxMesas.SelectedValue.ToString());
            comboBoxLibros.SelectedIndexChanged -= comboBoxLibros_SelectedIndexChanged;

            if (comboBoxMesas.SelectedValue.ToString() != null)
            {

                String mesa = comboBoxMesas.SelectedValue.ToString();
                cargaComboBoxLibros(mesa);
                comboBoxLibros.SelectedIndexChanged += comboBoxLibros_SelectedIndexChanged;



            }

        }

        private void comboBoxLibros_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxHoja.SelectedIndexChanged -= comboBoxHoja_SelectedIndexChanged;


            if (comboBoxLibros.SelectedValue.ToString() != null)
            {

                String libro = comboBoxLibros.SelectedValue.ToString();
                cargaComboBoxHojas(libro);
                comboBoxHoja.SelectedIndexChanged += comboBoxHoja_SelectedIndexChanged;




            }

        }

        private void comboBoxHoja_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxHoja.SelectedValue.ToString() != null)
            {

                String hoja = comboBoxHoja.SelectedValue.ToString();
                cargaComboBoxLineas(hoja);


            }
        }
    }
}
