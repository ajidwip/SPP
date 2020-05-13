using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Vision;
using Android.Gms.Vision.Barcodes;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using static Android.Gms.Vision.Detector;
namespace BarcodeTeknikMobile
{
   
    public class scanact : Android.App.Fragment,ISurfaceHolderCallback, IProcessor
    {
        SurfaceView surfaceView;
        TextView txtResult;
        BarcodeDetector barcodeDetector;
        CameraSource cameraSource;
        const int RequestCameraPermisionID = 1001;
        Android.Hardware.Camera _camera;
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestCameraPermisionID:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            if (ActivityCompat.CheckSelfPermission(Activity, Manifest.Permission.Camera) != Android.Content.PM.Permission.Granted)
                            {
                                //Request Permision  
                                ActivityCompat.RequestPermissions(Activity, new string[]
                                {
                    Manifest.Permission.Camera
                                }, RequestCameraPermisionID);
                                return;
                            }
                            try
                            {
                                cameraSource.Start(surfaceView.Holder);
                            }
                            catch (InvalidOperationException)
                            {
                            }
                        }
                    }
                    break;
            }
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //_camera = Android.Hardware.Camera.Open();
            //Android.Hardware.Camera.Parameters paramsq = _camera.GetParameters();
            //paramsq.SceneMode = Android.Hardware.Camera.Parameters.SceneModeBarcode;
            //paramsq.FocusMode = Android.Hardware.Camera.Parameters.FocusModeAuto;

           
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View itemView = LayoutInflater.From(container.Context).
                 Inflate(Resource.Layout.scanlayout, container, false);
            surfaceView = itemView.FindViewById<SurfaceView>(Resource.Id.cameraView);
            txtResult = itemView.FindViewById<TextView>(Resource.Id.txtResult);

            barcodeDetector = new BarcodeDetector.Builder(Activity)
               .SetBarcodeFormats(BarcodeFormat.Code128)
               .Build();

            cameraSource = new CameraSource
                .Builder(Activity, barcodeDetector)
                .SetAutoFocusEnabled(true)
                .SetRequestedPreviewSize(640, 480)
                .Build();

            surfaceView.Holder.AddCallback(this);
            barcodeDetector.SetProcessor(this);

            return itemView;
        }
            public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
        }
        public void SurfaceCreated(ISurfaceHolder holder)
        {
            if (ActivityCompat.CheckSelfPermission(Activity, Manifest.Permission.Camera) != Android.Content.PM.Permission.Granted)
            {
                //Request Permision  
                ActivityCompat.RequestPermissions(Activity, new string[]
                {
                    Manifest.Permission.Camera
                }, RequestCameraPermisionID);
                return;
            }
            try
            {
                cameraSource.Start(surfaceView.Holder);
            }
            catch (InvalidOperationException)
            {
            }
        }
        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            cameraSource.Stop();
        }
        public void ReceiveDetections(Detections detections)
        {
            SparseArray qrcodes = detections.DetectedItems;
            if (qrcodes.Size() != 0)
            {
                txtResult.Post(() => {
                    //Vibrator vibrator = (Vibrator)Activity.GetSystemService(Context.VibratorService);
                    //vibrator.Vibrate(1000);
                    txtResult.Text = ((Barcode)qrcodes.ValueAt(0)).RawValue;
                    if(txtResult.Text.ToString()!="")
                    {
                        barcodeDetector.Release();
                        Intent i = new Intent(Activity, typeof(MainActivity));
                        i.PutExtra("result", txtResult.Text.ToString());
                        StartActivity(i);
                        Activity.Finish();
                    }
                    
                });
            }
        }
        public void Release()
        {

        }
    }
}