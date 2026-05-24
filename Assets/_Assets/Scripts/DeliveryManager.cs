using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager : MonoBehaviour
{

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager instance{get;private set;}
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax=4f;
    private int waitingRecipesMax=4;

    private void Awake()
    {
        instance=this;
        waitingRecipeSOList=new List<RecipeSO>(); 
    }

    private void Update()
    { 
        spawnRecipeTimer-=Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer=spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count<waitingRecipesMax)
            {
                 RecipeSO waitingRecipeSO=recipeListSO.recipeSOList[UnityEngine.Random.Range(0,recipeListSO.recipeSOList.Count)];
                //  Debug.Log(waitingRecipeSO.recipeName);
                 waitingRecipeSOList.Add(waitingRecipeSO);

                 OnRecipeSpawned?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO=waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe=true;
                //Has the same number of ingredients
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound=false;
                    //循环食谱中所有配料
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //循环盘子上的所有配料
                        if (plateKitchenObjectSO==recipeKitchenObjectSO)
                        {
                            ingredientFound=true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        //食谱没找到这个食物
                        plateContentsMatchesRecipe=false;
                    }
                }
                if (plateContentsMatchesRecipe)
                {
                    //玩家交付正确食谱
                    // Debug.Log("交付正确食谱");
                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this,EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this,EventArgs.Empty);
                    return;
                }
            }
        }
        //没有找到匹配的
        // Debug.Log("交付错误食谱");
        OnRecipeFailed?.Invoke(this,EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
}

