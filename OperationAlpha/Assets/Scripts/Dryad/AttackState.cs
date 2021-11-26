using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class AttackState : DryadState
    {
        public override DryadState Tick(DryadManager dryadManager, DryadStats dryadStats, DryadAnimatorManager dryadAnimatorManager)
        {
            // Select random attack
            // If selected attack is not able to be used because of angle or distance, change attack
            // If attack is doable, stop our movement and attack
            // Set recovery time to default
            // return to combat stance

            return this;
        }
    }
}