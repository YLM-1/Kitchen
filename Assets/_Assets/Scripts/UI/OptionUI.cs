using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionUI : MonoBehaviour
{
  [SerializeField] private Button soundEffectButton;
  [SerializeField] private Button musicButton;
  [SerializeField] private TextMeshProUGUI soundEffectText;
  [SerializeField] private TextMeshProUGUI musicText;

  private void Awake()
  {
        soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

  }
  private void Start()
  {
    
    UpdateVisual();
  }

  private void UpdateVisual()
  {
      soundEffectText.text="Sound Effect:"+Mathf.Round(SoundManager.instance.GetVolume()*10f);
      musicText.text="Music:"+Mathf.Round(MusicManager.Instance.GetVolume()*10f);
  }
}
