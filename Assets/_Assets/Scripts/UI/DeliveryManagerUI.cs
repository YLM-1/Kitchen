using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate; 

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.instance.OnRecipeSpawned+= DeliveryManager_OnecipeSpawned;
        DeliveryManager.instance.OnRecipeCompleted+= DeliveryManager_OnecipeCompleted;
        UpdateVisual();
    }

    private void DeliveryManager_OnecipeSpawned(object sender,System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnecipeCompleted(object sender,System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if (child == recipeTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }
        foreach(RecipeSO recipeSO in DeliveryManager.instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform=Instantiate(recipeTemplate,container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
     }
}
