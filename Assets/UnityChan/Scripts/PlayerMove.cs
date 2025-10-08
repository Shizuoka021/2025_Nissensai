using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveScale = 5f; // Inspectorで調整可能
    private Vector3 moveDirection;
    private Rigidbody rb;
    private PlayerRotation playerRotation;
    private bool canMove = true; // 移動可能フラグ
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRotation = GetComponent<PlayerRotation>();
        animator = GetComponent<Animator>(); // Animator取得
    }

    void Update()
    {
        if (!canMove)
        {
            animator.SetBool("isMoving", false); // 停止状態を送信
            return;
        }

        // 🎮 ゲームパッド入力
        float lsh = Input.GetAxis("L_Stick_H");
        float lsv = Input.GetAxis("L_Stick_V");

        // ⌨ キーボード入力 (WASD)
        float h = Input.GetAxis("Horizontal"); // A(-1) D(+1)
        float v = Input.GetAxis("Vertical");   // S(-1) W(+1)

        // どちらか大きい入力を採用（ゲームパッド＋WASD両対応）
        float finalH = Mathf.Abs(lsh) > Mathf.Abs(h) ? lsh : h;
        float finalV = Mathf.Abs(lsv) > Mathf.Abs(v) ? lsv : v;

        moveDirection = new Vector3(finalH, 0, finalV);

        bool moving = moveDirection.magnitude > 0.1f;
        animator.SetBool("isMoving", moving); // 移動状態を送信


        if (moving)
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
}