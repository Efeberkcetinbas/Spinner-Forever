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
    [SerializeField] private SkinnedMeshRenderer spinnerMesh;
    [SerializeField] private Transform Root;

    [SerializeField] private Transform pointPos;

    [SerializeField] private Animator animator;


    private float dragDistance;
    private float timer;

    [Header("UI's")]
    public Image ProgressImage;

    [Header("Randomness")]
    //Shoptan almali olur
    [SerializeField] private List<Material> materials=new List<Material>();

    private int matIndex;

    private float rotIndex;


    [Header("Data's")]
    public PlayerData playerData;
    public GameDataV gameData;

    private void Start() 
    {
        //Spin baslayarak donuyorlar. Sureleri Update de azaliyor
        timer=playerData.MaxSpinTime;
        SetRandomMat();
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
            playerData.AllSpinners[i].animator.SetBool("CanSpin",false);
        }

        isSelected=true;
        Debug.Log(playerData.AllSpinners.Count);
        
        EventManager.Broadcast(GameEvent.OnSelectedSpin);
        selected.SetActive(true);
        //Animasyon yerine elle yapsak? BKNZ : 2.1
        
        animator.SetBool("CanSpin",true);
        
    }

    private Material SetRandomMat()
    {
        matIndex=Random.Range(0,materials.Count);
        spinnerMesh.material=materials[matIndex];
        return spinnerMesh.material;
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
                    EventManager.Broadcast(GameEvent.OnTargetSpin);
                    StartCoinMove();

                    //!!!2.1
                    /*if(lastPosition.x>firstPosition.x)
                    {
                        rotIndex+=5;
                        if(rotIndex>360)
                            rotIndex=0;

                        Root.DORotate(new Vector3(270,rotIndex,0),0.1f,RotateMode.Fast);
                    }
                    else
                    {
                        rotIndex-=5;
                        if(rotIndex<-360)
                            rotIndex=0;

                        Root.DORotate(new Vector3(270,rotIndex,0),0.1f,RotateMode.Fast);
                    }*/
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
