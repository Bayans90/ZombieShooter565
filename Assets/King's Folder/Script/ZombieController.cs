using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public int healthPoints;
    public Animator animator;
    public CharacterController characterController;


    //public BoxCollider handCollider;

    public GameObject zombieHand;


    NavMeshAgent nm;
    public Transform target;

    public int prevHealthPoints;
    public bool isDead;


    public AudioSource source;
    public AudioSource attackSource;
    public AudioClip zombieGrowl1, zombieGrowl2, zombieGrowl3;
    public AudioClip zombieAttack1, zombieAttack2, zombieAttack3;

    private void Awake()
    {
        healthPoints = 100;
        prevHealthPoints = healthPoints;
        characterController.enabled = true;
        isDead = false;
        
        nm = GetComponent<NavMeshAgent>();
    }


    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        
        if (!isDead)
        {
            if (!source.isPlaying)
            {
                PlayRandomGrowl();
            }
            if (distance <= 100f)
            {
                nm.destination = target.position;

                if(distance <= 1.1f)
                {
                    source.Stop();
                    animator.SetTrigger("Attack");
                }
            }
        }
        else
        {
            nm.isStopped = true;
        }

        if(nm.velocity.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }


        if (healthPoints < prevHealthPoints)
        {
            animator.SetTrigger("tookDamage");
        }
        prevHealthPoints = healthPoints;



        if (healthPoints <= 0)
        {
            animator.SetBool("isDead", true);
            characterController.enabled = false;
            isDead = true;
            
        }
    }



    public void takeDamage(int damagePoints)
    {
        healthPoints -= damagePoints;
    }


    public void enableHandCollider()
    {
        zombieHand.GetComponentInChildren<ZombieHand>().ActivateCollider();
    }
    public void disableHandCollider()
    {
        zombieHand.GetComponentInChildren<ZombieHand>().DeactivateCollider();
    }


    private void PlayRandomGrowl()
    {
        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                source.PlayOneShot(zombieGrowl1);
                break;
            case 1:
                source.PlayOneShot(zombieGrowl2);
                break;
            case 2:
                source.PlayOneShot(zombieGrowl3);
                break;
        }
    }


    private void PlayRandomAttack()
    {
        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                attackSource.PlayOneShot(zombieAttack1);
                break;
            case 1:
                attackSource.PlayOneShot(zombieAttack2);
                break;
            case 2:
                attackSource.PlayOneShot(zombieAttack3);
                break;
        }
    }
}
