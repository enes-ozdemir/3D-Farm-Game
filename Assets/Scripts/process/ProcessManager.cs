using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farm.Processor
{
    public class ProcessManager : MonoBehaviour
    {

        public static List<BaseProcessor> Processors;

        void Start()
        {
            Processors = new List<BaseProcessor>(FindObjectsOfType<BaseProcessor>());
        }

        public static BaseProcessor GetProcessor(string id)
        {
            return Processors.Find(processor => processor.Profile.ProductJobs.Find(job => job.id == id) != null);
        }
    }
}
