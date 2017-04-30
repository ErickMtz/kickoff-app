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

namespace AndroidPager
{
    class Usuarios
    {
        private string nombre_usuario;
        private string nombre;
        private string fecha;
        private int  peso;
        private double estatura;
        private string posisicion;
        private string descripcion;
        private string password;
        private string correo;
        private string perfil;
        public string Nombre_usuario { get => nombre_usuario; set => nombre_usuario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public int Peso { get => peso; set => peso = value; }
        public double Estatura { get => estatura; set => estatura = value; }
        public string Posisicion { get => posisicion; set => posisicion = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Perfil { get => perfil; set => perfil = value; }
        public string Password { get => password; set => password = value; }
        public string Correo { get => correo; set => correo = value; }
    }
}