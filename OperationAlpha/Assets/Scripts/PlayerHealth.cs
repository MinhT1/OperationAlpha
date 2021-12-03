using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
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
}
