using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchCorridor1 : MonoBehaviour
{
    // This value is found by going in Unity -> File -> Build Settings 
    // -> Scenes In Build in top section -> number next to it
    int Corridor1SceneNumber = 3;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(3);
        }
        //SceneManager.LoadScene(Corridor1SceneNumber);
    }
}
