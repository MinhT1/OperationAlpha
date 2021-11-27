using UnityEngine;

namespace SG
{
    [CreateAssetMenu(menuName = "A.I/Enemy Actions/Attack Action")]
    public class DryadAttackAction : DryadActions
    {
        public int attackScore = 3;
        public float recoveryTime = 4;

        public float maximumAttackAngle = 90;
        public float minimumAttackAngle = -90;

        public float minimumDistanceNeededToAttack = 0;
        public float maximumDistanceNeededToAttack = 7;
    }
}