using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;

    private Rigidbody rigidbody;
    private TrailRenderer trailRenderer;

    private Vector3 direction;
    private Vector3 rotateVector = new Vector3(360f, 0f, 0f);

    private KnifeData knifeData;

    private float currentDuration;

    private bool isReady;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void Update()
    {
        if (!isReady)
            return;

        currentDuration += Time.deltaTime;

        if (knifeData.duration < currentDuration)
            DestroyKnife();

        transform.Rotate(rotateVector * Time.deltaTime);

        rigidbody.velocity = direction * knifeData.speed;
    }

    public void InitializeAttack(Vector3 direction, KnifeData knifeData)
    {
        this.direction = direction;
        this.knifeData = knifeData;

        trailRenderer.Clear();

        transform.forward = this.direction;

        currentDuration = 0f;

        isReady = true;
    }

    private void DestroyKnife()
    {
        isReady = false;

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsLayerMatched(obstacleLayer.value, other.gameObject.layer))
        {
            DestroyKnife();
        }
        else if (IsLayerMatched(knifeData.target.value, other.gameObject.layer))
        {
            if (other.TryGetComponent(out IDamagable damagable))
                damagable.TakePhysicalDamage(knifeData.power);

            DestroyKnife();
        }
    }

    private bool IsLayerMatched(int value, int layer)
    {
        return value == (value | 1 << layer);
    }
}