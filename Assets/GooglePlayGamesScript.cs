using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
public class GooglePlayGamesScript : MonoBehaviour
{
#region PUBLIC_VAR
    public string leaderboard;
    public Button gameCenter;

#endregion
    #region DEFAULT_UNITY_CALLBACKS
    void Start ()
    {
        // recommended for debugging:
        gameCenter.onClick.AddListener(OnShowLeaderBoard);
        if (Time.time < 2)
        {
            PlayerPrefs.GetInt("loggerdIn", 0);
            PlayerPrefs.SetInt("loggerdIn", 0);
            PlayerPrefs.Save();
            PlayGamesPlatform.DebugLogEnabled = true;

            // Activate the Google Play Games platform
            PlayGamesPlatform.Activate();
            LogIn();
        }
    }
    #endregion
#region BUTTON_CALLBACKS
    /// <summary>
    /// Login In Into Your Google+ Account
    /// </summary>
    public void LogIn ()
    {

        Social.localUser.Authenticate ((bool success) =>
        {
            if (success) {
                Debug.Log ("Login Sucess");
                PlayerPrefs.GetInt("loggerdIn", 0);
                PlayerPrefs.SetInt("loggerdIn",1);
                PlayerPrefs.Save();
            } 
            else 
            {
                PlayerPrefs.GetInt("loggerdIn", 0);
                PlayerPrefs.SetInt("loggerdIn",0);
                 PlayerPrefs.Save();
                Debug.Log ("Login failed");
            }
        });
    }
    /// <summary>
    /// Shows All Available Leaderborad
    /// </summary>
    public void OnShowLeaderBoard()
    {
        
        // new = CgkInoic2v4REAIQBw  old = CgkInoic2v4REAIQAQ
        if (PlayerPrefs.GetInt("loggerdIn", 0) == 1)
        {
               Social.ShowLeaderboardUI (); // Show all leaderboard
        } else
        {
            LogIn();
        }
       
        
     
    }
    /// <summary>
    /// Adds Score To leader board
    /// </summary>
    public void OnAddScoreToLeaderBorad (int score)
    {
        if (Social.localUser.authenticated) {

            if (PlayerPrefs.GetInt("hardModeTrump", 0) == 1)
            {
                 Social.ReportScore (score, "CgkInoic2v4REAIQCQ", (bool success) =>
            {
                if (success) {
                    Debug.Log ("Update Score Success"); 
                } else {
                    Debug.Log ("Update Score Fail");
                }
            });

            }
            else
            {
                 Social.ReportScore (score, "CgkInoic2v4REAIQBw", (bool success) =>
            {
                if (success) {
                    Debug.Log ("Update Score Success");
                 //    OnShowLeaderBoard();                 
                } else {
                    Debug.Log ("Update Score Fail");
                }
            });
            }          
        }
    }

    /// <summary>
    /// On Logout of your Google+ Account
    /// </summary>
    public void OnLogOut ()
    {
        ((PlayGamesPlatform)Social.Active).SignOut ();
    }
#endregion
}
