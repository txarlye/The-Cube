using System;
using _Scripts;
using UnityEngine;


public class DamageOnTriggerEnter : MonoBehaviour
{
    public bool destroySelf;
    public bool destroyOther;
    public LayerMask targetLayer;
     

    /*
    mask:                    0000 0000 0000 0000 0000 0000 1100 0000 
    layer convertido a mask: 0000 0000 0000 0000 0000 0000 0100 0000 
    resultado                0000 0000 1000 0000 0000 0000 1100 0000 
    */

    public void Awake()
    {
        string name = this.gameObject.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetLayer == (1 << other.gameObject.layer | targetLayer))
        {
            if (destroyOther)
            {
                Destroy(other.gameObject);
            }
            // Evitamos que nuestro player se meta en la pool
            if (destroySelf & name !="Cube [Player](Clone)" /* & name !="moneda(Clone)" */) {
                PoolManager.instance.Despawn(gameObject);
                UI_Manager.instance.WinPoints(GameManager.instance.getPointsToKillEnemy());
                SoundManager.instance.AudioDeathEnemy();
            }
            
            if (destroySelf & name =="Cube [Player](Clone)") {
                GameManager.instance.receiveDamage();
                
            }
            
            if (name =="moneda(Clone)") {
                UI_Manager.instance.WinPoints(GameManager.instance.getPointsToKillEnemy()); 
            } 
             if (name =="heart(Clone)") {
                UI_Manager.instance.addOneHeart();
                SoundManager.instance.AudioWinOneLive();
            }
            
        }
    }
}
