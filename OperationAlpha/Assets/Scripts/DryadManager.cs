using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DryadManager : MonoBehaviour
    {
        DryadLocomotionManager dryadLocomotionManager;
        DryadAnimatorManager dryadAnimatorManager;
        public bool isPerformingAction;

        public DryadAttackAction[] enemyAttacks;
        public DryadAttackAction currentAttack;

        [Header("A.I Setting")]
        public float detectionRadius = 100;

        // Higher = bigger
        public float maximumAngleDetection = 130;
        // Lower = bigger
        public float minimumAngleDetection = -130;

        public float currentRecoveryTime = 0;

        private void Awake()
        {
            dryadLocomotionManager = GetComponent<DryadLocomotionManager>();
            dryadAnimatorManager = GetComponent<DryadAnimatorManager>();
        }
        private void Update()
        {
            HandleRecoveryTimer();
        }

        private void FixedUpdate()
        {
            HandleCurrentAction();
        }

        private void HandleCurrentAction()
        {
            if (dryadLocomotionManager.currentTarget != null)
            {
                dryadLocomotionManager.distanceFromTarget = 
                    Vector3.Distance(dryadLocomotionManager.currentTarget.transform.position, transform.position);
            }
            if (dryadLocomotionManager.currentTarget == null)
            {
                dryadLocomotionManager.HandleDetection();
            }
            else if (dryadLocomotionManager.distanceFromTarget > dryadLocomotionManager.stoppingDistance)
            {
                dryadLocomotionManager.HandleMoveToTarget();
            }
            else if (dryadLocomotionManager.distanceFromTarget <= dryadLocomotionManager.stoppingDistance)
            {
                AttackTarget();
            }
        }

        private void HandleRecoveryTimer()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if (isPerformingAction)
            {
                if (currentRecoveryTime <= 0)
                {
                    isPerformingAction = false;
                }
            }
        }

        #region Attacks

        private void AttackTarget()
        {
            if (isPerformingAction)
            {
                return;
            }

            if (currentAttack == null)
            {
                GetNewAttack();
            }
            else
            {
                isPerformingAction = true;
                currentRecoveryTime = currentAttack.recoveryTime;
                dryadAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
                currentAttack = null;
            }
        }

        private void GetNewAttack()
        {
            Vector3 targetsDirection = dryadLocomotionManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);
            dryadLocomotionManager.distanceFromTarget = Vector3.Distance(dryadLocomotionManager.currentTarget.transform.position, transform.position);

            int maxScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                DryadAttackAction enemyAttackAction = enemyAttacks[i];
                if (dryadLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    && dryadLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                        && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        maxScore += enemyAttackAction.attackScore;
                    }
                }
            }

            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                DryadAttackAction enemyAttackAction = enemyAttacks[i];
                if (dryadLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    && dryadLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                        && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        if (currentAttack != null)
                        {
                            return;
                        }
                        temporaryScore += enemyAttackAction.attackScore;

                        if (temporaryScore > randomValue)
                        {
                            currentAttack = enemyAttackAction;
                        }
                    }
                }
            }
        }
        #endregion

    }
}
