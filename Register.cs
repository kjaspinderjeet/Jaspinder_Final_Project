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
using Realms;

namespace groupF
{
    [Activity(Label = "Register")]
    public class Register : Activity
    {

        EditText nameofuser;
        EditText email;
        EditText phonenumber;
        EditText age;
        EditText password;
        Button registerButton;
        Button returnBtn;


        Realm realmDB;
        string nameofuserValue;
        string emailValue;
    
        string passwordValue;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);

            realmDB = Realm.GetInstance();

            nameofuser = FindViewById<EditText>(Resource.Id.nameofuserID);

            email = FindViewById<EditText>(Resource.Id.emailID);

            phonenumber = FindViewById<EditText>(Resource.Id.phonenumberID);


            password = FindViewById<EditText>(Resource.Id.passwordID);



            returnBtn.Click += ReturnBtn_Click;

            registerButton = FindViewById<Button>(Resource.Id.registerBtnID);
            registerButton.Click += registerUser;
        }


        private void registerUser(object sender, EventArgs e)
        {
            emailValue = email.Text;
            passwordValue = password.Text;
            nameofuserValue = nameofuser.Text;

            if (emailValue.Trim() == "" )
            {
                Toast.MakeText(this, "Please Enter Email!",
                    ToastLength.Long).Show();
            }

            else if (nameofuserValue.Trim() == "" )
            {
                Toast.MakeText(this, " Enter User Name!",
                    ToastLength.Long).Show();
            }

            else if (passwordValue.Trim() == "" )
            {
                Toast.MakeText(this, " Enter the Password!",
                    ToastLength.Long).Show();
            }

            else
            {
                var customerData = realmDB.All<UserInfoDB>().Where(d => d.email == emailValue.ToLower());
                var customerCount = customerData.Count();

                if ( customerCount > 0)
                {
                    Toast.MakeText(this, "USER ALREADY EXIST", ToastLength.Long).Show();
                }

                else
                {
                    UserInfoDB saveUserInfo = new UserInfoDB();

                    saveUserInfo.nameofuser = nameofuserValue;

                    saveUserInfo.email = emailValue.ToLower();

                    saveUserInfo.password = passwordValue.ToLower();

                    realmDB.Write(() =>
                    {
                        realmDB.Add(saveUserInfo);
                    });
               
                    Toast.MakeText(this, "REGISTERED ", ToastLength.Short).Show();
                }
            }
        }
        private void ReturnBtn_Click(object sender, EventArgs e)

        {
            Intent newMainActivityScreen = new Intent(this, typeof(MainActivity));
            StartActivity(newMainActivityScreen);
        }

    }
}