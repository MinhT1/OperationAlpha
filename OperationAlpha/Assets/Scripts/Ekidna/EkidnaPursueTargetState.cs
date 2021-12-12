using UnityEngine;

namespace SG
{
    public class EkidnaPursueTargetState : EkidnaState
    {
        public EkidnaCombatStanceState combatStanceState;

        public EkidnaAttackAction walkMovement;
        public override EkidnaState Tick(EkidnaManager ekidnaManager, EkidnaStats ekidnaStats, EkidnaAnimatorManager ekidnaAnimatorManager)
        {
            if (ekidnaManager.isPerformingAction)
            {
                return this;
            }
            Vector3 targetDirection = ekidnaManager.currentTarget.transform.position - transform.position;
            ekidnaManager.distanceFromTarget = Vector3.Distance(ekidnaManager.currentTarget.transform.position, transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            // Impacts the when target is too far away
            if (ekidnaManager.distanceFromTarget > ekidnaManager.maximumAttackRange)
            {
                ekidnaAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);

                //transform.LookAt(targetDirection);
            }

            HandleRotateTowardsTarget(ekidnaManager);
            ekidnaManager.navMeshAgent.transform.localPosition = transform.position;
            ekidnaManager.navMeshAgent.transform.localRotation = Quaternion.identity;

            if (ekidnaManager.distanceFromTarget <= ekidnaManager.maximumAttackRange)
            {
                return combatStanceState;
            }
            else
            {
                return this;
            }
        }

        private void HandleRotateTowardsTarget(EkidnaManager ekidnaManager)
        {
            // Rotate Manually
            if (ekidnaManager.isPerformingAction)
            {
                Vector3 direction = ekidnaManager.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                ekidnaManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, ekidnaManager.rotationSpeed / Time.deltaTime);
            }
            // Rotate with Pathfinding from NavMeshAgent
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(ekidnaManager.navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = ekidnaManager.ekidnaRigidBody.velocity;
                
                ekidnaManager.navMeshAgent.enabled = true;
                ekidnaManager.navMeshAgent.SetDestination(ekidnaManager.currentTarget.transform.position);
                ekidnaManager.ekidnaRigidBody.velocity = targetVelocity;
                ekidnaManager.transform.rotation = Quaternion.Slerp(transform.rotation, ekidnaManager.navMeshAgent.transform.rotation, ekidnaManager.rotationSpeed / Time.deltaTime);
            }
        }
    }
}
