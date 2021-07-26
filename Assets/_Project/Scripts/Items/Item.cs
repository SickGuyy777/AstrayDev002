using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        MedPack,
        Money,
        RifleAmmo,
        PistolAmmo,
        SniperAmmo,
        ShotgunAmmo,
        Rifle,
        Pistol,
        Sniper,
        Shotgun,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case ItemType.MedPack:
                return ItemAssets.Instance.MedPack;
            case ItemType.Money:
                return ItemAssets.Instance.Money;
            case ItemType.RifleAmmo:
                return ItemAssets.Instance.RifleAmmo;
            case ItemType.PistolAmmo:
                return ItemAssets.Instance.PistolAmmo;
            case ItemType.SniperAmmo:
                return ItemAssets.Instance.SniperAmmo;
            case ItemType.ShotgunAmmo:
                return ItemAssets.Instance.Shotgun;
            case ItemType.Rifle:
                return ItemAssets.Instance.Shotgun;
            case ItemType.Pistol:
                return ItemAssets.Instance.Shotgun;
            case ItemType.Sniper:
                return ItemAssets.Instance.Shotgun;
            case ItemType.Shotgun:
                return ItemAssets.Instance.Shotgun;
        }
    }

    public bool IsStackable()
    {
        
        switch(itemType)
        {
            default:
            case ItemType.MedPack:
                return true;
            case ItemType.Money:
                return true;
            case ItemType.RifleAmmo:
                return true;
            case ItemType.PistolAmmo:
                return true;
            case ItemType.SniperAmmo:
                return true;
            case ItemType.ShotgunAmmo:
                return true;
        }
    }
}
