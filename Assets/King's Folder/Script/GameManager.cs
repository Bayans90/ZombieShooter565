using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player, zombie;
    public Camera camera;
    public LayerMask playerLayer;


    bool leftClick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftClick = Input.GetKey(KeyCode.F);
        //hitZombie(leftClick);
    }

    public void hitZombie(bool leftClick)
    {
        // Check for mouse input
        if (leftClick)
        {
            int attackPoints = player.GetComponentInChildren<PlayerController>().GetAttackPoints();
            // Cast a ray from the mouse position
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit,Mathf.Infinity, ~playerLayer))
            {
                // Check if the object hit has the tag "Zombie"
                if (hit.transform.tag.Equals("Zombie"))
                {
                    Debug.Log("Zombie hit detected.");
                    // Access the Zombie component on the hit object
                    ZombieController zombie = hit.transform.GetComponent<ZombieController>();

                    if (zombie != null)
                    {
                        Debug.Log("Zombie component found.");
                        zombie.takeDamage(attackPoints);
                        Debug.Log("Damage: " + attackPoints);
                    }
                    else
                    {
                        Debug.Log("Zombie component not found on the hit object");
                    }
                }
                else
                {
                    Debug.Log("Hit object is not tagged as Zombie.");
                }
            }
            else
            {
                Debug.Log("No hit detected.");
            }
        }
    }


}
