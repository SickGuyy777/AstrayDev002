using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite MedPack;
    public Sprite Money;
    public Sprite RifleAmmo;
    public Sprite PistolAmmo;
    public Sprite SniperAmmo;
    public Sprite ShotgunAmmo;
    public Sprite Pistol;
    public Sprite Rifle;
    public Sprite Sniper;
    public Sprite Shotgun;

}
