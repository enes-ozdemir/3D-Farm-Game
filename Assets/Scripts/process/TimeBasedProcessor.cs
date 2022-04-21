using Farm.Profile;
using System.Collections.Generic;
using UnityEngine;

namespace Farm.Processor
{
    public abstract class TimeBasedProcessor :BaseProcessor
    {
        protected List<float> timers = new List<float>();

        protected List<int> stages = new List<int>();

        protected int finishedCount = 0;

        public void Update()
        {
            //if status true and 
            if (Status && finishedCount != Profile.ProductJobs.Count)
            {
                if (Profile != null && Profile.ProductJobs.Count > 0)
                {
                    //loop every product job
                    for (int i = 0; i < Profile.ProductJobs.Count; i++)
                    {
                        ProductJob job = Profile.ProductJobs[i];
                        
                        //if this job new, add stage for stage tracking
                        if (stages.Count <= i)
                        {
                            stages.Add(0);
                        }

                        int Stage = stages[i];

                        if (!CanProcess(job, i)) continue;

                        //if current stage is the last stage, then continue
                        //finished job
                        if (Stage == job.stages.Count)
                        {
                            continue;
                        }

                        //if this job new, add stage for time tracking
                        if (timers.Count <= i)
                        {
                            timers.Add(Time.deltaTime);
                        }
                        else
                        {
                            timers[i] += Time.deltaTime;

                            //if current stage is time up, update state
                            if (job.stages[Stage].Time <= timers[i])
                            {
                                UpdateState(stages[i], i);
                                stages[i]++;
                            }

                            //if currrent product job is completed, then process
                            if (timers[i] >= job.Time)
                            {
                                timers[i] = 0f;
                                finishedCount++;

                                PreProcess(job, i);
                                Process(job, i);
                                AfterProcess(job, i);
                            }
                        }
                    }
                }
            }
        }


        public abstract void UpdateState(int stage, int jobIndex);

        public abstract void Collect(string jobId, string productId);
    }
}