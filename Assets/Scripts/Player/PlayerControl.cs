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

    [SerializeField] private ParticleSystem electricParticle;




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
            if(playerData.CanSpin)
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
            //playerData.AllSpinners[i].animator.SetBool("CanSpin",false);
        }

        isSelected=true;
        Debug.Log(playerData.AllSpinners.Count);
        
        EventManager.Broadcast(GameEvent.OnSelectedSpin);
        selected.SetActive(true);
        //Animasyon yerine elle yapsak? BKNZ : 2.1
        
        //animator.SetBool("CanSpin",true);
        
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
                    StartCoroutine(SetSpinTrue());
                    electricParticle.Play();
                    //switch case
                    //!!!2.1
                    if(lastPosition.x>firstPosition.x)
                        Root.DOLocalRotate(new Vector3(0, 360, 0), .2f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
                    else
                        Root.DOLocalRotate(new Vector3(0, -360, 0), .2f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
                }
            }

        }

        
    }

    private IEnumerator SetSpinTrue()
    {
        playerData.CanSpin=false;
        yield return new WaitForSeconds(playerData.ClickSpin);
        playerData.CanSpin=true;
    }

    private void StartCoinMove()
    {
        GameObject coin=Instantiate(increaseScorePrefab,pointPos.transform.position,increaseScorePrefab.transform.rotation);
        coin.transform.LookAt(Camera.main.transform);
        var pos=coin.transform.localPosition;
        coin.transform.DOLocalJump(new Vector3(pos.x,pos.y+2,pos.z),1,1,1,false);
        //coin.transform.DOScale(Vector3.zero,1.5f);
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().text=" + " + playerData.MaxDamageAmount.ToString() + " xP";
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0,1.5f).OnComplete(()=>coin.transform.GetChild(0).gameObject.SetActive(false));
        Destroy(coin,2);
    }
}
