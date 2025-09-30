using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 0;
    [SerializeField] float maxHealth = 100f;

    [SerializeField] ParticleSystem deathEffect;
    [SerializeField] Slider healthBAR;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    void Start()
    {
        health = maxHealth;
        healthBAR.maxValue = maxHealth;
        healthBAR.value = health;
        fill.color = gradient.Evaluate(1f);
        fill.color = gradient.Evaluate(healthBAR.normalizedValue);
    }

    public void Damage(float dmg)
    {
        health -= dmg;
        healthBAR.value = health;
        fill.color = gradient.Evaluate(healthBAR.normalizedValue);

        if (health <= 0)
        {
            if (deathEffect != null)
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject, .2f);
        }
    }
}
