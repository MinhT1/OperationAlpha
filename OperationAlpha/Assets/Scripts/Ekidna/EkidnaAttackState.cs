using UnityEngine;

namespace SG
{
    public class EkidnaAttackState : EkidnaState
    {
        public EkidnaCombatStanceState combatStanceState;

        public EkidnaAttackAction[] enemyAttacks;
        public EkidnaAttackAction currentAttack;

        public override EkidnaState Tick(EkidnaManager ekidnaManager, EkidnaStats ekidnaStats, EkidnaAnimatorManager ekidnaAnimatorManager)
        {
            Vector3 targetDirection = ekidnaManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            if (ekidnaManager.isPerformingAction)
            {
                return combatStanceState;
            }

            if (currentAttack != null)
            {
                // If too close to do current attack, do another one
                if (ekidnaManager.distanceFromTarget < currentAttack.minimumDistanceNeededToAttack)
                {
                    return this;
                }
                // If we are close enough, attack
                else if (ekidnaManager.distanceFromTarget < currentAttack.maximumDistanceNeededToAttack)
                {
                    // If player is in attack viewable angle, attack
                    if (ekidnaManager.viewableAngle <= currentAttack.maximumAttackAngle &&
                        ekidnaManager.viewableAngle >= currentAttack.minimumAttackAngle)
                    {
                        if (ekidnaManager.currentRecoveryTime <= 0 && ekidnaManager.isPerformingAction == false)
                        {
                            ekidnaAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                            ekidnaAnimatorManager.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                            ekidnaAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);

                            ekidnaManager.isPerformingAction = true;
                            ekidnaManager.currentRecoveryTime = currentAttack.recoveryTime;

                            currentAttack = null;
                            return combatStanceState;
                        }
                    }
                }   
            } 
            else
            {
                GetNewAttack(ekidnaManager);
            }

            return combatStanceState;
        }

        private void GetNewAttack(EkidnaManager ekidnaManager)
        {
            Vector3 targetsDirection = ekidnaManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);
            ekidnaManager.distanceFromTarget = Vector3.Distance(ekidnaManager.currentTarget.transform.position, transform.position);

            int maxScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EkidnaAttackAction enemyAttackAction = enemyAttacks[i];

                if (ekidnaManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    && ekidnaManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
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
                EkidnaAttackAction enemyAttackAction = enemyAttacks[i];
                if (ekidnaManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                    && ekidnaManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
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