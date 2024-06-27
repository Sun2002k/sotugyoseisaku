//�e�̐���

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletMove : MonoBehaviour
{
    #region �ϐ�
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


    #region �֐���`
    /// <summary>
    /// �����ݒ�
    /// </summary>
    void StartUp()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 v_player;
        Vector3 v_bullet;

        //���W���Z�b�g
        v_bullet = this.transform.position;   //�G�̍��W
        v_player = player.transform.position; //�v���C���[�̍��W

        //�v���C���[�̕��֌�������
        float angle = 90; // �G����]����p�x
        float theta = Mathf.Atan2(v_player.x - v_bullet.x, v_player.z - v_bullet.z);//�v���C���[�̍��W�ƓG�̍��W��x��z�ɕ����Ă��ꂼ��v�Z
        float deg = -(angle - theta * Mathf.Rad2Deg);
        transform.rotation = Quaternion.Euler(new Vector3(0, deg, 0));//�v���C���[�̕��֌���
    }

    /// <summary>
    /// �e����������
    /// </summary>
    void Bullet()
    {
        Vector3 velocity = gameObject.transform.rotation * new Vector3(-speed, 0, 0);
        gameObject.transform.position -= velocity * Time.deltaTime;
        
        float count = 0; count++;
        if(count > 100)//count���z�����玩�󂷂�
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
