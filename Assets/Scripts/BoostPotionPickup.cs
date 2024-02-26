using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPotionPickup : Pickup
{
    public float speedMultiplier = 2f;
    public float duration = 5f;

    public override void OnPickup(TopDownCharacterController player)
    {
        SoundManager.Play("Pickup");
        player.BoostSpeed(speedMultiplier, duration);
        Destroy();
    }
}
