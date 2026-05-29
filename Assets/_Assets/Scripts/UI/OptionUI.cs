using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionUI : MonoBehaviour
{
  public static OptionUI Instance{get;private set;}
  [SerializeField] private Button soundEffectButton;
  [SerializeField] private Button musicButton;
  [SerializeField] private Button closeButton;
  [SerializeField] private Button moveUpButton;
  [SerializeField] private Button moveDownButton;
  [SerializeField] private Button moveLeftButton;
  [SerializeField] private Button moveRightButton;
  [SerializeField] private Button interacrButton;
  [SerializeField] private Button interactAlternateButton;
  [SerializeField] private Button PauseButton;
  [SerializeField] private TextMeshProUGUI soundEffectText;
  [SerializeField] private TextMeshProUGUI musicText;
  [SerializeField] private TextMeshProUGUI moveUpText;
  [SerializeField] private TextMeshProUGUI moveDownText;
  [SerializeField] private TextMeshProUGUI moveLeftText;
  [SerializeField] private TextMeshProUGUI moveRightText;
  [SerializeField] private TextMeshProUGUI InteractText;
  [SerializeField] private TextMeshProUGUI InteractAlternateText;
  [SerializeField] private TextMeshProUGUI PauseText;
  [SerializeField] private Transform presstoRebindKeyTransform;
  private void Awake()
  {
        Instance=this;

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
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
        moveUpButton.onClick.AddListener(() => {RebinBindinng(GameInput.E_Binding.Move_Up);});
        moveDownButton.onClick.AddListener(() => {RebinBindinng(GameInput.E_Binding.Move_Down);});
        moveLeftButton.onClick.AddListener(() => {RebinBindinng(GameInput.E_Binding.Move_Left);});
        moveRightButton.onClick.AddListener(() => {RebinBindinng(GameInput.E_Binding.Move_Right);});
        interacrButton.onClick.AddListener(() => {RebinBindinng(GameInput.E_Binding.Interact);});
        interactAlternateButton.onClick.AddListener(() => {RebinBindinng(GameInput.E_Binding.InteractAlternate);});
        PauseButton.onClick.AddListener(() => {RebinBindinng(GameInput.E_Binding.Pause);});
  }
  private void Start()
  {
    KitchenGameManager.Instance.OnGameUnPaused+=KitchenManager_OnGameUnPaused;
    UpdateVisual();
    HidePressToRebindKey();
    Hide();
  }
  private void KitchenManager_OnGameUnPaused(object sender,System.EventArgs e)
  {
    Hide();
  }

  private void UpdateVisual()
  {
      soundEffectText.text="Sound Effect:"+Mathf.Round(SoundManager.instance.GetVolume()*10f);
      musicText.text="Music:"+Mathf.Round(MusicManager.Instance.GetVolume()*10f);

      moveUpText.text=GameInput.Instance.GetBindingText(GameInput.E_Binding.Move_Up);
      moveDownText.text=GameInput.Instance.GetBindingText(GameInput.E_Binding.Move_Down);
      moveLeftText.text=GameInput.Instance.GetBindingText(GameInput.E_Binding.Move_Left);
      moveRightText.text=GameInput.Instance.GetBindingText(GameInput.E_Binding.Move_Right);
      InteractText.text=GameInput.Instance.GetBindingText(GameInput.E_Binding.Interact);
      InteractAlternateText.text=GameInput.Instance.GetBindingText(GameInput.E_Binding.InteractAlternate);
      PauseText.text=GameInput.Instance.GetBindingText(GameInput.E_Binding.Pause);
  }

  public void Show()
  {
    gameObject.SetActive(true);
  }
  private void Hide()
  {
    gameObject.SetActive(false);
  }

  private void ShowPressToRebindKey()
  {
    presstoRebindKeyTransform.gameObject.SetActive(true);
  }
   private void HidePressToRebindKey()
  {
    presstoRebindKeyTransform.gameObject.SetActive(false);
  }
  private void RebinBindinng(GameInput.E_Binding binding)
  {
    ShowPressToRebindKey();
    GameInput.Instance.RebindBinding(binding, () =>
    {
      HidePressToRebindKey();
      UpdateVisual();
    });
  }
}
