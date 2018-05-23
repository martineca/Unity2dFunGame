using ExaGames.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generate : MonoBehaviour {
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;
    public GameObject navPanel;
    public Camera mainCamera;
    public LivesManager LivesManager;

    public int score = 0;
    public float CreateWallsSpeed = 1.2f;
    public float CameraSpeed = 0.02f;
    public bool IsEnabled = true;
    public float firstWallPosition = 1.4f;
    public int dead = 0;
    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    // Use this for initialization
    public GameObject[] AllWalls;
    void Start()
	{
        navPanel.SetActive(false);
        InvokeRepeating("CreateObstacle",0.1f, 0.1f);
        Input.multiTouchEnabled = false;
    }
	
	// Update is called once per frame

    void Update()
    {
     
        if (!IsEnabled)
        { 
            CancelInvoke();   
        }
   
    }


	void OnGUI () 
	{     
        guiStyle.fontSize = (int)(70.0f * (float)(Screen.width) / 1920.0f);
        GUI.color = Color.black;
	    GUI.Label(new Rect(0,Screen.height - 50,100,50)," Score: " + score.ToString(), guiStyle);
    }


    public void CreateObstacle()
    {
            int rnd = Random.Range(1, 4);
            float afterFirst = 2.8f;
            Vector3 screenPosition = Camera.main.transform.position;
            
            if (rnd == 1)
            {
             var newWall = Instantiate(wall1, new Vector3(-1.31f, firstWallPosition + afterFirst, 0), Quaternion.identity);
             var newWall2 = Instantiate(wall2, new Vector3(1.41f, firstWallPosition + afterFirst, 0), Quaternion.identity);
             newWall.tag = "NewWall";
             newWall2.tag = "NewWall";
            }
            else if (rnd == 2)
            {
               var newWall = Instantiate(wall3, new Vector3(-0.59f, firstWallPosition + afterFirst, 0), Quaternion.identity);
                 newWall.tag = "NewWall";
            }
            else if (rnd == 3)
            {
             var newWall =   Instantiate(wall4, new Vector3(0.48f, firstWallPosition + afterFirst, 0), Quaternion.identity);
                 newWall.tag = "NewWall";
            }
            firstWallPosition += 1.4f;

    }

}
