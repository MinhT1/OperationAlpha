using UnityEngine;

namespace SG
{
    public class EkidnaIdleState : EkidnaState
    {
        public EkidnaPursueTargetState pursueTargetState;
        public LayerMask detectionLayer;

        public override EkidnaState Tick(EkidnaManager ekidnaManager, EkidnaStats ekidnaStats, EkidnaAnimatorManager ekidnaAnimatorManager)
        {
            // Look for potential target
            // Switch to pursue target state
            // If no target return this state

            #region Handle Enemy Target Detection
            Collider[] colliders = Physics.OverlapSphere(transform.position, ekidnaManager.detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

                if (characterStats != null)
                {
                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                    if (viewableAngle > ekidnaManager.minimumAngleDetection && viewableAngle < ekidnaManager.maximumAngleDetection)
                    {
                        ekidnaManager.currentTarget = characterStats;
                    }
                }
            }
            #endregion

            #region Handle Switching To Next State
            if (ekidnaManager.currentTarget != null)
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
