using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PursueTargetState : DryadState
    {
        public override DryadState Tick(DryadManager dryadManager, DryadStats dryadStats, DryadAnimatorManager dryadAnimatorManager)
        {
            // Chase target
            // If within attack range, switch to combat stance
            // If return out of range, return this state and continue chasing
            return this;
        }
    }
}
