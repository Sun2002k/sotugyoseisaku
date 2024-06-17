//�G�̊�{����

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemymove : MonoBehaviour
{
    Vector3 v_enemy; //�G�̃x�N�^�[
    Vector3 v_player; //�v���C���[�̃x�N�^�[
    Vector3 v_range; //�G�ƃv���C���[�̋���
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
    /// �v���C���[��ǂ�������
    /// </summary>
    public void EnemyMove()
    {
        //�v���C���[�̌��ւƍs��
        v_enemy = this.transform.position;   //�G�̍��W
        v_player = player.transform.position; //�v���C���[�̍��W
        v_range = v_player - v_enemy; //�v���C���[�̍��W�ƓG�̍��W���΃x�N�^�[
        v_move = v_range * speed; //���΃x�N�^�[�ɃX�s�[�h���|����
        this.transform.position += v_move;

        //�v���C���[�̕��֌�������
        float theta = Mathf.Atan2(v_player.x - v_enemy.x, v_player.z - v_enemy.z);//�v���C���[�̍��W�ƓG�̍��W��x��z�ɕ����Ă��ꂼ��v�Z
        float deg = -(270 - theta*Mathf.Rad2Deg);
        transform.rotation = Quaternion.Euler(new Vector3(0,deg,0));//�v���C���[�̕��֌���
    }
}
