using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] float speed;
    public float rotationSpeed = 50f; // ��]���x

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        // �����L�[�������ꂽ�獶�ɉ�]
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        // �E���L�[�������ꂽ��E�ɉ�]
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        // ����L�[�������ꂽ���ɉ�]
        /*if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }

        // �����L�[�������ꂽ�牺�ɉ�]
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.forward,  rotationSpeed * Time.deltaTime);
        }*/

    }

    private void PlayerMove()
    {
        //�O�ɐi��
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(speed, 0, 0);
        }
        //���ɐi��
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(-speed, 0, 0);
        }
        //�E�ɐi��
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
