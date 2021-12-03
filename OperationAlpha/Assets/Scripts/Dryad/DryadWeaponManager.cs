using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryadWeaponManager : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.Damage();
            }
        }  
    }
}
