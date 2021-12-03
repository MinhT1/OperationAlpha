using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Image healthBar;
    //public Text healthPercentage;

    public float health, maxHealth = 300;
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
        HealthBarFiller();
        ColorChanger();
        if (health <= -6)
        {
            Destroy(gameObject);
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
        //if (Input.GetKeyDown(KeyCode.L) && health > 0)
        //{
        health -= 2;
        //}
    }
}