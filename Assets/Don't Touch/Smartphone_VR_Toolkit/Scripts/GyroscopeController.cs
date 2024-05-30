using UnityEngine;

public class GyroscopeController : MonoBehaviour
{
    [SerializeField] GameObject containerPrefab;
    [SerializeField] float lerpSpeed = 1;
    private bool gyroEnabled;
    private Gyroscope gyro;
    private GameObject cameraContainer;
    private Quaternion rotation;
    Player _player;
    Vector3 _lastGyroAttitude;
    float _minDistance = 0.15f;
    public bool GyroAvailable { get; private set; }

    private void Awake()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        cameraContainer.transform.rotation = Quaternion.Euler(90, 90, 0);
        rotation = new Quaternion(0, 0, 1, 0);

        _player = Instantiate(containerPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity).GetComponent<Player>();

        transform.SetParent(cameraContainer.transform);
        cameraContainer.transform.SetParent(_player.transform);
        gyroEnabled = EnableGyroscope();

    }

    private bool EnableGyroscope()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            GyroAvailable = SystemInfo.supportsGyroscope;
            _lastGyroAttitude = gyro.attitude.eulerAngles;
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            if (Vector3.Distance(_lastGyroAttitude, gyro.attitude.eulerAngles) < _minDistance)
                return;

            transform.localRotation = Quaternion.Lerp(transform.localRotation, gyro.attitude * rotation, Time.deltaTime * lerpSpeed);
            _lastGyroAttitude = gyro.attitude.eulerAngles;
        }
    }

    public void MovePlayer(Transform destination)
    {
        _player.Move(destination.position);
    }

    internal void CanMoveHead(bool active)
    {
        if (!GyroAvailable) return;

        if (gyroEnabled == active)
            return;

        gyroEnabled = active;
    }
}
