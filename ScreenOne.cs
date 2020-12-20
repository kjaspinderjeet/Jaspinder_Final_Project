using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Realms;


namespace groupF
{
    public class ScreenOne : Fragment
    {
        TextView userinfo;

        UserInfoDB userinfodbObj;

        TextView email; 

        TextView phonenumber;

        TextView name;

        TextView age;

        Activity context;

        Realm realmDB;
        // VARIABLES FOR DATABASE
        string changeName;
        string changeEmail;
        string changePhone;

        string changeAge;

        public ScreenOne( UserInfoDB userInfo)
        {
            this.userinfodbObj = userInfo;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        // DEEPANSHU CHECK THIS
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View myView = inflater.Inflate(Resource.Layout.ScreenOne, container, false);


            userinfo = myView.FindViewById<TextView>(Resource.Id.textView1);

            email = myView.FindViewById<TextView>(Resource.Id.emailID);

            phonenumber = myView.FindViewById<TextView>(Resource.Id.phonenumberID);

            name = myView.FindViewById<TextView>(Resource.Id.nameofuserID);

            age = myView.FindViewById<TextView>(Resource.Id.ageID);


            userinfo.Text = userinfodbObj.nameofuser;

            email.Text = userinfodbObj.email;

            name.Text = userinfodbObj.nameofuser;
         

            return myView;
        }
        private void changeUserInfo(object sender, EventArgs e)
        {
            changeName = name.Text;

            changePhone = phonenumber.Text;

            changeAge = age.Text;

            UserInfoDB saveUserData = new UserInfoDB();

            saveUserData.nameofuser = changeName;


            var toast = Toast.MakeText(context, "DATA SAVED", ToastLength.Short);
            toast.Show();

            realmDB.Write(() =>
            {
                realmDB.Add(saveUserData);
            });
            name.Text = changeName; //ISSUE FIXED


        }
    }
}