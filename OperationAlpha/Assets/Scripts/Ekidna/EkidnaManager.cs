using UnityEngine;
using UnityEngine.AI;

namespace SG
{
    public class EkidnaManager : MonoBehaviour
    {
        EkidnaLocomotionManager ekidnaLocomotionManager;
        EkidnaAnimatorManager ekidnaAnimatorManager;
        EkidnaStats ekidnaStats;

        public EkidnaState currentState;
        public CharacterStats currentTarget;
        public NavMeshAgent navMeshAgent;
        public Rigidbody ekidnaRigidBody;

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
            ekidnaLocomotionManager = GetComponent<EkidnaLocomotionManager>();
            ekidnaAnimatorManager = GetComponent<EkidnaAnimatorManager>();
            ekidnaStats = GetComponent<EkidnaStats>();
            ekidnaRigidBody = GetComponent<Rigidbody>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            navMeshAgent.enabled = false;
        }

        private void Start()
        {
            ekidnaRigidBody.isKinematic = false;
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
                EkidnaState nextState = currentState.Tick(this, ekidnaStats, ekidnaAnimatorManager);
                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(EkidnaState ekidnaState)
        {
            currentState = ekidnaState;
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
