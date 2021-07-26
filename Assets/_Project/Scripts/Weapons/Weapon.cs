using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for any type of gun
public class Weapon : Pickupable
{
    protected int MagazineSize;
    protected int Magazine;
    protected bool reloading = false;

    // Automatically shoots bullets when the firebutton is pressed down
    public void AutoShoot(KeyCode FireButton, float TimeBetweenShots, GameObject projectile, float BulletForce, Transform FirePoint)
    {
        if(Input.GetKey(FireButton))
        {
            Fire(projectile, BulletForce, FirePoint.transform);
            Invoke("InvokePause", TimeBetweenShots); //Invokes a pause to keep from 100's of shots being fired at once
        }
    }

    // Allows for the burst fire of a weapon
    public void BurstShoot(KeyCode FireButton, float TimeBetweenBursts, float TimeBetweenBurstShots, float AmountOfBurstShots, GameObject projectile, float BulletForce, Transform FirePoint)
    {
        if (Input.GetKey(FireButton))
        {
            for (int i = 0; i < AmountOfBurstShots; ++i)
            {
                Fire(projectile, BulletForce, FirePoint.transform);

                Invoke("InvokePause", TimeBetweenBursts); //Invokes a pause to keep all of the bursts from being fired at once
            }


            Invoke("InvokePause", TimeBetweenBurstShots); //Invokes a pause to keep from 100's of shots being fired at once
        }
    }

    // Makes FireButton be clicked for every shot
    public void SemiAutoShoot(KeyCode FireButton, float TimeBetweenShots, GameObject projectile, float BulletForce, Transform FirePoint)
    {
        if(Input.GetKeyDown(FireButton))
        {
            Fire(projectile, BulletForce, FirePoint.transform);
            Invoke("InvokePause", TimeBetweenShots); //Invokes a pause to keep from 100's of shots being fired at once
        }
    }

    // Allows for the burst fire of a weapon
    public void BurstShoot(KeyCode FireButton, float TimeBetweenShots, float ConeSize, int AmountOfShotsInSpray, GameObject projectile, float BulletForce, Transform FirePoint)
    {
        if (Input.GetKey(FireButton))
        {
            float DegreesBetweenBullet = ConeSize / AmountOfShotsInSpray;

            Quaternion InitialFirepointRotation = FirePoint.transform.rotation; // Saving the initial Rotation of the firepoint

            for (int i = 1; i < AmountOfShotsInSpray + 1; ++i)
            {
                FirePoint.transform.rotation = Quaternion.Euler(ConeSize / 2, 0, DegreesBetweenBullet * i);

                Fire(projectile, BulletForce, FirePoint.transform);

                Invoke("InvokePause", TimeBetweenShots); //Invokes a pause to keep all of the bursts from being fired at once
            }
            FirePoint.transform.rotation = InitialFirepointRotation; // Centers the firepoint so the next shots arent angled down

            Invoke("InvokePause", TimeBetweenShots); //Invokes a pause to keep the for loop to run 100's of times
        }
    }

    // Actually shoots the projectile
    private void Fire(GameObject projectile, float bulletForce, Transform firePoint)
    {
        GameObject Bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        --Magazine;
    }

    // Reloads the gun
    public void reload(KeyCode ReloadButton, float ReloadTime)
    {
        if(Input.GetKeyDown(ReloadButton))
        {
            reloading = false;
            Invoke("finishReload", ReloadTime); //Allows for a quick pause before the reload finishes.
        }
    }

    private void finishReload()
    {
        Magazine = MagazineSize;
    }

    // Just an empty method so you can invoke a pause
    private void InvokePause()
    {
        
    }
}
