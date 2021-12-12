using UnityEngine;

namespace SG
{
    public abstract class EkidnaState : MonoBehaviour
    {
        public abstract EkidnaState Tick(EkidnaManager ekidnaManager, EkidnaStats ekidnaStats, EkidnaAnimatorManager ekidnaAnimatorManager);
    }
}
