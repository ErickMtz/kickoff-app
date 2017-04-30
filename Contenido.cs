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
using Java.Lang;
using App1;
using AndroidPager;

namespace App1
{
    class Contenido : BaseAdapter
    {
        private Activity actividad;
        private List<Noticias> lista;
        public Contenido(Activity ac, List<Noticias> l)
        {
            this.actividad = ac;
            this.lista = l;
        }
        public override int Count
        {
            get
            {
                return lista.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return lista[position].Id_lista;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? actividad.LayoutInflater.Inflate(Resource.Layout.Control, parent, false);
            TextView Titulo;
            TextView autor;
            TextView descripcion;
            TextView fecha;
            Titulo = view.FindViewById<TextView>(Resource.Id.Titulo);
            autor = view.FindViewById<TextView>(Resource.Id.Autor);
            descripcion = view.FindViewById<TextView>(Resource.Id.Descripcion);
            fecha = view.FindViewById<TextView>(Resource.Id.Fecha);
            Titulo.Text =lista[position].Titulo;
            autor.Text = lista[position].Autor;
            descripcion.Text = lista[position].Decripcion;
            fecha.Text = lista[position].Fecha;
            return view;

        }
    }
}