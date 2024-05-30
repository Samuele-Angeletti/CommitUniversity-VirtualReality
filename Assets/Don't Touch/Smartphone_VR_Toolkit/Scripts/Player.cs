using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour, ISubscriber
{
    [SerializeField] List<AudioClip> steps;
    [SerializeField] float timeSteps;
    [SerializeField] GameObject powerEffect;
    [SerializeField] bool useGyro = true;
    AudioSource audioSource;
    NavMeshAgent agent;
    GyroscopeController gyroscopeController;
    CameraController cameraController;
    float timePassed;
    int objectCollected = 0;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        agent.updateRotation = false;
        timePassed = timeSteps / 2;

        Publisher.Subscribe(this, typeof(CollectedObjectMessage));
    }
    private void Start()
    {
        if (useGyro)
        {
            gyroscopeController = transform.GetChild(2).GetComponentInChildren<GyroscopeController>();
            cameraController = gyroscopeController.GetComponent<CameraController>();
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Move(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    private void Update()
    {
        if (!agent.hasPath) return;
        PlayStep();

    }

    private void PlayStep()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= timeSteps)
        {
            timePassed = 0;
            audioSource.PlayOneShot(steps[UnityEngine.Random.Range(0, steps.Count)]);
        }
    }

    internal void ActiveParticlePower()
    {
        powerEffect.gameObject.SetActive(true);
    }

    public void End()
    {
        cameraController.End();
    }

    public void OnPublish(IPublisherMessage message)
    {
        if (message is CollectedObjectMessage)
        {
            objectCollected++;
        }
    }

    public void OnDisableSubscribe()
    {
        Publisher.Unsubscribe(this, typeof(CollectedObjectMessage));
    }

    private void OnDisable()
    {
        OnDisableSubscribe();
    }

    public bool CollectionMatch(int collectionRequest) => collectionRequest <= objectCollected;
}
