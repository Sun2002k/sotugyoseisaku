//弾の制御

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletMove : MonoBehaviour
{
    #region 変数
    [SerializeField] float speed;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartUp();
    }

    void FixedUpdate()
    {
        Bullet();
    }


    #region 関数定義
    /// <summary>
    /// 初期設定
    /// </summary>
    void StartUp()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 v_player;
        Vector3 v_bullet;

        //座標をセット
        v_bullet = this.transform.position;   //敵の座標
        v_player = player.transform.position; //プレイヤーの座標

        //プレイヤーの方へ向く処理
        float angle = 90; // 敵が回転する角度
        float theta = Mathf.Atan2(v_player.x - v_bullet.x, v_player.z - v_bullet.z);//プレイヤーの座標と敵の座標をxとzに分けてそれぞれ計算
        float deg = -(angle - theta * Mathf.Rad2Deg);
        transform.rotation = Quaternion.Euler(new Vector3(0, deg, 0));//プレイヤーの方へ向く
    }

    /// <summary>
    /// 弾が動く処理
    /// </summary>
    void Bullet()
    {
        Vector3 velocity = gameObject.transform.rotation * new Vector3(-speed, 0, 0);
        gameObject.transform.position -= velocity * Time.deltaTime;
        
        float count = 0; count++;
        if(count > 100)//countを越えたら自壊する
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
