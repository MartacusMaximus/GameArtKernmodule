using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool activated;
    public AudioSource whack;
    public float rotationSpeed;
    public int swordDamage;


    void Update()
    {
        if (activated)
        {
            transform.localEulerAngles += Vector3.forward * Time.deltaTime;
            transform.localEulerAngles += Vector3.down * rotationSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 9)
        {
            whack.enabled = true;
            collision.gameObject.GetComponentInParent<HingeJoint>().breakForce = 100;
        }
        if (collision.gameObject.layer == 12)
        {
            whack.enabled = true;
            EnemyAI enemyhealth = collision.gameObject.GetComponent<EnemyAI>();
            if (enemyhealth != null)
            {
                enemyhealth.TakeDamage(swordDamage);
            }
        }
        else 
        {
            whack.enabled = true;
            GetComponent<Rigidbody>().Sleep();
            GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            GetComponent<Rigidbody>().isKinematic = true;
            activated = false;
        }
    }   
}