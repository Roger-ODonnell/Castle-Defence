using System;
using Unity.Mathematics;
using UnityEngine;

public class Sentry : MonoBehaviour
{
    [SerializeField] Transform turretHead;
    [SerializeField] AudioSource turretShot;

    [SerializeField] Transform[] firePonts;
    [SerializeField] GameObject bullet;

    public GameObject target;
    [SerializeField] bool targetLocked = false;

    [SerializeField] float bulletSpeed = 30f;
    [SerializeField] float baseShootDelay = 5f;
    [SerializeField] float damage = 20f;
    public float shootDelay;

    [SerializeField] AudioManager audioManager;

    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        shootDelay = baseShootDelay;
    }


    void Update()
    {
        if (targetLocked)
        {
            if (target == null)
            {
                LockOff();
                shootDelay = baseShootDelay;
                return;
            }

            Vector3 relativePos = target.transform.position - turretHead.transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);
            float rotationSpeed = 360f; // degrees per second, adjust as needed
            turretHead.transform.rotation = Quaternion.RotateTowards(
                turretHead.transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            if (shootDelay <= 0)
            {
                Shoot();
            }

            shootDelay -= Time.deltaTime;
        }

    }

    private void Shoot()
    {

        GameObject activeBullet = Instantiate(bullet, firePonts[0].position, Quaternion.identity);
        activeBullet.GetComponent<bullet>().damage = damage;
        Rigidbody bulletRb = activeBullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            int rand = UnityEngine.Random.Range(0, 2);
            firePonts[rand].GetComponentInChildren<ParticleSystem>().Play();
            bulletRb.AddForce(firePonts[rand].forward * bulletSpeed, ForceMode.Impulse);
        }

        AudioManager.Instance.PlaySFX("Cannon Shot");

        shootDelay = baseShootDelay;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "target" && target == null)
        {
            LockOn(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "target" && target == null)
        {
            LockOn(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "target")
        {
            LockOff();
        }
    }

    private void LockOff()
    {

        target = null;
        targetLocked = false;
    }

    private void LockOn(GameObject tar)
    {
        target = tar;
        targetLocked = true;
    }
}
