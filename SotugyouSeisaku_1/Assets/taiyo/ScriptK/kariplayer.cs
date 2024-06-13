using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Newtonsoft.Json.Linq;

public class kariplayer : MonoBehaviour
{
    //ジャンプの速度を設定
    private const float _velocity = 5.0f;
    public Rigidbody rb; // プレイヤーのリジットボディ
    //着地状態を管理
    private bool _isGrounded;
    public Vector3 pos;
    public float my; // 現在のy座標
    [SerializeField] MeshRenderer mr;
    [SerializeField] Camera cam; // カメラを取得
    static public float HP;  // プレイヤーの体力
    static public float guncolor; // 銃の色　1.赤 2.青 3.黄
    static public float gunbulletR, gunbulletB, gunbulletY; // それぞれの残弾数
    static public float gunstockR, gunstockB, gunstockY; // それぞれの銃弾のストック
    static public float guncoolR, guncoolB, guncoolY; // それぞれの発射間隔
    static public bool cooltimeR, cooltimeB, cooltimeY; // それぞれ撃てるかどうか
    [SerializeField] GameObject BulletR, BulletB, BulletY; // それぞれの銃弾
    static public float bulletspeed; // 弾速
    [SerializeField] GameObject bulletpos; // 弾丸が発射されるポイント
    //[SerializeField] float speed; // プレイヤーの移動速度
    [SerializeField] Image StockIcon1, StockIcon2, StockIcon3; // HPのストックを表すアイコン
    public float Life; // ストックが何個あるか
    [SerializeField] Animator animator;
    public Vector2 debug; // デバッグ用の変数
    [SerializeField] InputAction inputMover; // 移動のインプットシステム
    [SerializeField] InputAction inputCamera; // カメラ移動のインプットシステム
    [SerializeField] float speed; // プレイヤーの移動速度
    [SerializeField] GameObject centerObj; // カメラ回転の中心
    [SerializeField] float angle; // 回転の角度
    [SerializeField] float camkakudox; // カメラの角度を取得する
    [SerializeField] float camkakudoy; // カメラの角度を取得する
    [SerializeField] float camkakudoz; // カメラの角度を取得する
    [SerializeField] float rotateSpeed;
    // オブジェクトがアクティブになったときに呼ばれるイベント
    private void OnDisable()
    {
        inputMover.Disable();
        inputCamera.Disable();
    }

    // オブジェクトが非表示になったときに呼ばれるイベント
    private void OnEnable()
    {
        inputMover.Enable();
        inputCamera.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
        //最初は着地していない状態
        _isGrounded = false;
        mr = GetComponent<MeshRenderer>();
        animator = GetComponent<Animator>(); // アニメーション発動のために取得
        HP = 100;
        guncolor = 1; // 赤色スタート
        gunbulletR = 20; // 赤銃の弾薬数
        gunbulletB = 10; // 青銃の弾薬数
        gunbulletY = 40; // 黄銃の弾薬数
        gunstockR = 40; // 赤銃の弾薬のストック
        gunstockB = 20; // 青銃の弾薬のストック
        gunstockY = 80; // 黄銃の弾薬のストック
        guncoolR = 0.5f; // 赤銃のクールタイム
        guncoolB = 1.5f; // 青銃のクールタイム
        guncoolY = 0.3f; // 黄銃のクールタイム
        cooltimeR = true; // 赤銃が撃てるかどうか
        cooltimeB = true; // 青銃が撃てるかどうか
        cooltimeY = true; // 黄銃が撃てるかどうか
        bulletspeed = 400; // 弾に掛けるパワー
        // 1アイコンにつきHPが100あることを示す(総HPは300)
        StockIcon1.enabled = true; // HPが0になった時に消える(表記は0のまま)
        StockIcon2.enabled = true; // HPが100を下回った時に消える(表記は100に戻る)
        StockIcon3.enabled = true; // HPが200を下回った時に消える(表記は100に戻る)
        StockIcon1.color = new Color32(0, 255, 255, 255); // アイコンの色は最初は水色
        StockIcon2.color = new Color32(0, 255, 255, 255);
        StockIcon3.color = new Color32(0, 255, 255, 255);
        Life = 3; // ライフ(アイコン)の数を示す変数
        pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        my = transform.position.y;
        camkakudox = cam.transform.localEulerAngles.x;
        camkakudoy = cam.transform.localEulerAngles.y;
        camkakudoz = cam.transform.localEulerAngles.z;
        // 銃弾数が0以下になったりなど最大値や最低値を越えないようにする処理
        #region 最大値最低値管理
        if (camkakudox >= 100)
        {
            cam.transform.rotation = Quaternion.Euler(99f, camkakudoy, camkakudoz);
        }
        if (camkakudox <= -100)
        {
            cam.transform.rotation = Quaternion.Euler(-99f, camkakudoy, camkakudoz);
        }
        if (guncolor <= 0)
        {
            guncolor = 3;
        }
        if (guncolor >= 4)
        {
            guncolor = 1;
        }
        if (gunbulletR <= 0)
        {
            gunbulletR = 0;
        }
        if (gunbulletB <= 0)
        {
            gunbulletB = 0;
        }
        if (gunbulletY <= 0)
        {
            gunbulletY = 0;
        }
        if (gunbulletR >= 20)
        {
            gunbulletR = 20;
        }
        if (gunbulletB >= 10)
        {
            gunbulletB = 10;
        }
        if (gunbulletY >= 40)
        {
            gunbulletY = 40;
        }
        if (HP <= 0)
        {
            HP = 0;
        }
        if (HP >= 100)
        {
            HP = 100;
        }
        if (Life <= 0)
        {
            Life = 0;
        }
        if (Life >= 3)
        {
            Life = 3;
        }
        #endregion
        // 色ごとの弾速
        #region 弾速変更
        if (guncolor == 1)
        {
            bulletspeed = 400;
        }
        if (guncolor == 2)
        {
            bulletspeed = 300;
        }
        if (guncolor == 3){
            bulletspeed = 600;
        }
        #endregion
        // ライフ判定
        #region ライフ判定
        if (HP <= 0)
        {
            if (Life == 3)
            {
                Life = 2;
                StockIcon3.enabled = false;
                HP = 100;
            }
            else if(Life == 2)
            {
                Life = 1;
                StockIcon2.enabled = false;
                HP = 100;
            }
            else if (Life == 1)
            {
                Life = 0;
                StockIcon1.enabled = false;
                // StartCoroutine(Death());
                // ストックが尽きたら効果音を鳴らすなどでゲームオーバーシーンに移行する
            }
        }
        if (Life == 2)
        {
            StockIcon2.color = new Color32(255, 120, 0, 255);
            StockIcon1.color = new Color32(255, 120, 0, 255);
        }
        if (Life == 1)
        {
            StockIcon1.color = new Color32(255, 0, 0, 255);
        }
        #endregion
        // スティック移動
        #region スティック移動
        var moveVec = inputMover.ReadValue<Vector2>();
        float Xvalue=moveVec.x*speed*Time.deltaTime;
        float Yvalue=moveVec.y*speed*Time.deltaTime;
        // プレイヤーを移動
        if (moveVec != new Vector2(0, 0))
        {
            animator.SetBool("move", true);
        }
        else if (moveVec == new Vector2(0, 0))
        {
            animator.SetBool("move", false);
        }
        /*
        transform.position += new Vector3(
            moveVec.x * speed,
            0,
            moveVec.y * speed);
        */
        /*
        if(moveVec!=new Vector2(0, 0))
        {
            if (moveVec.y >= 0)
            {
                transform.Rotate(0, 0, 0);
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            else if (moveVec.y <= 0)
            {
                transform.position -= transform.forward * speed * Time.deltaTime;
            }
            if (moveVec.x >= 0)
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
            else if (moveVec.x <= 0)
            {
                transform.position -= transform.right * speed * Time.deltaTime;
            }
        }
        */
        //現在のX,Zベクトルに上の処理で取得した値を渡す。
        Vector3 movedir = new Vector3(Xvalue, 0, Yvalue);

        //現在地に上で取得をした値を足して移動する。
        transform.position += movedir;

        //進む方向に滑らかに向く。
        transform.forward = Vector3.Slerp(transform.forward, movedir, Time.deltaTime * rotateSpeed);

        #endregion
        #region カメラ移動
        var cameravec = inputCamera.ReadValue<Vector2>();
        debug = cameravec;
        if (cameravec != new Vector2 (0, 0))
        {
            if (cameravec.x >= 0)
            {
                cam.transform.RotateAround(centerObj.transform.position, Vector3.up, angle * Time.deltaTime);
            }
            else if (cameravec.x <= 0)
            {
                cam.transform.RotateAround(centerObj.transform.position, Vector3.down, angle * Time.deltaTime);
            }
            
            if (cameravec.y >= 0)
            {
                cam.transform.Rotate(-cameravec.y, 0, 0);
            }
            else if (cameravec.y <= 0)
            {
                cam.transform.Rotate(cameravec.y, 0, 0);
            }
            
            //this.transform.Rotate(0, cameravec.x, 0);
        }
        #endregion
        if (Input.GetKeyDown(KeyCode.H))
        {
            HP -= 10;
        }

        //着地しているかを判定
        if (_isGrounded == true)
        {
            //スペースキーが押されているかを判定
            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                //ジャンプの方向を上向きのベクトルに設定
                Vector3 jump_vector = Vector3.up;
                //ジャンプの速度を計算
                Vector3 jump_velocity = jump_vector * _velocity;

                //上向きの速度を設定
                rb.velocity = jump_velocity;
                //地面から離れるので着地状態を書き換え
                _isGrounded = false;
            }
        }
    }

    #region インプットシステムのイベント
    public void OnJump(InputAction.CallbackContext context) // 地面に接触している事を条件にジャンプする処理
    {

        //着地しているかを判定
        if (_isGrounded == true)
        {
            //ジャンプの方向を上向きのベクトルに設定
            Vector3 jump_vector = Vector3.up;
            //ジャンプの速度を計算
            Vector3 jump_velocity = jump_vector * _velocity;
            //上向きの速度を設定
            rb.velocity = jump_velocity;
            //地面から離れるので着地状態を書き換え
            _isGrounded = false;
        }
        Debug.Log("ジャンプ");
    }

    public void Onspin(InputAction.CallbackContext context) // 右スティックが左右どちらかを取得しプレイヤーを回転させる処理
    {
        float hori2 = Input.GetAxis("Horizontal");
        if (hori2 > 0)
        {
            transform.Rotate(0, 0.5f, 0);
        }
        else if (hori2 < 0)
        {
            transform.Rotate(0, -0.5f, 0);
        }
    }

    public void OnReroad(InputAction.CallbackContext context) // Xボタンを取得し弾を補充する
    {
        if (context.performed){
            // リロード機能
            #region リロード機能
            if (guncolor == 1) // 赤の銃なら
            {
                if (gunbulletR == 0) // 残弾が0発なら
                {
                    if (gunstockR >= 20) // ストックが20発以上あるならそのまま移す
                    {
                        gunbulletR = 20;
                        gunstockR = gunstockR - 20;
                    }
                    else // そうじゃないなら今ある分を移す
                    {
                        gunbulletR = gunstockR;
                        gunstockR = 0;
                    }
                }
                else // まだ弾が余ってるなら
                {
                    float sabun = 0;
                    sabun = 20 - gunbulletR; // 何発撃ったのか 例:10発撃ったならsabunは10
                    if (gunstockR >= 20)
                    {
                        gunbulletR = 20;
                        gunstockR = gunstockR - sabun; // 使用分を減らす
                    }
                    else
                    {
                        if (sabun > gunstockR) // 全て入れるには足りない場合
                        {
                            gunbulletR = gunbulletR + gunstockR;
                            gunstockR = 0;
                        }
                        else // sabunに対しストックが足りる場合
                        {
                            gunbulletR = 40; // 弾を補充
                            gunstockR = gunstockR - sabun; // 使用分を減らす
                        }

                    }
                }
            }
            else if (guncolor == 2) // 青の銃なら
            {
                if (gunbulletB == 0) // 残弾が0発なら
                {
                    if (gunstockB >= 10) // ストックが10発以上あるならそのまま移す
                    {
                        gunbulletB = 10;
                        gunstockB = gunstockB - 10;
                    }
                    else // そうじゃないなら今ある分を移す
                    {
                        gunbulletB = gunstockB;
                        gunstockB = 0;
                    }
                }
                else // まだ弾が余ってるなら
                {
                    float sabun = 0;
                    sabun = 10 - gunbulletB; // 何発撃ったのか 例:5発撃ったならsabunは5
                    if (gunstockB >= 10)
                    {
                        gunbulletB = 10;
                        gunstockB = gunstockB - sabun; // 使用分を減らす
                    }
                    else
                    {
                        if (sabun > gunstockB) // 全て入れるには足りない場合
                        {
                            gunbulletB = gunbulletB + gunstockB;
                            gunstockB = 0;
                        }
                        else // sabunに対しストックが足りる場合
                        {
                            gunbulletB = 10; // 弾を補充
                            gunstockB = gunstockB - sabun; // 使用分を減らす
                        }
                    }
                }
            }
            else if (guncolor == 3) // 黄色の銃なら
            {
                if (gunbulletY == 0) // 残弾が0発なら
                {
                    if (gunstockY >= 40) // ストックが40発以上あるならそのまま移す
                    {
                        gunbulletY = 40;
                        gunstockY = gunstockY - 40;
                    }
                    else // そうじゃないなら今ある分を移す
                    {
                        gunbulletY = gunstockY;
                        gunstockY = 0;
                    }
                }
                else // まだ弾が余ってるなら
                {
                    float sabun = 0;
                    sabun = 40 - gunbulletY; // 何発撃ったのか 例:10発撃ったならsabunは30
                    if (gunstockY >= 40)
                    {
                        gunbulletY = 40;
                        gunstockY = gunstockY - sabun; // 使用分を減らす
                    }
                    else
                    {
                        if (sabun > gunstockY) // 全て入れるには足りない場合
                        {
                            gunbulletY = gunbulletY + gunstockY;
                            gunstockY = 0;
                        }
                        else // sabunに対しストックが足りる場合
                        {
                            gunbulletY = 40; // 弾を補充
                            gunstockY = gunstockY - sabun; // 使用分を減らす
                        }

                    }
                }
            }
            #endregion
        }
    }

    public void OnChangeL(InputAction.CallbackContext context) // 左トリガーを取得し銃種選択を左に変える処理
    {
        if (context.performed)
        {
            guncolor--;
        }
    }

    public void OnChangeR(InputAction.CallbackContext context) // 上と同じく右トリガーを取得し銃種を右に変える処理
    {
        if (context.performed)
        {
            guncolor++;
        }
    }

    public void OnFire(InputAction.CallbackContext context) // R2ボタン(PSでいう)を取得し弾を撃つ処理
    {
        if (context.performed)
        {
            if (guncolor == 1 && cooltimeR == true && gunbulletR >= 1)
            {
                GameObject ball = Instantiate(BulletR, bulletpos.transform.position, Quaternion.identity);
                Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
                ballRigidbody.AddForce(transform.forward * bulletspeed);
                gunbulletR--;
                StartCoroutine(tamakankakuR());
            }
            else if (guncolor == 2 && cooltimeB == true && gunbulletB >= 1)
            {
                GameObject ball = Instantiate(BulletB, bulletpos.transform.position, Quaternion.identity);
                Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
                ballRigidbody.AddForce(transform.forward * bulletspeed);
                gunbulletB--;
                StartCoroutine(tamakankakuB());
            }
            else if (guncolor == 3 && cooltimeY == true && gunbulletY >= 1)
            {
                GameObject ball = Instantiate(BulletY, bulletpos.transform.position, Quaternion.identity);
                Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
                ballRigidbody.AddForce(transform.forward * bulletspeed);
                gunbulletY--;
                StartCoroutine(tamakankakuY());
            }
        }
        
    }
    #endregion

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            //着地を検出したので着地状態を書き換え
            _isGrounded = true;
        }
    }

    IEnumerator tamakankakuR()
    {
        cooltimeR = false;
        yield return new WaitForSeconds(guncoolR);
        cooltimeR = true;
    }

    IEnumerator tamakankakuB()
    {
        cooltimeB = false;
        yield return new WaitForSeconds(guncoolB);
        cooltimeB = true;
    }

    IEnumerator tamakankakuY()
    {
        cooltimeY = false;
        yield return new WaitForSeconds(guncoolY);
        cooltimeY = true;
    }
}
