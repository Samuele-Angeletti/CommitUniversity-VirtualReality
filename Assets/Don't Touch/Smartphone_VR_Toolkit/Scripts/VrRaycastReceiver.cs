using Assets.Scripts.VrToolkit;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VrRaycastReceiver : MonoBehaviour, IRaycastReceiver
{
    [SerializeField] float timer;
    [SerializeField] bool oneTimeUse = false;
    [SerializeField] bool isDirectionButton;
    [SerializeField] bool isCollectable = false;
    [SerializeField] bool collectionTester = false;
    [SerializeField] int collectionRequest = 0;
    [SerializeField] bool selfClose = false;
    [SerializeField] UnityEvent onAction;
    [SerializeField] Image radialLoading;
    [SerializeField] Transform[] worldSpaceUI;
    [SerializeField] Transform destinationPivot;
    
    public delegate void OnAction();
    public OnAction onActionDelegate;
    bool _used;
    Coroutine _timerCoroutine;
    GyroscopeController _gyroscopeController;
    Player _player;
    Transform _lookAtTarget;
    AudioSource _audioSource;
    bool _usingGyro = true;
    private void Awake()
    {
        foreach (var ui in worldSpaceUI)
        {
            ui.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (destinationPivot != null)
            destinationPivot.SetParent(null);

        _audioSource = GetComponent<AudioSource>();
        _lookAtTarget = Camera.main.transform;
        _gyroscopeController = FindObjectOfType<GyroscopeController>();
        if (_gyroscopeController == null)
        {
            _usingGyro = false;
        }
    }

    private void Start()
    {
        if (!_usingGyro)
        {
            _player = FindObjectOfType<Player>();
        }
    }

    private void Update()
    {
        transform.LookAt(_lookAtTarget);
    }

    public void MoveGyroscope(Transform target)
    {
        if (_usingGyro)
            _gyroscopeController.MovePlayer(target);
        else
            _player.Move(target.position);
    }

    public void OnPointerEnter()
    {
        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);
        _timerCoroutine = StartCoroutine(TimerCoroutine());
    }

    public void OnPointerExit()
    {
        StopCoroutine(_timerCoroutine);
        radialLoading.fillAmount = 0;

        if (oneTimeUse && _used)
        {
            if (isDirectionButton && _used)
            {
                transform.parent.gameObject.SetActive(false);
            }
            gameObject.SetActive(false);

        }
        else if (selfClose && _used)
        {
            if (isDirectionButton)
            {
                transform.parent.gameObject.SetActive(false);
            }
            else
                gameObject.SetActive(false);
            _used = false;
        }
        else
            _used = false;

        
    }

    public void Action()
    {
        if (collectionTester)
        {
            if (!FindObjectOfType<Player>().CollectionMatch(collectionRequest))
            {
                return;
            }
        }

        _audioSource.Play();

        _used = true;
        onAction?.Invoke();
        onActionDelegate?.Invoke();

        if (isCollectable)
        {
            Publisher.Publish(new CollectedObjectMessage());
        }
    }


    public IEnumerator TimerCoroutine()
    {
        float timePassed = 0;
        while (true)
        {
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            radialLoading.fillAmount = timePassed / timer;
            if (timePassed > timer)
            {
                radialLoading.fillAmount = 0;
                break;
            }
        }

        Action();
    }

}
