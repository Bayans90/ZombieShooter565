using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour
{
    public int weaponType; //1 melee, 2 gun
    public bool isAuto_gun;
    public int attackPoints;

    [SerializeField]
    private BoxCollider meleeCollider;



    private void Awake()
    {
        meleeCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the tag "Zombie"
        if (other.gameObject.CompareTag("Zombie") && weaponType == 1)
        {
            // Apply damage or other logic here
            Debug.Log("Hit a zombie!");

            ZombieController zombie = other.gameObject.GetComponent<ZombieController>();

            zombie.takeDamage(attackPoints);

            Debug.Log("Zombie health is:" + zombie.healthPoints);
            DeactivateCollider();
        }
    }

    public void ActivateCollider()
    {
        // Set the collider as active
        meleeCollider.enabled = true;
    }

    public void DeactivateCollider()
    {
        // Set the collider as inactive
        meleeCollider.enabled = false;
    }


}
