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
using Android.Support.V4.App;
using Android.Support.V4.View;
using App1;

namespace AndroidPager
{
    [Activity(Label = "Kickoff Soccer")]
    public class Home : FragmentActivity
    {
        int count = 1;
        ListView lista;
        Button btn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            

            var pager = FindViewById<ViewPager>(Resource.Id.pager);
            var adaptor = new GenericFragmentPagerAdaptor(SupportFragmentManager);
            adaptor.AddFragmentView((i, v, b) => {
                var view = i.Inflate(Resource.Layout.Noticias, v, false);

                
                
                


                return view;
            });

            adaptor.AddFragmentView((i, v, b) => {
                var view = i.Inflate(Resource.Layout.Perfil, v, false);

                var txtUserID = FindViewById<TextView>(Resource.Id.txtNombre);
               
               var User = Intent.GetStringExtra("IdUser");


                



                return view;
            });
            adaptor.AddFragmentView((i, v, b) => {

                
                var view = i.Inflate(Resource.Layout.tab, v, false);

                return view;
            });

            pager.Adapter = adaptor;
            pager.SetOnPageChangeListener(new ViewPageListenerForActionBar(ActionBar));

            ActionBar.AddTab(pager.GetViewPageTab(ActionBar, "Noticias"));
            ActionBar.AddTab(pager.GetViewPageTab(ActionBar, "Perfil"));
            ActionBar.AddTab(pager.GetViewPageTab(ActionBar, "Ranking"));


        }
    }
}