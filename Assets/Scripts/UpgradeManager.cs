using System;
using System.Collections.Generic;
using Farm.Core;
using Farm.Profile;
using UnityEngine;

namespace Farm
{
    public enum ComponentEnum
    {
        SEED
    }

    public class UpgradeManager : Singleton<UpgradeManager>
    {
        private KeyValueStore<ComponentEnum, KeyValueStore<string, List<BoostProfile>>> boostProfiles;

        public List<BoostProfile> carrotBoostProfiles = new List<BoostProfile>();

        public UpgradeManager(KeyValueStore<ComponentEnum, KeyValueStore<string, List<BoostProfile>>> boostProfiles)
        {
            this.boostProfiles = boostProfiles;
        }

        public UpgradeManager()
        {
            this.boostProfiles =
                new KeyValueStore<ComponentEnum, KeyValueStore<string, List<BoostProfile>>>(
                    new Dictionary<ComponentEnum, KeyValueStore<string, List<BoostProfile>>>());

        }

        private void Awake()
        {
            addSeedBoostProfile("Carrot", carrotBoostProfiles);
        }


        public List<BoostProfile> getSeedBoostProfileByName(string Name)
        {
            var seedStore = this.boostProfiles.getOrDefault(ComponentEnum.SEED,
                new KeyValueStore<string, List<BoostProfile>>(new Dictionary<string, List<BoostProfile>>()));
            return seedStore.getOrDefault(Name, new List<BoostProfile>());
        }

        public void addSeedBoostProfile(string Name, BoostProfile profile)
        {
            var seedStore = this.boostProfiles.getOrDefault(ComponentEnum.SEED,
                new KeyValueStore<string, List<BoostProfile>>(new Dictionary<string, List<BoostProfile>>()));
            if (seedStore.exist(Name))
            {
                var profiles = seedStore.getOrDefault(Name, new List<BoostProfile>());
                profiles.Add(profile);
                seedStore.set(Name, profiles);
            }
            else
            {
                var profiles = new List<BoostProfile>();
                seedStore.set(Name, profiles);
            }
            this.boostProfiles.set(ComponentEnum.SEED, seedStore);
        }

        public void addSeedBoostProfile(string Name, List<BoostProfile> profile)
        {
            var seedStore = this.boostProfiles.getOrDefault(ComponentEnum.SEED,
                new KeyValueStore<string, List<BoostProfile>>(new Dictionary<string, List<BoostProfile>>()));
            var item = seedStore.getOrDefault(Name, new List<BoostProfile>());
            item.AddRange(profile);
            seedStore.set(Name, item);
            this.boostProfiles.set(ComponentEnum.SEED, seedStore);
        }

        protected override void OnApplicationQuitCallback()
        {
        }

        protected override void OnEnableCallback()
        {
        }
    }
}