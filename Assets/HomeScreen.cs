using ExaGames.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HomeScreen : MonoBehaviour {
    public Button PlayBtn;
    public GameObject HomeScreenPanel;
    public Text highScoreText;
    public Button jumpRight;
    public Button jumpCenter;
    public Button jumpLeft;
    public Button musicBtn;
    public Button hardModeBtn;
    public Button ToggleButtons;
    public Text hardModeBtnText;
    public Player player;
    private GooglePlayGamesScript play;
    public GameObject clickHereAnimation;
    public static int HomeScreenClosed = 0;
    void Start () {
        HomeScreenPanel.SetActive(false);
        if(HomeScreenClosed == 0)
        {
            HomeScreenPanel.SetActive(true);
            PlayBtn.onClick.AddListener(PlayGame);
            musicBtn.onClick.AddListener(AudioSettings);
            hardModeBtn.onClick.AddListener(ActivateHardMode);
            int highscore = PlayerPrefs.GetInt("highscore", 0);      
            clickHereAnimation.SetActive(false);
            jumpRight.interactable = false;
            jumpLeft.interactable = false;
            jumpCenter.interactable = false;

            if (PlayerPrefs.GetInt("audiosettings", 0) == 0)
            {
                musicBtn.image.overrideSprite = Resources.Load<Sprite>("Sprites/musicBtn");
                AudioListener.pause = false;
            }
            else
            {
                musicBtn.image.overrideSprite = Resources.Load<Sprite>("Sprites/musicBtnDisabled");
                AudioListener.pause = true;
            }

             if (PlayerPrefs.GetInt("hardModeTrump", 0) == 0)
              {
                  //Deactivate
                   highScoreText.text = highscore.ToString();
                   hardModeBtnText.color = Color.black;
              }
              else
              {
                  //Activate
                    highScoreText.text = PlayerPrefs.GetInt("hardModeHighScore", 0).ToString();
                    hardModeBtnText.color = Color.red;
              }
        }
    }
	
	// Update is called once per frame
	void Update () {

	}



    public void setHomeScreenActive()
    {
            HomeScreenPanel.SetActive(true);
            PlayBtn.onClick.AddListener(PlayGame);
            int highscore = PlayerPrefs.GetInt("highscore", 0);
            highScoreText.text = highscore.ToString();
            clickHereAnimation.SetActive(false);
            jumpRight.interactable = false;
            jumpLeft.interactable = false;
            jumpCenter.interactable = false;
    }

    void PlayGame()
    {
        HomeScreenClosed = 1;
        HomeScreenPanel.SetActive(false);
        clickHereAnimation.SetActive(true);
        jumpRight.interactable = true;
        jumpLeft.interactable = true;
        jumpCenter.interactable = true;
    }
  
    void AudioSettings()
    {
     
        int audioToggle = PlayerPrefs.GetInt("audiosettings", 0);
        if (audioToggle == 0)
        {
            AudioListener.pause = true;
            PlayerPrefs.SetInt("audiosettings", 1);
            //   SpriteRenderer musicBtn = GetComponent<SpriteRenderer>();
            musicBtn.image.overrideSprite = Resources.Load<Sprite>("Sprites/musicBtnDisabled");
        } else
        {
            AudioListener.pause = false;
            PlayerPrefs.SetInt("audiosettings", 0);
            musicBtn.image.overrideSprite = Resources.Load<Sprite>("Sprites/musicBtn");
        }
        PlayerPrefs.Save();
    }

    void ActivateHardMode()
    {
        if (PlayerPrefs.GetInt("hardModeTrump", 0) == 1)
        {
            //Deactivate
             PlayerPrefs.SetInt("hardModeTrump", 0);
             hardModeBtnText.color = Color.black;
        }
        else
        {
            //Activate
              PlayerPrefs.SetInt("hardModeTrump", 1);
              hardModeBtnText.color = Color.red;
        }
    }
}
