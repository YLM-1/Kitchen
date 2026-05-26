using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
  [SerializeField] private Button playBotton;
  [SerializeField] private Button quitButton;

  private void Awake()
  {
    playBotton.onClick.AddListener(()=>{
        Loader.Load(Loader.E_Scene.GameScene);
    });
    quitButton.onClick.AddListener(()=>{
        Application.Quit();
    });
    Time.timeScale=1f;
  }
}
