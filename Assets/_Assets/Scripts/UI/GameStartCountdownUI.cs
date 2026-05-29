using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountdownUI : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI countdownText;

  private Animator animator;
  private int previousCountdownNumber;
  private const string NUMBER_POP="NumberPopup";

  private void Awake()
  {
    animator=GetComponent<Animator>();
  }

  private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged+=KitchenGameManager_OnStateChange;

        Hide();
    }

    private void Update()
    {
        int countdownNumber=Mathf.CeilToInt(KitchenGameManager.Instance.GetCountdownToStartTimer());
        countdownText.text=countdownNumber.ToString();

        if(countdownNumber!=previousCountdownNumber)
        {
            previousCountdownNumber=countdownNumber;
            animator.SetTrigger(NUMBER_POP);//让动画系统立刻播放名为 "NUMBER_POP" 的动画
            SoundManager.instance.PlayCountdownSound();//播放倒计时音效
        }
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
