using UnityEngine;

namespace SG
{
    public class CombatStanceState : DryadState
    {
        public AttackState attackState;
        public PursueTargetState pursueTargetState;
        public override DryadState Tick(DryadManager dryadManager, DryadStats dryadStats, DryadAnimatorManager dryadAnimatorManager)
        {
            dryadManager.distanceFromTarget = Vector3.Distance(dryadManager.currentTarget.transform.position, dryadManager.transform.position);
            // Circle around player


            // Check for attack range
            if (dryadManager.currentRecoveryTime <= 0 && dryadManager.distanceFromTarget <= dryadManager.maximumAttackRange)
            {
                return attackState;
            }
            else if (dryadManager.distanceFromTarget > dryadManager.maximumAttackRange)
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