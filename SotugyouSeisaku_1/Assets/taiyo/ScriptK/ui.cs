using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui : MonoBehaviour
{
    [SerializeField] Text hptext; // HP��\������e�L�X�g
    [SerializeField] Text bullettext; // �c�e����\������e�L�X�g
    [SerializeField] Image redgun, bluegun, yerrowgun; // ���ꂼ��̏e�̉摜�̗��n
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region HP�ƒe���̃e�L�X�g
        hptext.text = kariplayer.HP + " / 100";

        if (kariplayer.guncolor == 1)
        {
            bullettext.text = kariplayer.gunbulletR + " / " + kariplayer.gunstockR;
        }
        else if (kariplayer.guncolor == 2)
        {
            bullettext.text = kariplayer.gunbulletB + " / " + kariplayer.gunstockB;
        }
        else if (kariplayer.guncolor == 3)
        {
            bullettext.text = kariplayer.gunbulletY + " / " + kariplayer.gunstockY;
        }
        #endregion


        // �摜��1�������邭������I��ł邩���o�I�ɕ�����₷������
        if (kariplayer.guncolor == 1)
        {
            redgun.color = new Color32(255, 0, 0, 255);
            bluegun.color = new Color32(0, 0, 255, 100);
            yerrowgun.color = new Color32(255, 255, 0, 100);
        }
        else if (kariplayer.guncolor == 2)
        {
            redgun.color = new Color32(255, 0, 0, 100);
            bluegun.color = new Color32(0, 0, 255, 255);
            yerrowgun.color = new Color32(255, 255, 0, 100);
        }
        else if (kariplayer.guncolor == 3)
        {
            redgun.color = new Color32(255, 0, 0, 100);
            bluegun.color = new Color32(0, 0, 255, 100);
            yerrowgun.color = new Color32(255, 255, 0, 255);
        }
    }
}
