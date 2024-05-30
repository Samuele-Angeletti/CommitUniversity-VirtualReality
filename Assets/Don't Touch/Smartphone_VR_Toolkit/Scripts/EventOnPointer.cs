using Assets.Scripts.VrToolkit;
using UnityEngine;
using UnityEngine.Events;

public class EventOnPointer : MonoBehaviour, IRaycastReceiver
{
    [SerializeField] UnityEvent onEnter;
    [SerializeField] UnityEvent onExit;
    [SerializeField] bool oneTime;
    [SerializeField] bool activeParticlePlayer;
    bool entered;
    bool exited;
    Player player;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    public void OnPointerEnter()
    {
        if (oneTime && entered)
            return;

        entered = true;

        onEnter?.Invoke();
        if (activeParticlePlayer)
            player.ActiveParticlePower();
    }

    public void OnPointerExit()
    {
        if (oneTime && exited)
            return;

        exited = true;

        onExit?.Invoke();
    }
}
