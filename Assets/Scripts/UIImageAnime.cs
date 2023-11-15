using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIImageAnime : MonoBehaviour
{
    public Image m_Image;
    public Sprite[] m_SpriteArray;
    public float m_Speed = 1f;
    public int m_IndexSprite;
    Coroutine m_CorotineAnim;
    bool IsDone;

 
    private void OnEnable()
    {
        m_Image.sprite = m_SpriteArray[0];
        Func_PlayUIAnim();

     
    }
   
    public void Func_PlayUIAnim()
    {

        IsDone = false;
        m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
    }

    private void OnDisable()
    {
        Func_StopUIAnim();
        m_IndexSprite = 0;
    }
    public void Func_StopUIAnim()
    {
        IsDone = true;
        StopCoroutine(m_CorotineAnim);
    }
    IEnumerator Func_PlayAnimUI()
    {
        yield return new WaitForSeconds(m_Speed);
        if (m_IndexSprite >= m_SpriteArray.Length)
        {
            m_IndexSprite = 0;
        }
        m_Image.sprite = m_SpriteArray[m_IndexSprite];
        m_IndexSprite += 1;
        if (IsDone == false)
            m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
    }

}
