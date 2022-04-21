using Farm.Profile;
using UnityEngine;

namespace Farm.Processor
{
    public abstract class BaseProcessor : MonoBehaviour, IProcessor
    {
        public IProfile Profile { get; set; }

        public bool Status { get; set; }

        public string Id { get; set; } = System.Guid.NewGuid().ToString();

        public abstract void AfterProcess(ProductJob job, int jobIndex);

        public abstract bool PreProcess(ProductJob job, int jobIndex);

        public abstract void Process(ProductJob job, int jobIndex);

        public abstract bool CanProcess(ProductJob job, int jobIndex);
    }
}