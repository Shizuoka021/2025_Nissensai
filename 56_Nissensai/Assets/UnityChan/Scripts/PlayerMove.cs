using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveScale = 5f; // Inspector�Œ����\
    private Vector3 moveDirection;
    private Rigidbody rb;
    private PlayerRotation playerRotation;
    private bool canMove = true; // �ړ��\�t���O

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRotation = GetComponent<PlayerRotation>();
    }

    void Update()
    {
        if (!canMove) return;

        float lsh = Input.GetAxis("L_Stick_H");
        float lsv = Input.GetAxis("L_Stick_V");
        moveDirection = new Vector3(lsh, 0, lsv);

        if (moveDirection.magnitude > 0.1f)
        {
            playerRotation?.RotatePlayer(moveDirection);
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        if (moveDirection.magnitude > 0.1f)
        {
            Vector3 moveVelocity = moveDirection.normalized * moveScale;
            rb.MovePosition(transform.position + moveVelocity * Time.fixedDeltaTime);
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
        if (!value) moveDirection = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SetCanMove(false);
            Debug.Log("Wall�ɐڐG �� ��~");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SetCanMove(true);
            Debug.Log("Wall���痣�ꂽ �� �ړ��\");
        }
    }
}