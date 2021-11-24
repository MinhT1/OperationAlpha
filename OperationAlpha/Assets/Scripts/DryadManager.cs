using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG {
    public class DryadManager : MonoBehaviour
    {
        DryadLocomotionManager dryadLocomotionManager;
        public bool isPerformingAction;

        [Header("A.I Setting")]
        public float detectionRadius = 40;

        // Higher = bigger
        public float maximumAngleDetection = 50;
        // Lower = bigger
        public float minimumAngleDetection = -50;
        private void Awake()
        {
            dryadLocomotionManager = GetComponent<DryadLocomotionManager>();
        }
        private void Update()
        {
            
        }

        private void FixedUpdate()
        {
            HandleCurrentAction();
        }

        private void HandleCurrentAction()
        {

            if (dryadLocomotionManager.currentTarget == null)
            {
                dryadLocomotionManager.HandleDetection();
            } else
            {
                dryadLocomotionManager.HandleMoveToTarget();
            }
        }
    }
}
