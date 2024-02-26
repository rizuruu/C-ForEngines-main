using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected KeyCode interactKey = KeyCode.F;

    protected SpriteRenderer spriteRenderer;

    private const string HighlightProperty = "_Highlighted";
    private int highlightPropertyID;

    private bool isPlayerInRange = false;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        highlightPropertyID = Shader.PropertyToID(HighlightProperty);
    }

    protected virtual void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(interactKey))
            Interact();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            isPlayerInRange = true;
            Highlight();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            isPlayerInRange = false;
            Unhighlight();
        }
    }

    public virtual void Highlight()
    {
        spriteRenderer.material.SetInt(highlightPropertyID, 1);
    }

    public virtual void Unhighlight()
    {
        spriteRenderer.material.SetInt(highlightPropertyID, 0);
    }

    public abstract void Interact();

    private bool IsPlayer(Collider2D other) => other.CompareTag("Player"); 
}
