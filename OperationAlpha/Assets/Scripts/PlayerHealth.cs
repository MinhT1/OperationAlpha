using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar; 
    public GameObject deathMenu;
    //public Text healthPercentage;

    public float health, maxHealth = 100;
    float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //healthPercentage.text = health + "%";

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        lerpSpeed = 3f * Time.deltaTime;
        //Damage();
        HealthBarFiller();
        ColorChanger();

        if(health <= 0)
        {
            //Load death menu screen
            SceneManager.LoadScene(1);

            //If we use death screen panel, load death menu panel
            /*deathMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;*/
        }
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        healthBar.color = healthColor;
    }

    public void Damage()
    {
       // if (Input.GetKeyDown(KeyCode.B) && health > 0)
       // {
           health -= 10;
       // }
    }

    public void Kill()
    {
        // if (Input.GetKeyDown(KeyCode.B) && health > 0)
        // {
        health -= 100;
        // }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            Damage();
        }
        if (collision.gameObject.tag == "Death")
        {
            Kill();
        }
    }
}
