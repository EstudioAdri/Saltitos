using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : ThinkingSpawnable
{
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Activate(Faction sFaction, SpawnableData sData)
    {
        sType = sData.sType;
        faction = sFaction;
        hitPoints = sData.hitPoints;
        targetType = sData.targetType;        
        dieAudioClip = sData.dieClip;
        //TODO: add more as necessary

        
    }

    protected override void Die()
    {
        base.Die();

        FindObjectOfType<GameManager>().GameOver();
    }
}
