using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class PlayerControl : MonoBehaviour
{
    [Header("SelectionControl")]
    public bool isSelected=false;

    private Vector3 firstPosition;
    private Vector3 lastPosition;

    [SerializeField] private GameObject selected;
    [SerializeField] private GameObject increaseScorePrefab;

    [SerializeField] private Transform pointPos;

    private float dragDistance;
    private float timer;

    [Header("UI's")]
    public Image ProgressImage;


    public PlayerData playerData;
    public GameDataV gameData;

    private void Start() 
    {
        //Spin baslayarak donuyorlar. Sureleri Update de azaliyor
        timer=playerData.MaxSpinTime;
    }
    private void Update() 
    {
        if(!gameData.isGameEnd)
        {
            CheckMove();
            StartTimer();   
        }
        
    }

    //Or Touch with Raycast ?
    private void OnMouseDown() 
    {
        for (int i = 0; i < playerData.AllSpinners.Count; i++)
        {
            playerData.AllSpinners[i].isSelected=false;
            playerData.AllSpinners[i].selected.SetActive(false);
        }

        isSelected=true;
        selected.SetActive(true);
        
    }

    private void StartTimer()
    {
        if(timer>0)
        {
            timer-=Time.deltaTime;
            ProgressImage.DOFillAmount(timer/playerData.MaxSpinTime,0.1f);
        }

        else
        {
            Debug.Log("Game Is Over");
            EventManager.Broadcast(GameEvent.OnGameOver);
        }
        
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
                    timer=playerData.MaxSpinTime;
                    StartCoinMove();
                }


            }
        }
    }

    private void StartCoinMove()
    {
        GameObject coin=Instantiate(increaseScorePrefab,pointPos.transform.position,increaseScorePrefab.transform.rotation);
        coin.transform.DOLocalJump(coin.transform.localPosition,1,1,1,false);
        //coin.transform.DOScale(Vector3.zero,1.5f);
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().text=" + " + playerData.MaxMoneyAmount.ToString();
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0,1.5f).OnComplete(()=>coin.transform.GetChild(0).gameObject.SetActive(false));
        Destroy(coin,2);
    }
}
