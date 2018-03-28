using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
    //显示的文本信息
    public Text mText;
    public GameObject Bg;
    //开始的帧数
    private int mStartFrameCount;

    public static Toast Instance;

    private void Awake()
    {
        Instance = this;
    }
    //显示文本
    public void Show(string text)
    {
        mText.text = text;
        Bg.SetActive(true);
        mStartFrameCount = Time.frameCount;
    }
    //隐藏文本
    public void Hide()
    {
        mText.text = "";
        Bg.SetActive(false);
    }

    private void Update()
    {
        //点击后帧数超过150帧，则关闭信息弹框
        if (Time.frameCount - mStartFrameCount == 150 && mStartFrameCount != 0)
        {
            Hide();
        }
    }
}
