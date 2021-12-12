using UnityEngine;

namespace SG
{
    public class EkidnaCombatStanceState : EkidnaState
    {
        public EkidnaAttackState attackState;
        public EkidnaPursueTargetState pursueTargetState;
        public override EkidnaState Tick(EkidnaManager ekidnaManager, EkidnaStats ekidnaStats, EkidnaAnimatorManager ekidnaAnimatorManager)
        {
            ekidnaManager.distanceFromTarget = Vector3.Distance(ekidnaManager.currentTarget.transform.position, ekidnaManager.transform.position);
            // Circle around player


            // Check for attack range
            if (ekidnaManager.currentRecoveryTime <= 0 && ekidnaManager.distanceFromTarget <= ekidnaManager.maximumAttackRange)
            {
                return attackState;
            }
            else if (ekidnaManager.distanceFromTarget > ekidnaManager.maximumAttackRange)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }
        }
    }
}