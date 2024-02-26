using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public int healAmount = 10;
    public override void OnPickup(TopDownCharacterController player)
    {
        var healthManager = HealthManager.Instance;
        if (!healthManager.IsFull())
        {
            SoundManager.Play("Pickup");
            healthManager.Heal(healAmount);
            Destroy();
        }
    }
}
