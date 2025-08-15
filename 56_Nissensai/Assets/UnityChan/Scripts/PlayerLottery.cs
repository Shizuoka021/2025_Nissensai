using System.Collections;
using UnityEngine;

public class PlayerLottery : MonoBehaviour
{
    private bool isNearLottery = false;
    private int lotteryCount = 0;             // くじ引き回数
    private bool ignoreCollision = false;     // 衝突無効フラグ
    private bool isLotteryRunning = false;    // くじ引き中フラグ

    private PlayerMove playerMove;
    private Animator animator;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        animator = GetComponent<Animator>(); // Animator取得
    }

    void Update()
    {
        // Lottery接触中かつBボタンでくじ引き、かつくじ引き中ではない
        if (isNearLottery && !isLotteryRunning && Input.GetButtonDown("B_Button") && lotteryCount < 2)
        {
            StartCoroutine(LotteryRoutine());
        }
    }

    private IEnumerator LotteryRoutine()
    {
        isLotteryRunning = true;
        animator.SetBool("isLotteryRunning", true); // ★ アニメータに送信
        lotteryCount++;
        Debug.Log($"くじを引きます！ 残り回数: {2 - lotteryCount}");

        playerMove.SetCanMove(false);

        // くじ演出 3 秒
        yield return new WaitForSeconds(3f);

        playerMove.SetCanMove(true);

        // Lottery 衝突無効 1 秒
        ignoreCollision = true;
        yield return new WaitForSeconds(1f);
        ignoreCollision = false;

        isLotteryRunning = false;
        animator.SetBool("isLotteryRunning", false); // ★ アニメータに送信
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lottery"))
        {
            if (ignoreCollision) return;

            isNearLottery = true;
            Debug.Log("Lotteryに接触 → 停止");
            playerMove.SetCanMove(false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lottery"))
        {
            isNearLottery = false;
            Debug.Log("Lotteryから離れた");

            if (!ignoreCollision)
            {
                playerMove.SetCanMove(true);
            }
        }
    }
}