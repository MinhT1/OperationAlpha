using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchCorridor1 : MonoBehaviour
{
    // This value is found by going in Unity -> File -> Build Settings 
    // -> Scenes In Build in top section -> number next to it
    int Corridor1SceneNumber = 2;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(2);
        }
        //SceneManager.LoadScene(Corridor1SceneNumber);
    }
}
