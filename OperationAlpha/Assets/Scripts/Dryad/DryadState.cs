using UnityEngine;

namespace SG
{
    public abstract class DryadState : MonoBehaviour
    {
        public abstract DryadState Tick(DryadManager dryadManager, DryadStats dryadStats, DryadAnimatorManager dryadAnimatorManager);
    }
}
