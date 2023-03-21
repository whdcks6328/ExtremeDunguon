using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIndicator : MonoBehaviour
{
    [SerializeField]
    GameObject[] itemModels = new GameObject[3];
    [SerializeField]
    Text level;
    [SerializeField]
    Image hpBar;
    [SerializeField]
    Image hpBar_Back;
    
    int HP_Max = 100;
    int HP = 0;
    int prevHP = 0;
    Vector2 sizeDelta;

    Color healthy;
    Color crisis;

    public enum WeaponType
    {
        Homing, UniformLinearmotion, Laser
    }

    void Start()
    {
        HP = HP_Max;
        prevHP = HP_Max;
        sizeDelta.x = hpBar.rectTransform.sizeDelta.x;
        sizeDelta.y = hpBar.rectTransform.sizeDelta.y;
        healthy = hpBar.color;
        crisis = Color.red;
    }
    public void SetWeaponValue(WeaponType weapon, int level)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == (int)weapon) itemModels[i].SetActive(true);
            else itemModels[i].SetActive(false);
        }
        this.level.text = "Lv. " + LevelColorText((int)weapon, level, 19);
    }

    public void SetHP(int hp)
    {
        hp = Mathf.Clamp(hp, 0, HP_Max);//範囲外の値を丸める
        if (hp == prevHP) return;
        StopCoroutine("AnimateHPBar");//再生中なら強制停止
        StartCoroutine("AnimateHPBar", hp);
        HP = hp;
        prevHP = hp;
    }



    IEnumerator AnimateHPBar(int hp)
    {
        float t = 0;
        sizeDelta.x = ConvertHpToBarWidth(hp);
        hpBar.rectTransform.sizeDelta = sizeDelta;
        Vector2 start = new Vector2(ConvertHpToBarWidth(prevHP), sizeDelta.y);
        Vector2 goal = new Vector2(ConvertHpToBarWidth(hp), sizeDelta.y);
        if (hp < prevHP)//ダメージ
        {
            hpBar.color = (HP <= HP_Max / 3f ? crisis : healthy);
            while (t <= 1)
            {
                t += Time.deltaTime * 2f;
                hpBar_Back.rectTransform.sizeDelta = Vector2.Lerp(start, goal, t);
                yield return null;
            }
        }
        else//回復
        {
            while (t <= 1)
            {
                t += Time.deltaTime;
                hpBar.rectTransform.sizeDelta = Vector2.Lerp(start, goal, t * 2f);
                hpBar.color = Color.Lerp(Color.white, HP <= HP_Max / 3f ? crisis : healthy, t);
                yield return null;
            }
        }
    }


    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.P))
        {
            HP += 5;
            SetHP(HP);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            HP -= 5;
            SetHP(HP);
        }
        
    }

    float ConvertHpToBarWidth(int hp)
    {
        return (float)hp / (float)HP_Max * 685f;
    }

    string LevelColorText(int weapon, int level, int size)
    {
        string s = string.Empty;
        switch (weapon)
        {
            case 0: s = "<color=#cc66cc>"; break;
            case 1: s = "<color=#ffbb55>"; break;
            case 2: s = "<color=#55cccc>"; break;
            default: break;
        }
        return s + "<size=" + size.ToString() + ">" + level.ToString() + "</size>" + "</color>";
    }


}
