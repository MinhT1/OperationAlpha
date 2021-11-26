using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CombatStanceState : DryadState
    {
        public override DryadState Tick(DryadManager dryadManager, DryadStats dryadStats, DryadAnimatorManager dryadAnimatorManager)
        {
            // Check for attack range
            // Circle around player
            // If ready to attack change to attack state
            // If player out of range or in cool down, return this state or go in pursue state
            return this;
        }
    }
}