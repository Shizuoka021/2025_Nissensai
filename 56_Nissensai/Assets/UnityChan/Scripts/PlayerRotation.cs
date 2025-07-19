using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private float rotationSpeed = 0.1f; // 回転速度（スムーズな回転のため）

    // プレイヤーを移動方向に回転させるメソッド
    public void RotatePlayer(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero) // 移動している場合のみ回転
        {
            // 現在の回転(transform.rotation)を移動方向に向ける回転(Quaternion.LookRotation)に近づける
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        }
    }
}
