using UnityEngine;
using UnityEngine.AI;

namespace SG
{
    public class DryadManager : MonoBehaviour
    {
        DryadLocomotionManager dryadLocomotionManager;
        DryadAnimatorManager dryadAnimatorManager;
        DryadStats dryadStats;

        public DryadState currentState;
        public CharacterStats currentTarget;
        public NavMeshAgent navMeshAgent;
        public Rigidbody dryadRigidBody;

        public bool isPerformingAction;
        public float distanceFromTarget;
        public float rotationSpeed = 40;
        public float maximumAttackRange = 10;

        [Header("A.I Setting")]
        public float detectionRadius = 100;
        // Higher = bigger
        public float maximumAngleDetection = 130;
        // Lower = bigger
        public float minimumAngleDetection = -130;
        public float viewableAngle;

        public float currentRecoveryTime = 0;

        private void Awake()
        {
            dryadLocomotionManager = GetComponent<DryadLocomotionManager>();
            dryadAnimatorManager = GetComponent<DryadAnimatorManager>();
            dryadStats = GetComponent<DryadStats>();
            dryadRigidBody = GetComponent<Rigidbody>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            navMeshAgent.enabled = false;
        }

        private void Start()
        {
            dryadRigidBody.isKinematic = false;
        }

        private void Update()
        {
            HandleRecoveryTimer();
        }

        private void FixedUpdate()
        {
            HandleStateMachine();
        }

        private void HandleStateMachine()
        {
            if (currentState != null)
            {
                DryadState nextState = currentState.Tick(this, dryadStats, dryadAnimatorManager);
                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(DryadState dryadState)
        {
            currentState = dryadState;
        }

        private void HandleRecoveryTimer()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if (isPerformingAction)
            {
                if (currentRecoveryTime <= 0)
                {
                    isPerformingAction = false;
                }
            }
        }
    }
}
