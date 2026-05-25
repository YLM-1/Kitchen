using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChange+=KitchenGameManager_OnStateChange;

        Hide();
    }

  private void KitchenGameManager_OnStateChange(object sender,System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsGameOver())
        {
            Show();
             recipesDeliveredText.text=DeliveryManager.instance.GetSuccessfulRecipesAmount().ToString();
        }
        else
        {
            Hide();
        }
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
