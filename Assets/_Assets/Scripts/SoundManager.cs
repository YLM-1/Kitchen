using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME="soundEffectsVolume";
    public static SoundManager instance{get;private set;}
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume=1f;

    private void Awake()
    {
        volume=PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME,1f);
        instance=this;
    }
    private  void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess+= DeliverManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed+= DeliveryManager_OnRecipeFailed;
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
    private void PlaySound(AudioClip audioClip,Vector3 position,float volumeMultiply)
    {
        AudioSource.PlayClipAtPoint(audioClip,position,volumeMultiply);
    }

    public void PlayFootstepSound(Vector3 position,float volume=1f)
    {
        PlaySound(audioClipRefsSO.footStep,position,volume);
    }

    public void PlayCountdownSound()
    {
        PlaySound(audioClipRefsSO.warning,Vector3.zero,volume);
    }
    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.warning,position,volume);
    }

    public void ChangeVolume()
    {
        volume+=0.1f;
        if(volume>1f)
        {
            volume=0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME,volume);//Unity自动将PLAYER_PREFS_SOUND_EFFECTS_VOLUME替换成soundEffectsVolume
        PlayerPrefs.Save();

    }

    public float GetVolume()
    {
        return  volume;
    }
}
