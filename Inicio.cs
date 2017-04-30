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
using App1;

namespace AndroidPager
{
    [Activity(Label = "Inicio")]
    public class Inicio : Activity
    {
        int count = 1;
        ListView lista;
        Android.App.ProgressDialog progress;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Noticias);

            progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("Iniciando Sesion... Por Favor espere...");
            progress.SetCancelable(false);
            progress.Show();

            RunOnUiThread(() =>
            {
                lista = FindViewById<ListView>(Resource.Id.listView1);
            Contenido con = new Contenido(this, Noticias.RegresarTodas());
            lista.Adapter = con;
                progress.Dismiss();
            });
        }
    }
}