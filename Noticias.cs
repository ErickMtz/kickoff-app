using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace App1
{
    class Noticias
    {
        private string id_noticias;
        private string titulo;
        private string decripcion;
        private string fecha;
        private string autor;
        private int id_lista;
        private Clases.MySqlBd bd;

        public string Id_noticias
        {
            get
            {
                return id_noticias;
            }

            set
            {
                id_noticias = value;
            }
        }

        public string Titulo
        {
            get
            {
                return titulo;
            }

            set
            {
                titulo = value;
            }
        }

        public string Decripcion
        {
            get
            {
                return Decripcion1;
            }

            set
            {
                Decripcion1 = value;
            }
        }

        public string Fecha
        {
            get
            {
                return fecha;
            }

            set
            {
                fecha = value;
            }
        }

        public string Autor
        {
            get
            {
                return autor;
            }

            set
            {
                autor = value;
            }
        }

        public string Decripcion1
        {
            get
            {
                return decripcion;
            }

            set
            {
                decripcion = value;
            }
        }

        public int Id_lista
        {
            get
            {
                return id_lista;
            }

            set
            {
                id_lista = value;
            }
        }

        private static Noticias AsignarDatos(MySql.Data.MySqlClient.MySqlDataReader lector)
        {
            Noticias noti = new Noticias();
            noti.Id_noticias = lector.GetString(0);
            noti.Titulo = lector.GetString(1);
            noti.Autor = lector.GetString(2);
            noti.Decripcion = lector.GetString(3);
            noti.fecha = lector.GetString(4);
            return noti;

        }
        public static List<Noticias> RegresarTodas()
        {
            MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection();
            con.ConnectionString = @"Password=PnH5LYCjBx;Persist Security Info=True;User ID=sql8171912;Initial Catalog=sql8171912;Data Source=sql8.freesqldatabase.com";
            con.Open();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from noticias";
            List<Noticias> MisNoticias = new List<Noticias>();
            
                MySql.Data.MySqlClient.MySqlDataReader lector = cmd.ExecuteReader();

            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    MisNoticias.Add(AsignarDatos(lector));
                }
                return MisNoticias;
            }
            else
            {
                return null;
            }
        }
        public Noticias()
        {
            bd = new Clases.MySqlBd();
            bd.GenerarCadena();
        }

    }
}