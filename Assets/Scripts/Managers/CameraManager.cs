using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;

    public CinemachineVirtualCamera cm;
    public Transform cmCamera;
    public CinemachineImpulseSource ImpulseSource;

    Vector3 cameraInitialPosition;
    [SerializeField] private float closeValue,normalValue;

    [Header("Shake Control")]
    public float shakeMagnitude = 0.05f;
    public float shakeTime = 0.5f;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnTargetSpin,OnTargetSpin);
        EventManager.AddHandler(GameEvent.OnGameOver,GameOver);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnTargetSpin,OnTargetSpin);
        EventManager.RemoveHandler(GameEvent.OnGameOver,GameOver);
    }

    void OnTargetSpin()
    {
        //ShakeIt();
        ChangeFieldOfView(closeValue,0.1f);
        ImpulseSource.GenerateImpulse();
    }

    

    

    
    public void ChangeFieldOfView(float fieldOfView, float duration = 1)
    {
        DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, fieldOfView, duration).OnComplete(()=>{
            ResetFieldOfView(normalValue,0.1f);
        });
    }

    private void ResetFieldOfView(float fieldOfView, float duration = 1)
    {
        DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, fieldOfView, duration);
    }
   
    public void ResetCamera()
    {
        cm.m_Priority = 1;
    }

    void GameOver()
    {
        DOTween.To(() => mainCamera.fieldOfView, x => mainCamera.fieldOfView = x, 60, 0.5f).OnComplete(()=>
        {
            //EventManager.Broadcast(GameEvent.OnUpdateGameOverManager);
        });
        
    }


    #region CameraShaker

    private void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);

    }

    private void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float cameraShakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        Vector3 cameraIntermediatePosition = mainCamera.transform.position;
        cameraIntermediatePosition.x += cameraShakingOffsetX;
        cameraIntermediatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermediatePosition;
    }

    private void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }
    #endregion    
}
