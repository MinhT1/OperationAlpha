using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace SG
{
    public class DryadLocomotionManager : MonoBehaviour
    {
        DryadManager dryadManager;
        DryadAnimatorManager dryadAnimatorManager;
        NavMeshAgent navMeshAgent;
        public Rigidbody dryadRigidBody;

        public CharacterStats currentTarget;
        public LayerMask detectionLayer;

        public float distanceFromTarget;
        public float stoppingDistance = 10;
        public float rotationSpeed = 40;

        private void Awake()
        {
            dryadManager = GetComponent<DryadManager>();
            dryadAnimatorManager = GetComponent<DryadAnimatorManager>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            dryadRigidBody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            navMeshAgent.enabled = false;
            dryadRigidBody.isKinematic = false;
        }

        public void HandleDetection()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, dryadManager.detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();
                if (characterStats != null)
                {
                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
                    currentTarget = characterStats;
                }   
            }
        }

        public void HandleMoveToTarget()
        {
            if (dryadManager.isPerformingAction)
            {
                return;
            }
            Vector3 targetDirection = currentTarget.transform.position - transform.position;
            distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            // If doing action, stop movement
            if (dryadManager.isPerformingAction)
            {
                dryadAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                navMeshAgent.enabled = false;
            } else
            {

                // Impacts the when target is too far away
                if (distanceFromTarget > stoppingDistance)
                {
                    dryadAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                    transform.LookAt(targetDirection);
                } 
                else if (distanceFromTarget <= stoppingDistance)
                {
                    dryadAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                }
            }

            HandleRotateTowardsTarget();
            // This is what is setting the initial position to 0
            //navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localPosition = transform.position;

            // This is what is causing the issue with the turning
            navMeshAgent.transform.localRotation = Quaternion.identity;
        }

        private void HandleRotateTowardsTarget()
        {
            // Rotate Manually
            if (dryadManager.isPerformingAction)
            {
                Vector3 direction = currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);
            } 
            // Rotate with Pathfinding from NavMeshAgent
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = dryadRigidBody.velocity;

                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(currentTarget.transform.position);
                dryadRigidBody.velocity = targetVelocity;
                transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
            }
        }
    }
}
