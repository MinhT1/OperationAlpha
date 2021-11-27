using UnityEngine;

namespace SG
{
    public class DryadAnimatorManager : AnimatorManager
    {
        DryadManager dryadManager;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            dryadManager = GetComponent<DryadManager>();
        }
        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            dryadManager.dryadRigidBody.drag = 0;

            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;

            Vector3 velocity = deltaPosition / delta;
            dryadManager.dryadRigidBody.velocity = velocity;
        }
    }
}