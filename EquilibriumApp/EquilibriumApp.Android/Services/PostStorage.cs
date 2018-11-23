//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using EquilibriumApp.Services.Common;
//using Xamarin.Forms;
//using Firebase.Storage;
//using Android.Support.V7.App;
//using Java.Lang;
//using Android.Gms.Tasks;

//[assembly: Dependency(typeof(EquilibriumApp.Droid.Services.PostStorage))]
//namespace EquilibriumApp.Droid.Services
//{
//    public class PostStorage : AppCompatActivity, IPostStorage,  IOnProgressListener, IOnSuccessListener, IOnFailureListener
//    {
//        ProgressDialog progressDialog;

//        public string SendImage(string path)
//        {
//            FirebaseStorage storage = FirebaseStorage.Instance;
//            StorageReference storageRef = storage.GetReferenceFromUrl("gs://roda-da-vida-app.appspot.com");

//            if(string.IsNullOrEmpty(path))
//                return null;

//            string nameImage = null;
//            try
//            {
//                progressDialog = new ProgressDialog(this);
//                nameImage = Guid.NewGuid().ToString();

//                var images = storageRef.Child("images/recommendedactivities/" + nameImage);

//                images.PutFile(Android.Net.Uri.Parse(path)).AddOnProgressListener(this).AddOnSuccessListener(this).AddOnFailureListener(this);
//            }
//            catch(System.Exception Ex)
//            {
//                System.Diagnostics.Debug.WriteLine(Ex.ToString());
//                App.Current.MainPage.DisplayAlert("Erro", Ex.ToString(),"Ok");
//            }
//            return nameImage;
//        }

//        public void OnProgress(Java.Lang.Object snapshot)
//        {
//            var taskSnapShot = (UploadTask.TaskSnapshot)snapshot;
//            double progress = (100.0 * taskSnapShot.BytesTransferred / taskSnapShot.TotalByteCount);
//            progressDialog.SetMessage("Uploaded " + (int)progress + " %");
//        }
//        public void OnSuccess(Java.Lang.Object result)
//        {
//            progressDialog.Dismiss();
//            Toast.MakeText(this, "Uploaded Successful", ToastLength.Short).Show();
//        }
//        public void OnFailure(Java.Lang.Exception e)
//        {
//            progressDialog.Dismiss();
//            Toast.MakeText(this, "" + e.Message, ToastLength.Short).Show();
//        }
//    }
//}