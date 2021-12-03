using UnityEngine;

namespace SG
{
    public class PursueTargetState : DryadState
    {
        public CombatStanceState combatStanceState;

        public DryadAttackAction walkMovement;
        public override DryadState Tick(DryadManager dryadManager, DryadStats dryadStats, DryadAnimatorManager dryadAnimatorManager)
        {
            if (dryadManager.isPerformingAction)
            {
                return this;
            }
            Vector3 targetDirection = dryadManager.currentTarget.transform.position - transform.position;
            dryadManager.distanceFromTarget = Vector3.Distance(dryadManager.currentTarget.transform.position, transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            // Impacts the when target is too far away
            if (dryadManager.distanceFromTarget > dryadManager.maximumAttackRange)
            {
                dryadAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);

                //transform.LookAt(targetDirection);
            }

            HandleRotateTowardsTarget(dryadManager);
            dryadManager.navMeshAgent.transform.localPosition = transform.position;
            dryadManager.navMeshAgent.transform.localRotation = Quaternion.identity;

            if (dryadManager.distanceFromTarget <= dryadManager.maximumAttackRange)
            {
                return combatStanceState;
            }
            else
            {
                return this;
            }
        }

        private void HandleRotateTowardsTarget(DryadManager dryadManager)
        {
            // Rotate Manually
            if (dryadManager.isPerformingAction)
            {
                Vector3 direction = dryadManager.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                dryadManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, dryadManager.rotationSpeed / Time.deltaTime);
            }
            // Rotate with Pathfinding from NavMeshAgent
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(dryadManager.navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = dryadManager.dryadRigidBody.velocity;
                
                dryadManager.navMeshAgent.enabled = true;
                dryadManager.navMeshAgent.SetDestination(dryadManager.currentTarget.transform.position);
                dryadManager.dryadRigidBody.velocity = targetVelocity;
                dryadManager.transform.rotation = Quaternion.Slerp(transform.rotation, dryadManager.navMeshAgent.transform.rotation, dryadManager.rotationSpeed / Time.deltaTime);
            }
        }
    }
}
