using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Clases
{
    public sealed class MySqlBd
    {
        #region Atributos;
        private string _server = "sql8.freesqldatabase.com";
        private string _user = "sql8171912";
        private string _password = "PnH5LYCjBx";
        private string _bd = "sql8171912";
        private object _Conexion = null;
        private string _cadenaCnn = "";
        private string _msj;
        #endregion
        #region Propiedades
        private string CadenaCnn
        {

            get
            {
                if (_cadenaCnn == "")
                    GenerarCadena();
                return _cadenaCnn;
            }
        }
        public string Server
        {
            get
            {
                return _server;
            }

            set
            {
                _server = value;
            }
        }

        public string User
        {
            get
            {
                return _user;
            }

            set
            {
                _user = value;
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

        public object Conexion
        {
            get
            {
                if (_Conexion == null)
                    AbreConexion();
                else
                   if (((MySqlConnection)_Conexion).State
                   != ConnectionState.Open)
                    AbreConexion();
                return _Conexion;
            }
        }

        public string Msj
        {
            get
            {
                return _msj;
            }

            set
            {
                _msj = value;
            }
        }

      
        #endregion

        #region Constructores
        public MySqlBd() { }
        public MySqlBd(string cadena)
        {
            this._cadenaCnn = cadena;
        }
        #endregion
        #region Metodos
        public void GenerarCadena()
        {
            if (_server != "" && _user != "" && _password != "" && _bd != "")
            {
                _cadenaCnn = String.Format("server={0};user id={1};password={2};persistsecurityinfo=True;database={3}", _server, _user, _password, _bd);

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
                _Conexion = new object();
               MySqlConnection conexionBd = new MySqlConnection();
                conexionBd.ConnectionString = CadenaCnn;
                conexionBd.Open();
                valido = true;
                _msj = "Conexion abierta";
                _Conexion = conexionBd;
            }
            catch (Exception varEx)
            {
                _msj = varEx.Message;
                valido = false;
                _Conexion = null;
            }
            return valido;
        }
        public bool CierraConexion()
        {
            bool valido = false;
            try
            {
                if (_Conexion != null)
                {
                    MySqlConnection ConexionBd = (MySqlConnection)_Conexion;
                    if (ConexionBd.State == ConnectionState.Open)
                        ConexionBd.Close();
                    else
                        throw new Exception("Conexio no esta abierta");
                    _Conexion = ConexionBd;
                    valido = true;
                }
                else
                {
                    throw new Exception("Conexion no existe");
                }
            }
            catch (Exception varEx)
            {
                _msj = varEx.Message;
                valido = false;

            }
            return valido;
        }
        private void AsignaParametros(ref MySqlCommand cmd,MySqlParameter[] parametros)
        {
            foreach (MySqlParameter item in parametros)
            {
                cmd.Parameters.Add(item);
            }
        }
        private void AsignarValores(ref MySqlCommand cmd,object[] valores )
        {
            int i = 0;
            foreach (object item in valores)
            {
                cmd.Parameters[i].Value = item;
                i++;
            }
        }
        #endregion
        #region Acceso a datos
        public object EjecutaAdaptador(object[] parametros, object[] valores, string setencia, object tipoEjecucion, string nombreTabla)
        {
            DataTable dttDatos = new DataTable();
            try
            {
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = (MySqlConnection)Conexion;
                comando.CommandType = (CommandType)tipoEjecucion;
                comando.CommandText = setencia;
                AsignaParametros(ref comando, (MySqlParameter[])parametros);
                AsignarValores(ref comando, valores);
                MySqlDataAdapter daDatos = new MySqlDataAdapter(comando);
                daDatos.Fill(dttDatos);
                dttDatos.TableName = nombreTabla;
                CierraConexion();
            }
            catch (Exception varEx)
            {
                _msj = varEx.Message;
                dttDatos = null;
            }
            return dttDatos;
        }
        public int EjecutaComando(object[] parametros, object[] valores, string sentencia, object tipoEjecucion)
        {
            int n = -1;
            MySqlCommand comando = ((MySqlConnection)Conexion).CreateCommand();
            try
            {
                comando.Connection = (MySqlConnection)Conexion;
                comando.CommandType = (CommandType)tipoEjecucion;
                comando.CommandText = sentencia;
                AsignaParametros(ref comando, (MySqlParameter[])parametros);
                AsignarValores(ref comando, valores);
                n = comando.ExecuteNonQuery();
                CierraConexion();
            }
            catch (Exception varEx)
            {

                _msj = varEx.Message;
                n = -1;
            }
            return n;
        }
        public int EjecutaComandoTrans(List<object> paramtros, List<object> valores, List<string> sentencia, object tipoEjecucion)
        {
            int n = -1;
            MySqlCommand comando = ((MySqlConnection)Conexion).CreateCommand();
            MySqlTransaction objTransaccion = ((MySqlConnection)Conexion).BeginTransaction();
            try
            {
                comando.Connection = (MySqlConnection)Conexion;
                comando.Transaction = objTransaccion;
                comando.CommandType = (CommandType)tipoEjecucion;
                int i = 0;
                foreach (string varSentencia in sentencia)
                {
                    comando.Parameters.Clear();
                    comando.CommandText = varSentencia;
                    AsignaParametros(ref comando, (MySqlParameter[])paramtros[i]);
                    AsignarValores(ref comando,valores.ToArray());
                    n = comando.ExecuteNonQuery();
                    i++;
                }
                objTransaccion.Commit();
                CierraConexion();
            }
            catch (Exception varEx)
            {
                _msj = varEx.Message;
                n = -1;
                objTransaccion.Rollback();
            }
            return n;
        }
        public object EjecutaLector(object[] parametros, object[] valores, string sentencia, object tipoEjecucion)
        {
            MySqlCommand comando = ((MySqlConnection)Conexion).CreateCommand();
            MySqlDataReader drDatos = null;
            try
            {
                comando.Connection = (MySqlConnection)Conexion;
                comando.CommandType = (CommandType)tipoEjecucion;
                comando.CommandText = sentencia;
                AsignaParametros(ref comando, (MySqlParameter[])parametros);
                AsignarValores(ref comando, valores);
                drDatos = comando.ExecuteReader();
            }
            catch (Exception varEx)
            {
                _msj = varEx.Message;


            }
            return drDatos;
        }
        #endregion
    }
}
