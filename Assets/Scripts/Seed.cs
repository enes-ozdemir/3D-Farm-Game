using System.Collections.Generic;
using Farm.Profile;
using UnityEngine;

namespace Farm.Entity
{
    public class Seed : MonoBehaviour, IEntity
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public SeedProfile seedProfile;

        public ProductJob ProductJob;

        private void Start()
        {
            Id = System.Guid.NewGuid().ToString();
        }
    }
}