using System;
using Farm.Entity;
using Farm.Profile;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Farm.Processor
{
    public class PlantProcessor : TimeBasedProcessor
    {
        private List<GameObject> Seeds = new List<GameObject>();

        private Inventory _inventory;

        public UpgradeManager upgradeManager;

        public void Start()
        {
            Seeds = GetComponentsInChildren<Seed>().ToList().Select(seed => seed.gameObject).ToList();
            upgradeManager = GameObject.Find("Logic").GetComponent<UpgradeManager>();
            LoadProfile();
            _inventory = FindObjectOfType<Inventory>();
        }

        public override void Process(ProductJob productJob, int index)
        {
            Seeds[index].AddComponent<Product>();
            Product product = Seeds[index].GetComponent<Product>();
            Seeds[index].name = index.ToString();
            product.Name = productJob.Name;
            product.Type = "ground";
            product.jobId = productJob.id;
            product.productId = Guid.NewGuid().ToString();
        }

        public override void UpdateState(int stage, int jobIndex)
        {
            //TODO dont destroy & instantiate, just change model
            List<ProductStage> stages = Profile.ProductJobs[jobIndex].stages;
            //GameObject newStage = Instantiate(stages[stage].prefab, Seeds[jobIndex].transform.parent);
            GameObject destroyedObject = Seeds[jobIndex];
            GameObject newStage = stages[stage].prefab;
            destroyedObject.GetComponent<MeshFilter>().sharedMesh = newStage.GetComponent<MeshFilter>().sharedMesh;
            destroyedObject.GetComponent<MeshRenderer>().sharedMaterials = newStage.GetComponent<MeshRenderer>().sharedMaterials;
           
            //newStage.transform.position = Seeds[jobIndex].transform.position;
            //Seed newStageSeed = newStage.GetComponent<Seed>();
            //Seed destroyedObjectSeed = newStage.GetComponent<Seed>();
            //newStageSeed.seedProfile = destroyedObjectSeed.seedProfile;
            //newStageSeed.ProductJob = Profile.ProductJobs[jobIndex];
            //Seeds[jobIndex] = newStage;
            //Destroy(destroyedObject);
        }

        public override void Collect(string jobId, string productId)
        {
            int jobIndex = Profile.ProductJobs.FindIndex(job => job.id == jobId);
            UpdateState(0, jobIndex);
            stages[jobIndex] = 0;
            finishedCount -= 1;
            _inventory.AddItem(Profile.ProductJobs[jobIndex].ResultProduct);
        }

        public override void AfterProcess(ProductJob job, int jobIndex)
        {
            Profile.ProductJobs[jobIndex].ResetPreRequirements();
        }

        public override bool PreProcess(ProductJob job, int jobIndex)
        {
            return true;
        }

        public override bool CanProcess(ProductJob job, int jobIndex)
        {
            return job.CanProduct();
        }

        public void LoadProfile()
        {
            List<ProductJob> productJobs = new List<ProductJob>();
            for (int i = 0; i < Seeds.Count; i++)
            {
                GameObject seedObject = Seeds[i];
                Assert.IsNotNull(seedObject, "Seed object cannot be null");

                Seed seed = seedObject.GetComponent<Seed>();
                Assert.IsNotNull(seed, "Seed cannot be null");

                SeedProfile seedProfile = seed.seedProfile;

                List<ProductStage> stages = new List<ProductStage>();
                for (int j = 0; j < seedProfile.stages.Count; j++)
                {
                    SeedStageProfile seedStageProfile = seedProfile.stages[j];
                    ProductStage productStage = new ProductStage(seedStageProfile.State, seedStageProfile.Time,
                        seedStageProfile.prefab);
                    stages.Add(productStage);
                }

                List<BoostProfile> boostProfiles = upgradeManager.getSeedBoostProfileByName(seedProfile.Name);
                ProductJob productJob = new ProductJob(seedProfile.Name,
                    seedProfile.ProductAmount,
                    stages,
                    seed,
                    seedProfile.resultProduct,
                    seedProfile.preRequirementProfile,
                    boostProfiles);
                seed.ProductJob = productJob;
                productJobs.Add(productJob);
            }

            Profile = new PlantAreaProfile("Area", "ground", productJobs);
            Status = true;
        }
    }
}