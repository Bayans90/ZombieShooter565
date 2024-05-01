using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSound : MonoBehaviour
{
    public AudioSource source;

    public AudioClip zombiestepClip1, zombiestepClip2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void zombieStep()
    {
        /// Create a System.Random object
        System.Random random = new System.Random();

        // Generate a random number between 1 and 4
        int randomStep = random.Next(1, 3); // Next(min, max) generates numbers in the range [min, max)


        if(randomStep == 1)
        {
            source.PlayOneShot(zombiestepClip1);
        }
        if(randomStep == 2)
        {
            source.PlayOneShot(zombiestepClip2);
        }
    }
}
