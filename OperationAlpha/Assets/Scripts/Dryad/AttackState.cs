using UnityEngine;

namespace SG
{
    public class AttackState : DryadState
    {
        public CombatStanceState combatStanceState;

        public DryadAttackAction[] enemyAttacks;
        public DryadAttackAction currentAttack;

        public override DryadState Tick(DryadManager dryadManager, DryadStats dryadStats, DryadAnimatorManager dryadAnimatorManager)
        {
            Vector3 targetDirection = dryadManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            if (dryadManager.isPerformingAction)
            {
                return combatStanceState;
            }

            if (currentAttack != null)
            {
                // If too close to do current attack, do another one
                if (dryadManager.distanceFromTarget < currentAttack.minimumDistanceNeededToAttack)
                {
                    return this;
                }
                // If we are close enough, attack
                else if (dryadManager.distanceFromTarget < currentAttack.maximumDistanceNeededToAttack)
                {
                    // If player is in attack viewable angle, attack
                    if (dryadManager.viewableAngle <= currentAttack.maximumAttackAngle &&
                        dryadManager.viewableAngle >= currentAttack.minimumAttackAngle)
                    {
                        if (dryadManager.currentRecoveryTime <= 0 && dryadManager.isPerformingAction == false)
                        {
                            dryadAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                            dryadAnimatorManager.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                            dryadAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);

                            dryadManager.isPerformingAction = true;
                            dryadManager.currentRecoveryTime = currentAttack.recoveryTime;

                            currentAttack = null;
                            return combatStanceState;
                        }
                    }
                }   
            } 
            else
            {
                GetNewAttack(dryadManager);
            }

            return combatStanceState;
        }

        private void GetNewAttack(DryadManager dryadManager)
        {
            Vector3 targetsDirection = dryadManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);
            dryadManager.distanceFromTarget = Vector3.Distance(dryadManager.currentTarget.transform.position, transform.position);

            int maxScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                DryadAttackAction enemyAttackAction = enemyAttacks[i];

                if (dryadManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    && dryadManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
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
                if (dryadManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    && dryadManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
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
    }
}