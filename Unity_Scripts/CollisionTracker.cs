using UnityEngine;

public class CollisionTracker : MonoBehaviour
{
    public string wireTag = "Wire";
    public Collider mainHandleCollider;

    public int CollisionCount { get; private set; } = 0;

    private bool isCurrentlyTouching = false;

    public static CollisionTracker Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (mainHandleCollider == null) return;

        bool anyContact = false;
        GameObject[] wires = GameObject.FindGameObjectsWithTag(wireTag);

        foreach (GameObject wire in wires)
        {
            Collider wireCollider = wire.GetComponent<Collider>();
            if (wireCollider == null) continue;

            bool isOverlapping = Physics.ComputePenetration(
                mainHandleCollider, mainHandleCollider.transform.position, mainHandleCollider.transform.rotation,
                wireCollider, wireCollider.transform.position, wireCollider.transform.rotation,
                out _, out _
            );

            if (isOverlapping)
            {
                anyContact = true;
                break;
            }
        }

        if (anyContact && !isCurrentlyTouching)
        {
            CollisionCount++;
            Debug.Log("🔴 Tel teması algılandı: " + CollisionCount);
            isCurrentlyTouching = true;
        }
        else if (!anyContact && isCurrentlyTouching)
        {
            isCurrentlyTouching = false;
        }
    }

    public int GetCollisionCount()
    {
        return CollisionCount;
    }

    public void ResetCount()
    {
        CollisionCount = 0;
    }
}
