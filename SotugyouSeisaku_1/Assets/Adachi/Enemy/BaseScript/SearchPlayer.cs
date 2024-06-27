//�v���C���[��T��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayer : MonoBehaviour
{
    #region �Q�[���I�u�W�F�N�g
    [HideInInspector] public bool search; //�v���C���[��������

    GameObject Enemy;
    EnemyBase enemyscript;
    #endregion
    void Start()
    {
        StartUp();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CatchPlayer();
    }


    #region �֐���`
    /// <summary>
    /// �����ݒ�
    /// </summary>
    void StartUp()
    {
        search = false; //search��false�ɂ���

        Enemy = transform.parent.gameObject;
        enemyscript = Enemy.GetComponent<EnemyBase>();
    }

    /// <summary>
    /// �v���C���[������������
    /// </summary>
    void CatchPlayer()
    {
        if (search == true)
        {
            enemyscript.BaseMove();
        }

        //Debug.Log(search);
    }

    //�����蔻��ɐڐG�������Ă�����
    //OnTriggerStay�͐ڐG���Ă��鎞�ɌĂ΂ꑱ����iOnTriggerEnter�͈�񂵂��Ă΂�Ȃ��j
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            search = true; //search��true�ɂ���
        }
    }

    //�����蔻��ɐڐG���Ȃ�������
    private void OnTriggerExit(Collider other)
    {
        search = false; //search��false�ɂ���
    }
    #endregion
}
