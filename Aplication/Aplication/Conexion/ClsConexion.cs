using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Intrinsics.Arm;

namespace Aplication.Conexion
{
    internal class ClsConexion
    {
        public SqlConnection conexion;
        private String _conexion { get; }

        public ClsConexion()
        {

            _conexion = "Data Source=DESKTOP-LTAB7H5\\MCRUZ;Initial Catalog=Db_Votaciones;Integrated Security=True";

        }



        /// <summary>
        /// Cierra la conexion.
        /// </summary>
        public void cerrarConexionBD()
        {
            conexion.Close();
        }



        /// <summary>
        /// abre la conexion
        /// </summary>
        public void abrirConexion()
        {
            conexion = new SqlConnection(_conexion);
            conexion.Open();
        }
    

        public DataTable cargarComboDepartamentos()
        {
            abrirConexion();
            String sqll = "SELECT departamentoCodigo,departamentoNombre FROM ProyectoFinal.Tb_Departamentos";
            SqlDataAdapter da = new SqlDataAdapter(sqll, conexion);
            da.SelectCommand.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            da.Fill(dt);
            cerrarConexionBD();
            return dt;            
        }
      
        public DataTable cargarComboMunicipios(String dep)
        {
            abrirConexion();
            String sqll = "SELECT municipioCodigo,municipioNombre FROM ProyectoFinal.Tb_Municipios WHERE municipioDepartamento = " + dep + ";";
            SqlDataAdapter da = new SqlDataAdapter(sqll, conexion);
            da.SelectCommand.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            da.Fill(dt);
            cerrarConexionBD();
            return dt;
        }        

        public DataTable cargarComboMesas(String municipio)
        {
            abrirConexion();
            String sqll = "SELECT MesaCodigo, MesaNumero FROM ProyectoFinal.Tb_Mesas WHERE MesaMunicipio = "+municipio+" AND MesaEstado = 1;";
            SqlDataAdapter da = new SqlDataAdapter(sqll, conexion);
            da.SelectCommand.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            da.Fill(dt);
            cerrarConexionBD();
            return dt;
        }

        public DataTable cargarComboLibros(String mesa)
        {
            abrirConexion();
            String sqll = "SELECT LibroCodigo, LibroNumero FROM ProyectoFinal.Tb_Libros WHERE LibroMesa = "+mesa+" AND LibroEstado = 1;";
            SqlDataAdapter da = new SqlDataAdapter(sqll, conexion);
            da.SelectCommand.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            da.Fill(dt);
            cerrarConexionBD();
            return dt;
        }

        public DataTable cargarComboHojas(String libro)
        {
            abrirConexion();
            String sqll = "SELECT HojaCodigo, HojaNumero FROM ProyectoFinal.Tb_Hojas WHERE HojaLibro = "+libro+" AND HojaEstado = 1;";
            SqlDataAdapter da = new SqlDataAdapter(sqll, conexion);
            da.SelectCommand.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            da.Fill(dt);
            cerrarConexionBD();
            return dt;
        }

        public DataTable cargarComboLineas(String hoja)
        {
            abrirConexion();
            String sqll = "SELECT LineaCodigo, LineaNumero FROM ProyectoFinal.Tb_Lineas WHERE LineaHoja = "+hoja+" AND LineaEstado = 1;";
            SqlDataAdapter da = new SqlDataAdapter(sqll, conexion);
            da.SelectCommand.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            da.Fill(dt);
            cerrarConexionBD();
            return dt;
        }



        /// <summary>
        /// metodo login de usuarios
        /// </summary>
        /// <param name="sqll"></param>
        /// <returns></returns>
        public Boolean login(String sqll)
        {

            abrirConexion();
            Boolean value = false;
            SqlDataReader dr;
            SqlCommand comm = new SqlCommand(sqll, conexion);
            dr = comm.ExecuteReader();
           
            if (dr.Read())
            {
                value = true;
            }
            
            cerrarConexionBD();
            return value;
        }

        /// <summary>
        /// metodo que ejecuta una consulta, esta clase maneja la apertura y clausura a la base de datos
        /// </summary>
        /// <param name="sqll"></param>
        /// <returns></returns>
        public DataTable consultaTablaDirecta(String sqll)
        {
            abrirConexion();
            SqlDataReader dr;
            SqlCommand comm = new SqlCommand(sqll, conexion);
            dr = comm.ExecuteReader();

            var dataTable = new DataTable();
            dataTable.Load(dr);
            cerrarConexionBD();
            return dataTable;
        }

        public DataTable PersonaObtener(String sqll)
        {

            abrirConexion();
            SqlDataReader dr;
            SqlCommand comm = new SqlCommand(sqll, conexion);
            dr = comm.ExecuteReader();

            var dataTable = new DataTable();
            dataTable.Load(dr);
            cerrarConexionBD();
            return dataTable;
        }



        /// <summary>
        /// ejecuta una instrucción de insersion, eliminación y actualización,
        /// esta clase se encarga de manejar las aperturas y clausuras de la conexion.
        /// </summary>
        /// <param name="sqll"></param>
        public void insertarPersona(String sqll)
        {
            abrirConexion();
            try
            {

                SqlCommand comm = new SqlCommand(sqll, conexion);
                comm.ExecuteReader();
               

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                cerrarConexionBD();
            }



        }

        public void EjecutaSQLDirecto(String sqll)
        {
            abrirConexion();
            try
            {

                SqlCommand comm = new SqlCommand(sqll, conexion);
                comm.ExecuteReader();                

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                cerrarConexionBD();
            }



        }

        public String EjecutaSQLDirectoInsert(String sqll)
        {
            string codigo = "";
            abrirConexion();           
            SqlCommand comm = new SqlCommand(sqll, conexion);                
            SqlDataReader dr = comm.ExecuteReader();

            dr.Read();
            codigo = dr[0].ToString();
            cerrarConexionBD();
            return codigo;
        }

        /// <summary>
        /// ejecuta instrucciones SQL, pero el progromador debe manejar la apertura y clausura
        /// de las conexiones.
        /// </summary>
        /// <param name="sqll"></param>
        public void EjecutaSQLManual(String sqll)
        {
            // se abre y cierra la conexino manualmente
            SqlCommand comm = new SqlCommand(sqll, conexion);
            comm.ExecuteReader();
        }
    }
}
