using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchBossRoom1 : MonoBehaviour
{
    // This value is found by going in Unity -> File -> Build Settings 
    // -> Scenes In Build in top section -> number next to it
    int Corridor1SceneNumber = 4;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(4);
        }
        //SceneManager.LoadScene(Corridor1SceneNumber);
    }
}
