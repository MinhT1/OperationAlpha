using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlayerCollision : MonoBehaviour
{
    public CapsuleCollider playerCollider;
    public CapsuleCollider playerBlockerCollider;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(playerCollider, playerBlockerCollider, true);
    }
}
