using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{
    public override void Interact()
    {
        var anim = GetComponentInChildren<Animator>();
        anim.Play("GateAnimation");
        SoundManager.Play("GateOpen");
    }
}
