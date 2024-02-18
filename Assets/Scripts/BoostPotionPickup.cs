using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPotionPickup : Pickup
{
    public float speedMultiplier = 2f;
    public float duration = 5f;
    public GameObject pickupFX;

    public override void OnPickup(TopDownCharacterController player)
    {
        player.BoostSpeed(speedMultiplier, duration);
        VFXManager.SpawnEffect(pickupFX, player.gameObject.transform.position);
        Destroy();
    }
}
