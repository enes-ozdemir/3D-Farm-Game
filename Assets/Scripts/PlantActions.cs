using System;
using System.Collections.Generic;
using Farm.Entity;
using Farm.Processor;
using Farm.Profile;
using UnityEngine;

public class PlantActions : MonoBehaviour
{
    public float distance = 10;
    public LayerMask layerMask;

    public RaycastHit[] hits;

    public void Update()
    {
        CheckRay();
        CheckCollect();
        CheckWatering();
    }
    
    private void CheckRay()
    {
        hits = Physics.BoxCastAll(transform.position, gameObject.transform.lossyScale / 2, transform.forward,
            gameObject.transform.rotation, distance, layerMask);
    }

    private void CheckFertilizer()
    {
        if (hits.Length == 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (RaycastHit raycastHit in hits)
            {
                Seed seed = raycastHit.transform.gameObject.GetComponent<Seed>();
                if (seed != null && seed.ProductJob != null)
                {
                    bool isFertilized = seed.ProductJob.CheckPreRequirementWithType(SeedPreRequirementType.FERTILIZER);
                    if (!isFertilized)
                    {
                        seed.ProductJob.UpdatePreRequirementAmountWithType(SeedPreRequirementType.FERTILIZER, 1);
                        Debug.Log("Fertilized with 1");
                    }
                }
            }
        }
    }

    private void CheckWatering()
    {
        if (hits.Length == 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (RaycastHit raycastHit in hits)
            {
                Seed seed = raycastHit.transform.gameObject.GetComponent<Seed>();
                if (seed != null && seed.ProductJob != null)
                {
                    bool isFinished = seed.ProductJob.CheckPreRequirementWithType(SeedPreRequirementType.WATER);
                    if (!isFinished)
                    {
                        seed.ProductJob.UpdatePreRequirementAmountWithType(SeedPreRequirementType.WATER, 1);
                        Debug.Log(1);
                    }
                }
            }
        }
    }

    private void CheckCollect()
    {
        if (hits.Length == 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (RaycastHit raycastHit in hits)
            {
                ICollectable product = raycastHit.transform.gameObject.GetComponent<ICollectable>();
                if (product != null)
                {
                    product.Collect();
                }
            }
        }
    }
}