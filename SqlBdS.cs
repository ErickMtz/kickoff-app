using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace SqlBdMy
{
    class ClsConexion
    {
        #region atributos
        private string _servidor = "HGM";
        private string _bd = "biblioteca_script";
        private string _usuario = "sa";
        private string _password = "abasolo";
        private string _cadenaCnn = "";
        private object _conexion = null;
        private string _mensaje = "";
        #endregion

        #region propiedades
        public string Servidor
        {
            get
            {
                return _servidor;
            }

        }

        public string Bd
        {
            get
            {
                return _bd;
            }

            set
            {
                _bd = value;
            }
        }

        public string Usuario
        {
            get
            {
                return _usuario;
            }

            set
            {
                _usuario = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
            }
        }

        public string CadenaCnn
        {
            get
            {
                if (_cadenaCnn == "")
                    GeneraCadena();
                return _cadenaCnn;
            }
        }

        public object Conexion
        {
            get
            {
                if (_conexion == null)
                    AbreConexion();
                else
                   if (((SqlConnection)_conexion).State
                   != ConnectionState.Open)
                    AbreConexion();
                return _conexion;
            }
        }

        public string Mensaje
        {
            get
            {
                return _mensaje;
            }

            set
            {
                _mensaje = value;
            }
        }

        #endregion
        #region constructores
        public ClsConexion() { }
        public ClsConexion(string cadena)
        {
            this._cadenaCnn = cadena;
        }
        #endregion
        #region Acceso a datos}
        public void GeneraCadena()
        {
            if (_servidor != "" && _password != "" && _usuario != "" && _bd != "")
            {
                _cadenaCnn = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", _servidor, _bd, _usuario, _password);
            }
            else
            {
                _cadenaCnn = "";
            }
        }
        public bool AbreConexion()
        {
            bool valido = false;
            try
            {
                _conexion = new object();
                SqlConnection conexionBd = new SqlConnection();
                conexionBd.ConnectionString = CadenaCnn;
                conexionBd.Open();
                valido = true;
                _mensaje = "Conexion abierta";
                _conexion = conexionBd;
            }
            catch (Exception varEx)
            {
                _mensaje = varEx.Message;
                valido = false;
                _conexion = null;
            }
            return valido;
        }
        public bool CierraConexion()
        {
            bool valido = false;
            try
            {
                if (_conexion != null)
                {
                    SqlConnection ConexionBd = (SqlConnection)_conexion;
                    if (ConexionBd.State == ConnectionState.Open)
                        ConexionBd.Close();
                    else
                        throw new Exception("Conexio no esta abierta");
                    _conexion = ConexionBd;
                    valido = true;
                }
                else
                {
                    throw new Exception("Conexion no existe");
                }
            }
            catch (Exception varEx)
            {
                _mensaje = varEx.Message;
                valido = false;

            }
            return valido;
        }
        #endregion
        #region PARAMETRO Y VALORES
        void AsignarParametros(ref SqlCommand cmdSql, params SqlParameter[] parametros)
        {
            if (parametros != null)
                foreach (SqlParameter par in parametros)
                {
                    cmdSql.Parameters.Add(par);
                }

        }
        void AsignaValores(ref SqlCommand cmdSql, params object[] valores)
        {
            int i = 0;
            if (valores != null)
                foreach (object val in valores)
                    cmdSql.Parameters[i++].Value = val;

        }
        #endregion
        #region Metodos
        public object EjecutaAdaptador(object[] parametros, object[] valores, string setencia, object tipoEjecucion, string nombreTabla)
        {
            DataTable dttDatos = new DataTable();
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = (SqlConnection)Conexion;
                comando.CommandType = (CommandType)tipoEjecucion;
                comando.CommandText = setencia;
                AsignarParametros(ref comando, (SqlParameter[])parametros);
                AsignaValores(ref comando, valores);
                SqlDataAdapter daDatos = new SqlDataAdapter(comando);
                daDatos.Fill(dttDatos);
                dttDatos.TableName = nombreTabla;
                CierraConexion();
            }
            catch (Exception varEx)
            {
                _mensaje = varEx.Message;
                dttDatos = null;
            }
            return dttDatos;
        }
        public int EjecutaComando(object[] parametros, object[] valores, string sentencia, object tipoEjecucion)
        {
            int n = -1;
            SqlCommand comando = ((SqlConnection)Conexion).CreateCommand();
            try
            {
                comando.Connection = (SqlConnection)Conexion;
                comando.CommandType = (CommandType)tipoEjecucion;
                comando.CommandText = sentencia;
                AsignarParametros(ref comando, (SqlParameter[])parametros);
                AsignaValores(ref comando, valores);
                n = comando.ExecuteNonQuery();
                CierraConexion();
            }
            catch (Exception varEx)
            {

                _mensaje = varEx.Message;
                n = -1;
            }
            return n;
        }
        public int EjecutaComandoTrans(List<object> paramtros, List<object> valores, List<string> sentencia, object tipoEjecucion)
        {
            int n = -1;
            SqlCommand comando = ((SqlConnection)Conexion).CreateCommand();
            SqlTransaction objTransaccion = ((SqlConnection)Conexion).BeginTransaction();
            try
            {
                comando.Connection = (SqlConnection)Conexion;
                comando.Transaction = objTransaccion;
                comando.CommandType = (CommandType)tipoEjecucion;
                int i = 0;
                foreach (string varSentencia in sentencia)
                {
                    comando.Parameters.Clear();
                    comando.CommandText = varSentencia;
                    AsignarParametros(ref comando, (SqlParameter[])paramtros[i]);
                    n = comando.ExecuteNonQuery();
                    i++;
                }
                objTransaccion.Commit();
                CierraConexion();
            }
            catch (Exception varEx)
            {
                _mensaje = varEx.Message;
                n = -1;
                objTransaccion.Rollback();
            }
            return n;
        }
        public object EjecutaLector(object[] parametros, object[] valores, string sentencia, object tipoEjecucion)
        {
            SqlCommand comando = ((SqlConnection)Conexion).CreateCommand();
            SqlDataReader drDatos = null;
            try
            {
                comando.Connection = (SqlConnection)Conexion;
                comando.CommandType = (CommandType)tipoEjecucion;
                comando.CommandText = sentencia;
                AsignarParametros(ref comando, (SqlParameter[])parametros);
                AsignaValores(ref comando, valores);
                drDatos = comando.ExecuteReader();
            }
            catch (Exception varEx)
            {
                _mensaje = varEx.Message;


            }
            return drDatos;
        }
        #endregion

    }
}
