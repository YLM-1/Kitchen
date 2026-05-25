using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountdownUI : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI countdownText;

  private void Start()
    {
        KitchenGameManager.Instance.OnStateChange+=KitchenGameManager_OnStateChange;

        Hide();
    }

    private void Update()
    {
        countdownText.text=KitchenGameManager.Instance.GetCountdownToStartTimer().ToString("F0");
    }

  private void KitchenGameManager_OnStateChange(object sender,System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Show();
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
