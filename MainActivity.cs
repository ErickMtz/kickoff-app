using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using Android.Content;

namespace AndroidPager
{
    [Activity(Label = "Kickoff Soccer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Android.App.ProgressDialog progress;
        EditText User, Password;
        Button btnIngresar;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Sesion);        
            User = FindViewById<EditText>(Resource.Id.edtUser);
            Password = FindViewById<EditText>(Resource.Id.edtPass);
            btnIngresar= FindViewById<Button>(Resource.Id.btnIngresar);
            TextView textRegistrar = FindViewById<TextView>(Resource.Id.textView3);


            btnIngresar.Click += BtnIngresar_Click;
            textRegistrar.Click += TextRegistrar_Click;
        }

        private void TextRegistrar_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(Registro));
        }

        private void BtnIngresar_Click(object sender, System.EventArgs e)
        {
            progress = new Android.App.ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("Iniciando Sesion... Por Favor espere...");
            progress.SetCancelable(false);
            progress.Show();
            RunOnUiThread(() =>
            {
              
            try
            {
                //creando la conexion
                MySqlConnection miConecion = new MySqlConnection(@"Password=PnH5LYCjBx;Persist Security Info=True;User ID=sql8171912;Initial Catalog=sql8171912;Data Source=sql8.freesqldatabase.com");
                //abriendo conexion
                miConecion.Open();

                MySqlCommand comando = new MySqlCommand("select id, password from usuarios where id = '" + User.Text + "'And password = '" + Password.Text + "' ", miConecion);

                //ejecuta una instruccion de sql devolviendo el numero de las filas afectadas
                comando.ExecuteNonQuery();
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(comando);

                //Llenando el dataAdapter
                da.Fill(ds, "usuarios");
                //utilizado para representar una fila de la tabla que necesitas en este caso usuario
                DataRow DR;
                DR = ds.Tables["usuarios"].Rows[0];

                //evaluando que la contraseña y usuario sean correctos
                if ((User.Text == DR["id"].ToString()) || (Password.Text == DR["password"].ToString()))
                {

                    string user = User.Text;

                    //instanciando la actividad principal
                    Intent intent = new Intent(this, typeof(Inicio));
                    intent.PutExtra("IdUser", user);
                    
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "Error! Su contraseña y/o usuario son invalidos", ToastLength.Long);
                }

            }
            catch
            {
                Toast.MakeText(this, "Error! Su contraseña y/o usuario son invalidos", ToastLength.Long);
            }
            });

        }

            

       
    }
   
}


