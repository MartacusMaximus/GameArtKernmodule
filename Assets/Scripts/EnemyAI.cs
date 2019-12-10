using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyAI : MonoBehaviour
{

    public float fpsTargetDistance;
    public float enemyLookDistance;
    public float attackDistance;
    public float enemyMovementSpeed;
    public Transform fpsTarget;
    public Transform healthBar;
    public Image barImage;
    private float barFill;

    Rigidbody theRigidbody;

    public float startingHealth;
    private float currentHealth;
    public AudioClip deathClip;

    Animator anim;
    AudioSource enemyAudio;
    bool isDead;


    // Use this for initialization
    void Start()
    {
        theRigidbody = GetComponent<Rigidbody>();
        currentHealth = startingHealth;
        barFill = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HealthBarPos();

        barImage.fillAmount = barFill;


        fpsTargetDistance = Vector3.Distance(fpsTarget.position, transform.position);
        if (fpsTargetDistance < enemyLookDistance)
        {
            lookAtPlayer();
        }
        if (fpsTargetDistance < attackDistance)
        {
            attackAggro();
            lookAtPlayer();
        }
        if (enemyLookDistance < fpsTargetDistance)
        {
        }

    }

    void lookAtPlayer()
    {
        transform.LookAt(fpsTarget);
    }
    void attackAggro()
    {
        theRigidbody.AddForce(transform.forward * enemyMovementSpeed);
    }
    private void OnCollisionStay(Collision collision)
    {
        GameObject enemy = collision.gameObject; //Vind de speler en call het health script
        if (enemy.tag == "Player")
        {
            PlayerHealth enemyhealth = enemy.GetComponent<PlayerHealth>();
            if (enemyhealth != null)
            {
                enemyhealth.TakeDamage(8);
            }
        }

    }
    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            return;
        }

        else
        {
            currentHealth -= amount;
            barFill = currentHealth / startingHealth;
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }
    private void HealthBarPos()
    {
        Vector3 currentPos = transform.position;

        healthBar.position = new Vector3(currentPos.x, currentPos.y + 1.1f, currentPos.z);

        healthBar.LookAt(Camera.main.transform);
    }
    void Death()
    {
        isDead = true;
        print("I am Dead");
        Destroy(gameObject, 0.5f);
    }

}
