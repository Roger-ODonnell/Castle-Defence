using UnityEngine;

public class bullet : MonoBehaviour
{

    public float damage = 10f;
    [SerializeField] ParticleSystem hitEffect;

    void OnCollisionEnter(Collision collision)
    {
        GetComponent<MeshRenderer>().enabled = false;
        if (hitEffect != null)
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        Health targetHealth = collision.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.Damage(damage);
        }
    }


    void Update()
    {
        Destroy(this.gameObject, 4);
    }
}
