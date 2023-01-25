using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Ghost))]
public class GhostBehaviour : MonoBehaviour
{
    public Ghost ghost { get; private set; }
    public float duration;
    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
    }
    public void Enable()
    {
        Enable(duration);
    }
    public virtual void Enable(float duration)
    {
        this.enabled = true;
        // Reset the timer in case we re-enable before the end of the timer
        CancelInvoke();
        Invoke(nameof(Disable),duration);
    }
    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();
    }
}
