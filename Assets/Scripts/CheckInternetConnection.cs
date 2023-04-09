using UnityEngine;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class CheckInternetConnection : MonoBehaviour
{
    WebView webView;
    void Start()
    {        
        webView = gameObject.GetComponent<WebView>();
        CheckForInternetConnection();
    }

    void CheckForInternetConnection()
    {
        // Check internet connection
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            // If no internet connection, show native dialog message with "Refresh" button
#if UNITY_ANDROID
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject alertBuilder = new AndroidJavaObject("android.app.AlertDialog$Builder", currentActivity);
            alertBuilder.Call<AndroidJavaObject>("setTitle", "Error");
            alertBuilder.Call<AndroidJavaObject>("setMessage", "No internet connection!");
            //alertBuilder.Call<AndroidJavaObject>("setPositiveButton", "OK", null);
            alertBuilder.Call<AndroidJavaObject>("setNegativeButton", "Refresh", new AlertDialogClickListener(this));
            alertBuilder.Call<AndroidJavaObject>("create").Call("show");
#endif
        }
        else
        {   // If internet ok, show url
            webView.ShowUrlFullScreen();
        }
    }

    class AlertDialogClickListener : AndroidJavaProxy
    {
        private CheckInternetConnection caller;

        public AlertDialogClickListener(CheckInternetConnection caller) : base("android.content.DialogInterface$OnClickListener")
        {
            this.caller = caller;
        }

        public void onClick(AndroidJavaObject dialog, int which)
        {
            // When "Refresh" button is clicked, check for internet connection again            
            caller.CheckForInternetConnection();

        }
    }
}
