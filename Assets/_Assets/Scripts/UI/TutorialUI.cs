using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TutorialUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI KeyMoveUpText;
   [SerializeField] private TextMeshProUGUI KeyMoveUDownText;
   [SerializeField] private TextMeshProUGUI KeyMoveLeftText;
   [SerializeField] private TextMeshProUGUI KeyMoveRightText;
   [SerializeField] private TextMeshProUGUI KeyInteractText;
   [SerializeField] private TextMeshProUGUI KaeyInteractAlternateText;
   [SerializeField] private TextMeshProUGUI KeyPauseText;

   private void Start()
   {
       GameInput.Instance.OnBindingRebind+=GameInput_OnBindingRebind;
       KitchenGameManager.Instance.OnStateChanged+= KitchenGameManager_OnStateChange;
       UpdateVisual();
       Show();
   }
   private void KitchenGameManager_OnStateChange(object sender,System.EventArgs e)
   {
       if (KitchenGameManager.Instance.IsCountdownToStartActive())
       {
           Hide();
       }
   }
   private void GameInput_OnBindingRebind(object sender,System.EventArgs e)
   {
      if (KitchenGameManager.Instance.IsCountdownToStartActive())
      {
         Hide();
      }
   }

   private void UpdateVisual()
   {
       KeyMoveUpText.text=GameInput.Instance.GetBindingText(GameInput.E_Binding.Move_Up);
       KeyMoveUDownText.text=GameInput.Instance.GetBindingText(GameInput.E_Binding.Move_Down);
       KeyMoveLeftText.text=GameInput.Instance.GetBindingText(GameInput.E_Binding.Move_Left);
       KeyMoveRightText.text=GameInput.Instance.GetBindingText(GameInput.E_Binding.Move_Right);
       KeyInteractText.text=GameInput.Instance.GetBindingText(GameInput.E_Binding.Interact);
       KaeyInteractAlternateText.text=GameInput.Instance.GetBindingText(GameInput.E_Binding.InteractAlternate);
       KeyPauseText.text=GameInput.Instance.GetBindingText(GameInput.E_Binding.Pause);
   }

   private void Show()
   {
      gameObject.SetActive(true);
   }

   private void Hide()
   {
      gameObject.SetActive(false);
   }
}
