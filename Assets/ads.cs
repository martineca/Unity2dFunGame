using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ads : MonoBehaviour {

	// Use this for initialization
    public int timeToDestory = 0;
    public int fourMinutesInGame = 0;
    public static ads _instance;
    private BannerView bannerView;
    private InterstitialAd interstitialad;

     public static ads GetInstance() { 

         return _instance; 
     
     }
    void Awake() {
        DontDestroyOnLoad(this);
          if (_instance == null) {
         _instance = this;
         } else {
             DestroyObject(gameObject);
         }
    }

    void Start () {
        RequestBanner();
        RequestInterstitial();        
	}

	// Update is called once per frame
	void Update () {
       
          if (Time.time % 120 > 0 && Time.time % 120 < 1  && Time.time > 10)
         {
            if (fourMinutesInGame == 0) {
            
            fourMinutesInGame = 1;
            }
        }
		
	}

    
        public void RequestBanner()
   {
       #if UNITY_EDITOR
           string adUnitId = "ca-app-pub-6815622516308976/7731340248";
       #elif UNITY_ANDROID
           string adUnitId = "ca-app-pub-6815622516308976/7731340248";
       #elif UNITY_IPHONE
           string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
       #else
           string adUnitId = "ca-app-pub-6815622516308976/7731340248";
       #endif
       AdSize adSize = new AdSize(260, 45);
       // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, adSize, AdPosition.Top);
       // Create an empty ad request.
       AdRequest request = new AdRequest.Builder().Build();
       // Load the banner with the request.
       bannerView.LoadAd(request);
     
   }


    public void ReloadBanner()
    {

      AdRequest request = new AdRequest.Builder().Build();
       // Load the banner with the request.
      bannerView.LoadAd(request);

    }


    public void RequestInterstitial()
    {

            #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-6815622516308976/6254607044";
            #elif UNITY_IPHONE
                                      string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
            #else
                                      string adUnitId = "ca-app-pub-6815622516308976/6254607044";
            #endif
            // Initialize an InterstitialAd.
            interstitialad = new InterstitialAd(adUnitId);
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
           // Load the interstitial with the request.
            interstitialad.LoadAd(request);
    }


    public void ShowAd()
    {
        if (interstitialad != null) {
                if (interstitialad.IsLoaded())
                {
                    interstitialad.Show();
                     ads._instance.fourMinutesInGame = 0;
                }
        }
    }
}
