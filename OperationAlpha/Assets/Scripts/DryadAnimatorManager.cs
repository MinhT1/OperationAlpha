using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DryadAnimatorManager : AnimatorManager
    {
        DryadLocomotionManager dryadLocomotionManager;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            dryadLocomotionManager = GetComponent<DryadLocomotionManager>();
        }
        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            dryadLocomotionManager.dryadRigidBody.drag = 0;

            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;

            Vector3 velocity = deltaPosition / delta;
            dryadLocomotionManager.dryadRigidBody.velocity = velocity;
        }
    }
}