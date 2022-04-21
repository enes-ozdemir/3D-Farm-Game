using UnityEngine;

namespace Farm.Vehicle
{
    public class HarvestingVehicleController : CarController
    {
        [SerializeField] private bool reelEnabled = false;
        [SerializeField] private float reelRotationSpeed = 10f;
        [SerializeField] private Transform reelTransform;

        private float timeCounter = 0f;
        private float timeToDisable = 1f;
        private bool reelDisableAnimation = false;

        protected override void OnFixedUpdate()
        {
        }

        protected override void OnUpdate()
        {
            HandleInput();
            HandleReelRotate();
        }

        protected override void OnStart()
        {
        }

        private void HandleReelRotate()
        {
            if (reelEnabled)
            {
                reelTransform.Rotate(90*Time.deltaTime*reelRotationSpeed,0,0);
            }
            else
            {
                if (reelDisableAnimation)
                {
                    timeCounter -= Time.deltaTime;
                    if (timeCounter <= 0)
                    {
                        timeCounter = 0f;
                        reelDisableAnimation = false;
                    }
                    reelTransform.Rotate(90*Time.deltaTime*reelRotationSpeed*(timeCounter/timeToDisable), 0, 0);
                }
            }
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                reelEnabled = !reelEnabled;
                if (!reelEnabled)
                {
                    timeCounter = timeToDisable;
                    reelDisableAnimation = true;
                }
                else
                {
                    timeCounter = timeToDisable;
                    reelDisableAnimation = false;
                }
            }
        }
    }
}