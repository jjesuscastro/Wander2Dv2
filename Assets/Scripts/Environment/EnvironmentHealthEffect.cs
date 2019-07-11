using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class EnvironmentHealthEffect : MonoBehaviour
{
    [Header("Delay of the effect in milliseconds")]
    public int delay;
    public float effect;
    float timer = float.MaxValue;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            timer += Time.deltaTime * 100;
            if (timer >= delay)
            {
                if(PlayerMentalHealth.instance.health >= 1 && effect > 0)
                {
                    // do nothing;
                } else {
                    PlayerMentalHealth.instance.changeHealth(effect);
                }
                timer = 0;
            }
        }
    }
}
