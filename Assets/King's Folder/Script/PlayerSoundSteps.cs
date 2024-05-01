using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundSteps : MonoBehaviour
{

    public AudioSource source;
    public AudioClip step1Clip, step2Clip, step3Clip, step4Clip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RandomStepAudio()
    {
        /// Create a System.Random object
        System.Random random = new System.Random();

        // Generate a random number between 1 and 4
        int randomStep = random.Next(1, 5); // Next(min, max) generates numbers in the range [min, max)

        if(randomStep == 1)
        {
            source.PlayOneShot(step1Clip);
        }

        if (randomStep == 2)
        {
            source.PlayOneShot(step2Clip);
        }

        if (randomStep == 3)
        {
            source.PlayOneShot(step3Clip);
        }

        if (randomStep == 4)
        {
            source.PlayOneShot(step4Clip);
        }
    }
}
