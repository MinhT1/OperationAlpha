using UnityEngine;
namespace SG
{
    public class EkidnaLocomotionManager : MonoBehaviour
    {
        EkidnaManager ekidnaManager;
        EkidnaAnimatorManager ekidnaAnimatorManager;

        public LayerMask detectionLayer;

        private void Awake()
        {
            ekidnaManager = GetComponent<EkidnaManager>();
            ekidnaAnimatorManager = GetComponent<EkidnaAnimatorManager>();
        }
    }
}
