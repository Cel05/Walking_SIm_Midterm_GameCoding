using UnityEngine;

public class SimpleNPCMover : MonoBehaviour
{
    [Header("Movement")]
    public bool moveBackAndForth = true;
    public float moveDistance = 1.5f;
    public float moveSpeed = 1.2f;

    [Header("Rotation")]
    public bool facePlayer = true;
    public Transform player;
    public float turnSpeed = 4f;

    [Header("Idle Motion")]
    public bool bobUpAndDown = true;
    public float bobAmount = 0.03f;
    public float bobSpeed = 2f;

    [Header("Start Direction")]
    public bool useLocalRight = false;

    private Vector3 startPos;
    private float bobTimer;

    void Start()
    {
        startPos = transform.position;

        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
        }
    }

    void Update()
    {
        HandleMove();
        HandleFacePlayer();
        HandleBob();
    }

    void HandleMove()
    {
        if (!moveBackAndForth) return;

        Vector3 moveDir = useLocalRight ? transform.right : transform.forward;
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        Vector3 targetPos = startPos + moveDir.normalized * offset;

        Vector3 currentPos = transform.position;
        currentPos.x = targetPos.x;
        currentPos.z = targetPos.z;
        transform.position = currentPos;
    }

    void HandleFacePlayer()
    {
        if (!facePlayer || player == null) return;

        Vector3 lookPos = player.position - transform.position;
        lookPos.y = 0f;

        if (lookPos.sqrMagnitude < 0.001f) return;

        Quaternion targetRot = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
    }

    void HandleBob()
    {
        if (!bobUpAndDown) return;

        bobTimer += Time.deltaTime * bobSpeed;

        Vector3 pos = transform.position;
        float yOffset = Mathf.Sin(bobTimer) * bobAmount;

        pos.y = startPos.y + yOffset;
        transform.position = pos;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Vector3 basePos = Application.isPlaying ? startPos : transform.position;
        Vector3 dir = useLocalRight ? transform.right : transform.forward;

        Vector3 a = basePos + dir.normalized * moveDistance;
        Vector3 b = basePos - dir.normalized * moveDistance;

        Gizmos.DrawLine(a, b);
        Gizmos.DrawSphere(a, 0.08f);
        Gizmos.DrawSphere(b, 0.08f);
    }
}