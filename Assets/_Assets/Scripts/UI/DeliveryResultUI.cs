using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeliveryResultUI : MonoBehaviour
{
  private const string POPUP="Popup";
  [SerializeField] private Image backgroundImage;
  [SerializeField] private Image iconImage;
  [SerializeField] private TextMeshProUGUI messageText;
  [SerializeField] private Color successcolor;
  [SerializeField] private Color failColor;
  [SerializeField] private Sprite successSprite;
  [SerializeField] private Sprite failSprite;

  private Animator animator;

  private void Awake()
  {
    animator=GetComponent<Animator>();
    gameObject.SetActive(false);
  }
  private void Start()
  {
    DeliveryManager.Instance.OnRecipeSuccess+= DeliveryManager_OnRecipeSuccess;
    DeliveryManager.Instance.OnRecipeFailed+= DeliveryManager_OnRecipeFailed;
  }

  private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
  {
    animator.SetTrigger(POPUP);
    backgroundImage.color=successcolor;
    iconImage.sprite=successSprite;
    messageText.text="DELIVERY/SUCCESS";
  }

  private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
  {
    animator.SetTrigger(POPUP);
    backgroundImage.color=failColor;
    iconImage.sprite=failSprite;
    messageText.text="DELIVERY/FAIL";
  }


}
