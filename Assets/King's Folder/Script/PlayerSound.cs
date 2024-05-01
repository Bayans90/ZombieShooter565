using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public PlayerController player;
    public AudioSource source;

    public AudioClip pistolClip;
    public AudioClip revolverClip;
    public AudioClip shotgunClip;
    public AudioClip twobarrelClip;
    public AudioClip m4Clip;
    public AudioClip mac10Clip;




    public AudioClip meleeSwingClip;


    bool forwardPressed;
    bool leftPressed;
    bool rightPressed;
    bool backwardPressed;
    bool runPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        forwardPressed = Input.GetKey("w");
        leftPressed = Input.GetKey("a");
        rightPressed = Input.GetKey("d");
        backwardPressed = Input.GetKey("s");
        runPressed = Input.GetKey("left shift");


    }

    void playGunClip()
    {
        if (player.currentGun == 0 || player.currentGun == 5)
        {
            source.PlayOneShot(pistolClip);
        }
        if(player.currentGun == 1)
        {
            source.PlayOneShot(revolverClip);
        }
        if (player.currentGun == 2)
        {
            source.PlayOneShot(shotgunClip);
        }
        if (player.currentGun == 3)
        {
            source.PlayOneShot(twobarrelClip);
        }
        if(player.currentGun == 4)
        {
            source.PlayOneShot(m4Clip);
        }

        
    }

    void playSwingClip()
    {
        source.PlayOneShot(meleeSwingClip);
    }
}
