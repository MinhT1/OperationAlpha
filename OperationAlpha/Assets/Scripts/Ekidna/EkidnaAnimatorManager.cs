using UnityEngine;

namespace SG
{
    public class EkidnaAnimatorManager : AnimatorManager
    {
        EkidnaManager ekidnaManager;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            ekidnaManager = GetComponent<EkidnaManager>();
        }
        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            ekidnaManager.ekidnaRigidBody.drag = 0;

            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;

            Vector3 velocity = deltaPosition / delta;
            ekidnaManager.ekidnaRigidBody.velocity = velocity;
        }
    }
}