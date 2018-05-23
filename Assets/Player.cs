using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExaGames.Common;

public class Player : MonoBehaviour {

    public Button jumpLeftBtn;
    public Button jumpRightBtn;
    public Button jumpCenterBtn;
    public Button restartBtn;
    public Button returnHomeBtnn;
    public Button exitGameBtn;
    public Generate generate;
    public cameraMove CameraMove;
    public HomeScreen HomeScreen;
    public GooglePlayGamesScript GooglePlayGamesScript;
    public Text scoreText;
    public Text highScoreText;
    public AudioSource jumpSound;
    public AudioSource deadSound;
    public bool finishTutorial = false;
    private int tutorialCenter = 0;
    private int tutorialLeft = 0;
    private int tutorialRight = 0;
    private int highscore = 0;
    private int hardModeHighScore = 0;
    public int dead = 0;
    public Text TrumpQuote;

    public float jumpForce = 1.4f;
    public float y = -2.2f;
    public float x = 0;

    private int walk = 0;

    // Admob ads
   
    public int fourMinutesInGame = 0;

    

    // Use this for initialization
    void Start () {
        Button btnLeft = jumpLeftBtn.GetComponent<Button>();
        Button btnCenter = jumpCenterBtn.GetComponent<Button>();
        Button btnRight = jumpRightBtn.GetComponent<Button>();
        Button returnHomeBtn = returnHomeBtnn.GetComponent<Button>();
        restartBtn.onClick.AddListener(restartGame);
        btnLeft.onClick.AddListener(jumpLeft);
        btnCenter.onClick.AddListener(jumpCenter);
        btnRight.onClick.AddListener(jumpRight);
        exitGameBtn.onClick.AddListener(quitApp);
        returnHomeBtn.onClick.AddListener(returnHome);
        highscore = PlayerPrefs.GetInt("highscore",0);
        hardModeHighScore = PlayerPrefs.GetInt("hardModeHighScore",0);
 
    }
    


// Update is called once per frame
     void Update ()
	{
    // banners disabled for now
         if (Time.time % 60 > 0 && Time.time % 60 < 1  && Time.time > 10)
      {
    
          ads._instance.timeToDestory = 1;
      }
       


       Rigidbody2D myRigidbody = GetComponent<Rigidbody2D>();
       Vector3 test = myRigidbody.transform.position;
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        myRigidbody.MovePosition( new Vector3(x, y, 0));

        //tutorial bit
        if (tutorialRight == 1)
        {
            GameObject.Find("clickHereAnimation").GetComponent<SpriteRenderer>().transform.position = new Vector3(1.7f, generate.mainCamera.transform.position.y -3.3f, 0); ;
        }
        if(tutorialLeft == 1)
        {
            GameObject.Find("clickHereAnimation").GetComponent<SpriteRenderer>().transform.position = new Vector3(-1.5f, generate.mainCamera.transform.position.y -3.3f, 0); ;
        }

        if (finishTutorial)
        {
             GameObject.Find("clickHereAnimation").GetComponent<SpriteRenderer>().enabled = false;
        }
       

        if (screenPosition.y > Screen.height || screenPosition.y < 0)
         {
           if (dead == 0)
            {
                Die();
                dead = 1;
            }           
        }

    }

   

    void jumpRight()
    {
        Rigidbody2D myRigidbody = GetComponent<Rigidbody2D>();
        Vector3 newPosition = myRigidbody.transform.position;
        SpriteRenderer currentPlayerSprite = GetComponent<SpriteRenderer>();
        jumpSound.Play();
        if (newPosition.x == 1.5f)
        {
            y += jumpForce;
            generate.score++;
            if (!finishTutorial)
            {
                tutorialRight = 0;
                tutorialLeft = 1;
         
                increaseCameraSpeed();
            } else
            {
                increaseCameraSpeed();
            }

            if (walk == 0)
                {
                      currentPlayerSprite.sprite = Resources.Load<Sprite>("Sprites/walk");
                      walk++;
                }
                else if (walk == 1)
                {
                     currentPlayerSprite.sprite = Resources.Load<Sprite>("Sprites/walk1");
                     walk = 0;
                }
        }
        else
        {
            x = 1.5f;
        }
          transform.position = newPosition;
       
    }

    void jumpCenter()
    {
        Rigidbody2D myRigidbody = GetComponent<Rigidbody2D>();
        Vector3 newPosition = myRigidbody.transform.position;
        SpriteRenderer currentPlayerSprite = GetComponent<SpriteRenderer>();
        if (newPosition.x == 0)
        {
            y += jumpForce;
            generate.score++;
            if (!finishTutorial)
            {

                tutorialRight = 1;
             
                increaseCameraSpeed();
            }
            else
            {
                increaseCameraSpeed();
            }

            if (walk == 0)
                {
                      currentPlayerSprite.sprite = Resources.Load<Sprite>("Sprites/walk");
                      walk++;
                }
                else if (walk == 1)
                {
                     currentPlayerSprite.sprite = Resources.Load<Sprite>("Sprites/walk1");
                     walk = 0;
                }
        } else
        {
            x= 0;
            transform.position = newPosition;

        }
        jumpSound.Play();
    }

    void jumpLeft()
    {
        Rigidbody2D myRigidbody = GetComponent<Rigidbody2D>();
        Vector3 newPosition = myRigidbody.transform.position;
        SpriteRenderer currentPlayerSprite = GetComponent<SpriteRenderer>();
      

        jumpSound.Play();
        if (newPosition.x == -1.7f)
        {
           y += jumpForce;
            generate.score++;
            tutorialLeft += 1;
            if (!finishTutorial)
            {
                if (tutorialLeft == 2)
                {
                     finishTutorial = true;
                   
                }

              
                increaseCameraSpeed();
            }
            else
            {
                increaseCameraSpeed();
            }

             if (walk == 0)
                {
                      currentPlayerSprite.sprite = Resources.Load<Sprite>("Sprites/walk");
                      walk++;
                }
                else if (walk == 1)
                {
                     currentPlayerSprite.sprite = Resources.Load<Sprite>("Sprites/walk1");
                     walk = 0;
                }
        }
        else
        {
            x = -1.7f;
            
        }
        transform.position = newPosition;

    }

    void  increaseCameraSpeed(){
        if (PlayerPrefs.GetInt("hardModeTrump", 0) == 1)
        {
            if (CameraMove.cameraSpeed < 3.4f)
                {
                CameraMove.cameraSpeed += 0.65f;
                }

           if (generate.score == 50)
              {
                  CameraMove.cameraSpeed += 0.15f;
              }
        }
        else
        {

             if (CameraMove.cameraSpeed < 2.8f)
                {
                CameraMove.cameraSpeed += 0.25f;
                }

             if (generate.score == 100)
                {
                    CameraMove.cameraSpeed += 0.25f;
                }
             if (generate.score == 200)
                {
                    CameraMove.cameraSpeed += 0.25f;
                }
      }
    }


    public void returnHome()
    {
        HomeScreen.HomeScreenClosed = 0;
        Application.LoadLevel(Application.loadedLevel);
    }
    
    	// Die by collision
    	void OnCollisionEnter2D(Collision2D other)
    	{
           if (dead == 0)
           {
               generate.score -= 1;
               Die();
               dead = 1;
           }
    	}
    	
    	void Die()
    	{
       
        if (dead == 0) {
          
            dead = 1;
            deadSound.Play();
            if ( ads._instance.fourMinutesInGame == 1)
            {
                  ads._instance.ShowAd();
                  ads._instance.RequestInterstitial();
            }
           
            generate.IsEnabled = false;
            CameraMove.IsEnabled = false;
            scoreText.text = generate.score.ToString();
           
            jumpCenterBtn.interactable = false;
            jumpLeftBtn.interactable = false;
            jumpRightBtn.interactable = false;
            int rnd = UnityEngine.Random.Range(1, 3);
              switch (rnd)
            {
                case 1:
                    TrumpQuote.text = "America is great again!";
                    break;
                case 2:
                    TrumpQuote.text = "All abroad!";
                    break;
                case 3:
                    TrumpQuote.text = "The wall is doing the job!";
                    break;
            }
           if( ads._instance.timeToDestory == 1)
               {
                ads._instance.ReloadBanner();
                ads._instance.timeToDestory = 0;
              }
                
            GameObject.Find("clickHereAnimation").GetComponent<SpriteRenderer>().enabled = false;
            if (PlayerPrefs.GetInt("loggerdIn", 0) == 1)
            {
             GooglePlayGamesScript.OnAddScoreToLeaderBorad(generate.score);
            }

            // create the player animation and show restart panel 
            StartCoroutine(showRestartPanel());
            if (PlayerPrefs.GetInt("hardModeTrump", 0) == 1)
            {
               highScoreText.text = hardModeHighScore.ToString();
               if ( generate.score > PlayerPrefs.GetInt("hardModeHighScore", 0))
                 {
                     TrumpQuote.text = "I will sign an order for that new score!";
                     PlayerPrefs.SetInt("hardModeHighScore", generate.score);
                     PlayerPrefs.Save();
                     
                 }
            }
            else
            {
                 highScoreText.text = highscore.ToString();
               if ( generate.score > PlayerPrefs.GetInt("highscore", 0))
                 {
                     TrumpQuote.text = "I will sign an order for that new score!";
                     PlayerPrefs.SetInt("highscore", generate.score);
                     PlayerPrefs.Save();
                     
                 }
            }

        }
    }


        IEnumerator showRestartPanel() 
        {  
         SpriteRenderer renderer = GetComponent<SpriteRenderer>();
         renderer.color = new Color(renderer.color.r, renderer.color.g,renderer.color.b, 0.5f);
         yield return new WaitForSeconds(0.4f);
           renderer.color = new Color(renderer.color.r, renderer.color.g,renderer.color.b,1f);
         yield return new WaitForSeconds(0.4f);
          renderer.color = new Color(renderer.color.r, renderer.color.g,renderer.color.b, 0.5f);
         yield return new WaitForSeconds(0.4f);
          renderer.color = new Color(renderer.color.r, renderer.color.g,renderer.color.b,1f);
         yield return new WaitForSeconds(0.4f);
          generate.navPanel.SetActive(true);
        }



    void restartGame()
        {
           generate.IsEnabled = true;
           generate.firstWallPosition = 1.4f;
           generate.score = 0;
           Application.LoadLevel(Application.loadedLevel);
       }


    public void quitApp()
    {
        Application.Quit();
    }    
}

