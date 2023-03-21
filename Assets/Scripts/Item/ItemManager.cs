using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private int type;

    public enum ItemType
    {
        weaponItem,  recovery
    }
    [SerializeField] public ItemType itemType;
    private UIIndicator uiIndicator;

    private void Start()
    {
        var ui = GameObject.FindWithTag("UI");
        uiIndicator = ui.GetComponent<UIIndicator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (itemType == ItemType.weaponItem)
        {
            
            if (other.gameObject.tag == "Player")
            {
                var shooter = other.GetComponent<PlayerShooter>();
                shooter.playerWeapon = (PlayerShooter.weapon) Enum.ToObject(typeof(PlayerShooter.weapon), type);
                shooter.level++;
                uiIndicator.SetWeaponValue((UIIndicator.WeaponType)Enum.ToObject(typeof(UIIndicator.WeaponType), type), shooter.level);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                var playerHP = other.GetComponent<PlayerController>().hp += 10;
                if (playerHP >= 100)
                {
                    other.GetComponent<PlayerController>().hp = 100;
                }
                uiIndicator.SetHP(playerHP);
                Destroy(gameObject);
                
            }
        }
    }
}
