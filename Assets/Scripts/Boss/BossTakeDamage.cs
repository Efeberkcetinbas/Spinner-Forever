using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossTakeDamage : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer meshRenderer;

    [SerializeField] private ParticleSystem damageParticle;

    [SerializeField] private float duration=0.2f;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnTargetSpin,OnHit);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnTargetSpin,OnHit);
    }

    private void OnHit()
    {
        damageParticle.Play();
        meshRenderer.material.color=Color.red;
        transform.DOScale(Vector3.one*1.5f,0.2f).OnComplete(()=>transform.DOScale(Vector3.one,0.2f));
        Invoke("OnDamageRed",duration);
    }

    private void OnDamageRed()
    {
        meshRenderer.material.color=Color.white;
    }

   
}
