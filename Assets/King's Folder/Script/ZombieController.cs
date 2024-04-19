using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public int healthPoints;
    public Animator animator;
    public CharacterController characterController;


    public int prevHealthPoints;

    private void Awake()
    {
        healthPoints = 100;
        prevHealthPoints = healthPoints;
        characterController.enabled = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healthPoints < prevHealthPoints)
        {
            animator.SetBool("tookDamage",true);
        }
        else if(healthPoints == prevHealthPoints)
        {
            animator.SetBool("tookDamage", false);
        }



        if(healthPoints <= 0)
        {
            animator.SetBool("isDead", true);
            characterController.enabled = false;
            
        }

        prevHealthPoints = healthPoints;
    }



    public void takeDamage(int damagePoint)
    {
        healthPoints -= damagePoint;
    }
}
