using UnityEngine;

namespace SG
{
    [CreateAssetMenu(menuName = "A.I/Ekidna Actions/Attack Action")]
    public class EkidnaAttackAction : EkidnaActions
    {
        public int attackScore = 3;
        public float recoveryTime = 4;

        public float maximumAttackAngle = 90;
        public float minimumAttackAngle = -90;

        public float minimumDistanceNeededToAttack = 0;
        public float maximumDistanceNeededToAttack = 7;
    }
}