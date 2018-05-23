using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour {
    public float cameraSpeed = 0;
    public bool IsEnabled = true;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
      if (IsEnabled)
      {
          transform.Translate(Vector3.up * Time.deltaTime * 1.2f *cameraSpeed);
      }

    }

    void MoveCamera()
    {
      if (IsEnabled) {
       
                  Vector3 newPosition = this.transform.position + new Vector3(0, cameraSpeed  , 0);
                  this.transform.position = newPosition;
              
      }
    }
}
