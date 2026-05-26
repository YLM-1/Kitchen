using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{

    public enum E_Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene,
    }
  private static E_Scene targetScene;//静态类只能有静态成员变量

  public static void Load(E_Scene targetScene)
    {
        Loader.targetScene=targetScene;

        SceneManager.LoadScene(E_Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
