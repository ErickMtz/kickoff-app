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
using MySql.Data.MySqlClient;

namespace AndroidPager
{
    [Activity(Label = "Registro")]
    public class Registro : Activity
    {
        Button btnRegistrar;


        EditText etNombre,edId, edFecha, edPeso, edEstatura, edPosicion, Descripcion, perfil, password, email;

        protected override void OnCreate(Bundle savedInstanceState)
        {
           
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.registro);


            edId = FindViewById<EditText>(Resource.Id.edtId);
            etNombre = FindViewById<EditText>(Resource.Id.edtUser);
            edFecha = FindViewById<EditText>(Resource.Id.edtFecha_nac);
            edPeso = FindViewById<EditText>(Resource.Id.edtPeso);
            edEstatura = FindViewById<EditText>(Resource.Id.edtEstatura);
            edPosicion = FindViewById<EditText>(Resource.Id.edtPosicion);
            Descripcion = FindViewById<EditText>(Resource.Id.edtDescrip);
            perfil = FindViewById<EditText>(Resource.Id.edtPerfil);
            password = FindViewById<EditText>(Resource.Id.edtPass);
            email = FindViewById<EditText>(Resource.Id.edtEmail);

            

            btnRegistrar = FindViewById<Button>(Resource.Id.btnRegistrar);

           btnRegistrar.Click += BtnRegistrar_Click;
        }

       

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(@"Password=PnH5LYCjBx;Persist Security Info=True;User ID=sql8171912;Initial Catalog=sql8171912;Data Source=sql8.freesqldatabase.com");

                //string insertQuery = "INSERT INTO usuarios(id,nombre,fecha_nac,peso,estatura,posicion,descripcion,perfil,password,correo) VALUES('" + edId.Text + "','" + edFecha.Text + "','" + edPeso.Text + "','" + edEstatura.Text + "','" + edPosicion.Text + "','" + Descripcion.Text + "','" + perfil.Text + "','" + password.Text + "','" + email.Text + "')";
                connection.Open();
                string insertQuery = "INSERT INTO usuarios(id,nombre,fecha_nac,peso,estatura,posicion,descripcion,perfil,password,correo) VALUES(@id,@nombre,@fecha,@peso,@estatura,@posicion,@descripcion,@perfil,@password,@coreo)";
                
                MySqlCommand command = new MySqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("id",edId.Text);
                command.Parameters.AddWithValue("nombre", etNombre.Text);
                command.Parameters.AddWithValue("fecha_nac", edFecha.Text);
                command.Parameters.AddWithValue("peso", edPeso.Text);
                command.Parameters.AddWithValue("estatura", edEstatura);
                command.Parameters.AddWithValue("posicion", edPosicion.Text);
                command.Parameters.AddWithValue("descripcion", Descripcion.Text);
                command.Parameters.AddWithValue("perfil", perfil.Text);
                command.Parameters.AddWithValue("password", password.Text);
                command.Parameters.AddWithValue("correo", email.Text);

                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        Toast.MakeText(this, "Registrado con exito", ToastLength.Short).Show();
                        StartActivity(typeof(MainActivity));
                    }
                    else
                    {
                        Toast.MakeText(this, "Error de registro", ToastLength.Short).Show(); ;
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short);
                }

                connection.Close();
            }
            catch {
            }
        }
    }
}