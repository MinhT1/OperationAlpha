using UnityEngine;
namespace SG
{
    public class DryadLocomotionManager : MonoBehaviour
    {
        DryadManager dryadManager;
        DryadAnimatorManager dryadAnimatorManager;

        public LayerMask detectionLayer;

        private void Awake()
        {
            dryadManager = GetComponent<DryadManager>();
            dryadAnimatorManager = GetComponent<DryadAnimatorManager>();
        }
    }
}
