//�v���C���[��T��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayer : MonoBehaviour
{
    [HideInInspector] public bool search; //�v���C���[��������

    GameObject Enemy;
    Enemymove enemyscript;
    void Start()
    {
        StartUp();
    }

    // Update is called once per frame
    void Update()
    {
        if(search == true)
        {
            enemyscript.EnemyMove();
        }
    }

    /// <summary>
    /// �����ݒ�
    /// </summary>
    void StartUp()
    {
        search = false; //search��false�ɂ���

        Enemy = transform.parent.gameObject;
        enemyscript = Enemy.GetComponent<Enemymove>();
    }

    //IsTrigger�ƐڐG������
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            search = true; //search��true�ɂ���
        }
    }

    //IsTrigger�ƐڐG���Ȃ�������
    private void OnTriggerExit(Collider other)
    {
        search = false; //search��false�ɂ���
    }
}
