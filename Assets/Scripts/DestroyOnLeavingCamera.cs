using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLeavingCamera : MonoBehaviour
{
    private void Start()
    {
        var anim = GetComponent<Animator>();
        var delay = anim.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log(delay);
        Destroy(gameObject, delay);
    }
}
