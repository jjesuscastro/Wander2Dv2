using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class EnvironmentHealthEffect : MonoBehaviour
{
    public float effect;
    
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerMentalHealth.instance.changeHealth(effect);
        }
    }
}
