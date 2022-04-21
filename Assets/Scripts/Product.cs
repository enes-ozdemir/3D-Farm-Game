using Farm.Processor;
using UnityEngine;

namespace Farm.Profile
{
    public class Product : MonoBehaviour, ICollectable
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string jobId;

        public string productId;

        public void OnMouseDown()
        {
            if (jobId != null && productId != null)
            {
                Collect();
            }
        }

        public BaseProcessor GetProcessor()
        {
            BaseProcessor processor = ProcessManager.GetProcessor(jobId);
            return processor;
        }

        public void Collect()
        {
            BaseProcessor processor = GetProcessor();

            if (processor != null && processor.GetType() == typeof(PlantProcessor))
            {
                (processor as PlantProcessor).Collect(jobId, productId);
            }
        }
    }
}