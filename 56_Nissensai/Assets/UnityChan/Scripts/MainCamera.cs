using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform player; // Inspectorで指定できるようにする
    private Vector3 offset;                   // プレイヤーとの相対距離

    void Start()
    {
        if (player == null)
        {
            // Playerが未指定なら名前で探す（保険）
            GameObject obj = GameObject.Find("Player");
            if (obj != null)
                player = obj.transform;
        }

        if (player != null)
        {
            // 初期の相対距離を記録
            offset = transform.position - player.position;
        }
        else
        {
            Debug.LogError("Playerが見つかりません。Inspectorで設定してください。");
        }
    }

    void LateUpdate() // UpdateよりLateUpdateの方が追従カメラに向いている
    {
        if (player != null)
        {
            // プレイヤー位置にオフセットを足してカメラ位置を更新
            transform.position = player.position + offset;
        }
    }
}