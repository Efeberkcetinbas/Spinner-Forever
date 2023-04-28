using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool isSelected=false;

    private Vector3 firstPosition;
    private Vector3 lastPosition;

    private float dragDistance;

    public PlayerData playerData;

    private void Start() 
    {
        //Spin baslayarak donuyorlar. Sureleri Update de azaliyor
        
    }
    private void Update() 
    {
        CheckMove();   
    }

    //Or Touch with Raycast ?
    private void OnMouseDown() 
    {
        for (int i = 0; i < playerData.AllSpinners.Count; i++)
        {
            playerData.AllSpinners[i].isSelected=false;
        }

        isSelected=true;
        
    }

    private void CheckMove()
    {

        if(Input.touchCount>0 && isSelected)
        {
            Touch touch=Input.GetTouch(0);
            if(touch.phase==TouchPhase.Began)
            {
                firstPosition=touch.position;
                lastPosition=touch.position;
            }

            else if(touch.phase==TouchPhase.Moved)
            {
                lastPosition=touch.position;
            }

            else if(touch.phase==TouchPhase.Ended)
            {
                lastPosition=touch.position;

                if(Mathf.Abs(lastPosition.x-firstPosition.x)>Mathf.Abs(lastPosition.y-firstPosition.y))
                {
                    if(lastPosition.x>firstPosition.x)
                    {
                        Debug.Log("SWIPE RIGHT");
                        
                    }
                    else
                    {
                        Debug.Log("SWIPE LEFT");
                    }
                }


            }
        }
    }
}
