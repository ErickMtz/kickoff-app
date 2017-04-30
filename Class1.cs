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
    class Holder:Java.Lang.Object
    {
        TextView Titulo;
        TextView autor;
        TextView descripcion;
        TextView fecha;

        public TextView Titulo1
        {
            get
            {
                return Titulo;
            }

            set
            {
                Titulo = value;
            }
        }

        public TextView Autor
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

        public TextView Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public TextView Fecha
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
    }
   
}