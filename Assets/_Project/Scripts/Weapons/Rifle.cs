using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for any type of Rifle
public class Rifle : Weapon
{
    // Declaring Variables
    #region Variables
    protected float BulletForce;

    protected int Size_of_Magazine;

    protected float ReloadTime;

    protected float Time_Between_Shots;

    protected GameObject projectile;
    protected GameObject FirePoint;

    protected KeyCode FireButton = KeyCode.Mouse0;   
    protected KeyCode ReloadButton = KeyCode.R;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Size_of_Magazine = MagazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        // Checking to see if the character is currently reloading
        if(reloading == true)
        {
            // Calls reload to check if the player is trying to reload, if 
            reload(ReloadButton, ReloadTime);
        }

        if (MagazineSize <= 0 || !reloading)
        {
            
        }
        else
        {
            AutoShoot(FireButton, Time_Between_Shots, projectile, BulletForce, FirePoint.transform);
        }
    }
}
