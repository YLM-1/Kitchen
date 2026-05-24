using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance{get;private set;}
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Awake()
    {
        instance=this;
    }
    private  void Start()
    {
        DeliveryManager.instance.OnRecipeSuccess+= DeliverManager_OnRecipeSuccess;
        DeliveryManager.instance.OnRecipeFailed+= DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut+= CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething+= Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere+= BaseCounter_OnAnyObjetPlacedHere;
        TrashCounter.OnAnyObjectTrashed+= TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender,System.EventArgs e)
    {
        TrashCounter trashCounter=sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash,trashCounter.transform.position);
    }
    private void BaseCounter_OnAnyObjetPlacedHere(object sender,System.EventArgs e)
    {
        BaseCounter baseCounter=sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop,baseCounter.transform.position);
    }
   
    private void Player_OnPickedSomething(object sender,System.EventArgs e)
    {
        
        PlaySound(audioClipRefsSO.objectPickup,Player.Instance.transform.position);
    }
    private void CuttingCounter_OnAnyCut(object sender,System.EventArgs e)
    {
        CuttingCounter cuttingCounter=sender as CuttingCounter;//传的是object,object是访问不到CuttingCounte的组件，必须转换才能用它的属性
        PlaySound(audioClipRefsSO.chop,cuttingCounter.transform.position);
    }
    private void DeliverManager_OnRecipeSuccess(object sender,System.EventArgs e)
    {
        DeliveryCounter deliveryCounter=DeliveryCounter.instance;
        PlaySound(audioClipRefsSO.deliverySuccess,deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender,System.EventArgs e)
    {
        DeliveryCounter deliveryCounter=DeliveryCounter.instance;
        PlaySound(audioClipRefsSO.deliveryFail,deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArry,Vector3 position,float volume = 1f)
    {
        PlaySound(audioClipArry[Random.Range(0,audioClipArry.Length)],position,volume);
    }
    private void PlaySound(AudioClip audioClip,Vector3 position,float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip,position,volume);
    }

    public void PlayFootstepSound(Vector3 position,float volume)
    {
        PlaySound(audioClipRefsSO.footStep,position,volume);
    }
}
