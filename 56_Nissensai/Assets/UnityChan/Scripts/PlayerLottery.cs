using System.Collections;
using UnityEngine;

public class PlayerLottery : MonoBehaviour
{
    private bool isNearLottery = false;
    private int lotteryCount = 0;             // くじ引き回数
    private bool ignoreCollision = false;     // 衝突無効フラグ（移動再開直後用）
    private bool isLotteryRunning = false;    // くじ引き中フラグ

    private PlayerMove playerMove;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    void Update()
    {
        // Lottery接触中かつBボタンでくじ引き
        // かつくじ引き中ではない
        if (isNearLottery && !isLotteryRunning && Input.GetButtonDown("B_Button") && lotteryCount < 2)
        {
            StartCoroutine(LotteryRoutine());
        }
    }

    private IEnumerator LotteryRoutine()
    {
        isLotteryRunning = true; // くじ引き開始
        lotteryCount++;
        Debug.Log($"くじを引きます！ 残り回数: {2 - lotteryCount}");

        // 移動停止（念のため）
        playerMove.SetCanMove(false);

        // くじ演出 3 秒
        yield return new WaitForSeconds(3f);

        // 移動再開
        playerMove.SetCanMove(true);

        // Lottery 衝突無効 1 秒
        ignoreCollision = true;
        yield return new WaitForSeconds(1f);
        ignoreCollision = false;

        isLotteryRunning = false; // くじ引き終了
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lottery"))
        {
            if (ignoreCollision) return; // 無効化中は無視

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