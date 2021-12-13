using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchCorridor2 : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            SceneManager.LoadScene(5);
        }
        //SceneManager.LoadScene(Corridor1SceneNumber);
    }
}
