using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter :BaseCounter
{
    public event EventHandler OnPlateSpawned;//生成一个盘子
    public event EventHandler OnPlateRemoved;//拿走一个盘子

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer=0f;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmout=0;
    private int platesSpawnedAmoutMax=4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer> spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;

            if (platesSpawnedAmout < platesSpawnedAmoutMax)
            {
                platesSpawnedAmout++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (platesSpawnedAmout > 0)
            {
                
                platesSpawnedAmout--;
                KitchenObject.SpawnKitchenObjectSO(plateKitchenObjectSO, player);
                //Transform kitchenObjectTransform = Instantiate(plateKitchenObjectSO.prefab);
                //KitchenObject plateKitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
                //plateKitchenObject.SetKitchenObjectParent(player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}