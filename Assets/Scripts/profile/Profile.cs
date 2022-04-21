using System;
using System.Collections.Generic;
using System.Linq;
using Farm.Entity;
using UnityEngine;

namespace Farm.Profile
{
    public interface IProfile
    {
        string Name { get; set; }

        string Type { get; set; }

        List<ProductJob> ProductJobs { get; set; }
    }

    public class ProductJob
    {
        public readonly string Name;

        public float Time;

        public int ProductAmount;

        public List<SeedPreRequirement> seedPreRequirements;

        public readonly List<ProductStage> stages;

        public string id;

        public readonly Seed Seed;

        public readonly Item ResultProduct;

        public readonly List<BoostProfile> boosts;

        public ProductJob(string name, int productAmount, List<ProductStage> stages, Seed seed, Item resultProduct,
            List<SeedPreRequirementProfile> seedPreRequirementProfiles,
            List<BoostProfile> boosts)
        {
            Name = name;
            ProductAmount = productAmount;
            this.stages = stages;
            this.Seed = seed;
            this.id = seed.Id;
            this.ResultProduct = resultProduct;
            this.Time = stages[stages.Count - 1].Time;
            this.seedPreRequirements = seedPreRequirementProfiles.ConvertAll(p => p.ToClass()).ToList();
            this.boosts = boosts;

            SetupBoost();
        }

        private void SetupBoost()
        {
            boosts.ForEach(boost =>
            {
                switch (boost.Type)
                {
                    case BoostType.TIME_REDUCE:
                    {
                        Time *= boost.Value / 100;
                        stages.ForEach(stage => { stage.Time *= boost.Value / 100; });
                        break;
                    }
                    case BoostType.QUALITY_INCREMENT:
                    {
                        break;
                    }
                    case BoostType.PRODUCTION_INCREMENT:
                    {
                        ProductAmount = (int) Math.Round(ProductAmount * boost.Value);
                        break;
                    }
                }
            });
        }

        public bool CanProduct()
        {
            if (this.seedPreRequirements.Count == 0) return true;

            return this.seedPreRequirements.TrueForAll(requirement =>
            {
                return this.CheckPreRequirementWithType(requirement.Type);
            });
        }

        public bool CheckPreRequirementWithType(SeedPreRequirementType type)
        {
            SeedPreRequirement requirement = this.seedPreRequirements.Find(p => p.Type == type);
            if (requirement == null)
            {
                return false;
            }
            return requirement.Amount >= requirement.profile.Amount;
        }

        public void UpdatePreRequirementAmountWithType(SeedPreRequirementType type, float Amount)
        {
            SeedPreRequirement requirement = this.seedPreRequirements.Find(p => p.Type == type);
            if (requirement == null)
            {
                throw new Exception("Requirement " + type + " not found on this profile " + this.Name);
            }
            
            requirement.Amount += Amount;
        }

        public void ResetPreRequirements()
        {
            this.seedPreRequirements.ForEach(requirement =>
            {
                requirement.Amount = 0f;
            });
        }
    }

    public class ProductStage
    {
        public readonly string State;
        public float Time;
        public readonly GameObject prefab;

        public ProductStage(string state, float time, GameObject prefab)
        {
            State = state;
            Time = time;
            this.prefab = prefab;
        }
    }
}