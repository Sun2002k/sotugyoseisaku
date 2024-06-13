//敵の基本動作

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemymove : MonoBehaviour
{
    Vector3 v_enemy; //敵のベクター
    Vector3 v_player; //プレイヤーのベクター
    Vector3 v_range; //敵とプレイヤーの距離
    Vector3 v_move;

    GameObject player;
    [SerializeField] float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
    }

    /// <summary>
    /// プレイヤーを追いかける
    /// </summary>
    public void EnemyMove()
    {
        //プレイヤーの元へと行く
        v_enemy = this.transform.position;   //敵の座標
        v_player = player.transform.position; //プレイヤーの座標
        v_range = v_player - v_enemy; //プレイヤーの座標と敵の座標相対ベクター
        v_move = v_range * speed; //相対ベクターにスピードを掛ける
        this.transform.position += v_move;

        //プレイヤーの方へ向く処理
        float theta = Mathf.Atan2(v_player.x - v_enemy.x, v_player.z - v_enemy.z);//プレイヤーの座標と敵の座標をxとzに分けてそれぞれ計算
        float deg = -(270 - theta*Mathf.Rad2Deg);
        transform.rotation = Quaternion.Euler(new Vector3(0,deg,0));//プレイヤーの方へ向く
    }
}
