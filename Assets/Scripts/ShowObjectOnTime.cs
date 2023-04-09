using System;
using UnityEngine;

public class ShowObjectOnTime : MonoBehaviour
{
    [SerializeField] GameObject webViewScreen;
    [SerializeField] GameObject helloScreen;


    private void Start()
    {

        // If the screen has already been shown, then we show it and exit the method
        if (PlayerPrefs.HasKey("SelectedScreen"))
        {
            if (PlayerPrefs.GetString("SelectedScreen") == "webViewScreen")
            {
                webViewScreen.SetActive(true);
            }
            else
            {
                helloScreen.SetActive(true);
            }
            
            return;
        }

        // Get the current time
        DateTime now = DateTime.Now;

        // If the minutes are even, then show helloScreen, otherwise - webViewScreen
        if (now.Minute % 2 == 0)
        {
            webViewScreen.SetActive(false);
            helloScreen.SetActive(true);

            PlayerPrefs.SetString("SelectedScreen", "helloScreen");
        }
        else
        {
            webViewScreen.SetActive(true);
            helloScreen.SetActive(false);

            PlayerPrefs.SetString("SelectedScreen", "webViewScreen");
        }

       
    }
}
