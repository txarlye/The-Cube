using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public GameObject hoverSound;
    public GameObject Collapse;
    public GameObject shoot;
    public GameObject floorCollapse;
    public GameObject deathEnemy;
    public GameObject getDamage;
    public GameObject loseOneLive;
    public GameObject winOneLive;
    public GameObject levelUp;

    
    public void AudioWinOneLive()
    {
        winOneLive.GetComponent<AudioSource>().Play();
    }
    public void AudioLoseOneLive()
    {
        loseOneLive.GetComponent<AudioSource>().Play();
    }
    public void AudioLevelUp()
    {
        levelUp.GetComponent<AudioSource>().Play();
    }
    public void AudioCollapse()
    {
        Collapse.GetComponent<AudioSource>().Play();
    }

    public void AudioHover()
    {
        hoverSound.GetComponent<AudioSource>().Play();
    }

    public void AudioShoot()
    {
        shoot.GetComponent<AudioSource>().Play();
    }

    public void AudioFloorCollapse()
    {
        floorCollapse.GetComponent<AudioSource>().Play();
    }
    public void AudioDeathEnemy()
    {
        deathEnemy.GetComponent<AudioSource>().Play();
    }
    public void AudioGetDamage()
    {
        getDamage.GetComponent<AudioSource>().Play();
    }
}
