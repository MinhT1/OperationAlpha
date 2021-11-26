using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class IdleState : DryadState
    {
        public PursueTargetState pursueTargetState;
        public LayerMask detectionLayer;

        public override DryadState Tick(DryadManager dryadManager, DryadStats dryadStats, DryadAnimatorManager dryadAnimatorManager)
        {
            // Look for potential target
            // Switch to pursue target state
            // If no target return this state

            #region Handle Enemy Target Detection
            Collider[] colliders = Physics.OverlapSphere(transform.position, dryadManager.detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

                if (characterStats != null)
                {
                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                    if (viewableAngle > dryadManager.minimumAngleDetection && viewableAngle < dryadManager.maximumAngleDetection)
                    {
                        dryadManager.currentTarget = characterStats;
                    }
                }
            }
            #endregion

            #region Handle Switching To Next State
            if (dryadManager.currentTarget != null)
            {
                return pursueTargetState;
            } 
            else
            {
                return this;
            }
            #endregion
        }

    }
}
