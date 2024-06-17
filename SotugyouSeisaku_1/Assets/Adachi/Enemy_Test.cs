using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] float speed;
    public float rotationSpeed = 50f; // 回転速度

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        // 左矢印キーが押されたら左に回転
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        // 右矢印キーが押されたら右に回転
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        // 上矢印キーが押されたら上に回転
        /*if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }

        // 下矢印キーが押されたら下に回転
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.forward,  rotationSpeed * Time.deltaTime);
        }*/

    }

    private void PlayerMove()
    {
        //前に進む
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(speed, 0, 0);
        }
        //後ろに進む
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(-speed, 0, 0);
        }
        //右に進む
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0, 0, -speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(0, 0, speed);
        }
    }
}
