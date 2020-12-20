using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Realms;
using Android.Views;
using Android.Content;
using System.Linq;
using System;

namespace groupF
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]

    public class MainActivity : Activity
    {

        EditText email;

        EditText password;

        Button loginBtn;

        Button registerBtn;
        
        Realm realmDB;
        //CONNECT WITH DATABASE
        string emailValue;

        string passwordValue;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            realmDB = Realm.GetInstance();


            email = FindViewById<EditText>(Resource.Id.emailID);

            password = FindViewById<EditText>(Resource.Id.passwordID);

            loginBtn = FindViewById<Button>(Resource.Id.button1);
            loginBtn.Click += loginBtn_Click;

            registerBtn = FindViewById<Button>(Resource.Id.registerBtnID);
            registerBtn.Click += RegisterBtn_Click;

        }

        private void loginBtn_Click(object sender, System.EventArgs e)
        {
            emailValue = email.Text;
            passwordValue = password.Text;

            if (emailValue.Trim() == "" )
            {
                Toast.MakeText(this, "PLEASE ENTER YOU EMAIL ",
                    ToastLength.Long).Show();
            }

            else if (passwordValue.Trim() == "")
            {
                Toast.MakeText(this, "PLEASE ENTER YOUR PASSWORD",
                    ToastLength.Long).Show();
            }

            else
            {
                var userinfodbObj = realmDB.All<UserInfoDB>().Where(d => d.email == emailValue.ToLower() && d.password == passwordValue);
                var count = userinfodbObj.Count();

                if (count > 0)
                {
                    Intent newFrameScreen = new Intent(this, typeof(Frame));

                    newFrameScreen.PutExtra("email", emailValue);

                    StartActivity(newFrameScreen);
                }

                else
                {
                    Toast.MakeText(this, "NO USER WITH THIS EMAIL",
                        ToastLength.Long).Show();
                }
            }
        }  

          private void RegisterBtn_Click (object sender, System.EventArgs e)
            {
                Intent newRegisterScreen = new Intent(this,
                    typeof(Register));
                StartActivity(newRegisterScreen);
            }

        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}