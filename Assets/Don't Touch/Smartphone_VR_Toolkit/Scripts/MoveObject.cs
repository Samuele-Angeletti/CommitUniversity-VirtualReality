using UnityEngine;
using UnityEngine.Events;

public class MoveObject : MonoBehaviour
{
    [SerializeField] Transform destination;
    [SerializeField] float speed;
    [SerializeField] bool destroyOnStop = true;
    [SerializeField] UnityEvent onStop;
    [SerializeField] bool oneTime = true;
    bool done;
    // Update is called once per frame
    void Update()
    {
        if (oneTime && done)
            return;

        transform.position += speed * Time.deltaTime * transform.forward;

        if (Vector3.Distance(transform.position, destination.position) < 1)
        {
            if (destroyOnStop)
                Destroy(gameObject);

            done = true;

            onStop?.Invoke();
        }
    }
}
