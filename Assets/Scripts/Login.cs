using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{

    public InputField InputUsername;
    public InputField InputPassword;

    public Button BtnLogin;
    public Button BtnResetPassword;
    public Button BtnExit;

    private void Start()
    {
        BtnLogin.onClick.AddListener(OnLoginClick);
        BtnExit.onClick.AddListener(() => Application.Quit());
        BtnResetPassword.onClick.AddListener(() => SceneManager.LoadScene("ResetPswd"));
    }

    private void OnLoginClick()
    {
        if (string.IsNullOrEmpty(InputUsername.text) || string.IsNullOrEmpty(InputPassword.text))
        {
            Toast.Instance.Show("Username or Password is Empty");
            return;
        }
        StartCoroutine(Check());
    }

    IEnumerator Check()
    {
        if(InputUsername.text == "student" && InputPassword.text == "student")
        {
            Debug.Log("Hard forcing into student subsystem");
            SceneManager.LoadScene("Scenes/StudentSubsystem");
            yield break;
        }
        else if (InputUsername.text == "admin" && InputPassword.text == "admin")
        {
            Debug.Log("Hard forcing into admin subsystem");
            SceneManager.LoadScene("");
            yield break;
        }
        string url = "http://chang1134.com/php/taobao/purties180323/manager.php?action=login&username=" + InputUsername.text + "&password=" + InputPassword.text;
        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
        {
            Toast.Instance.Show("Error:" + www.error);
            yield break;
        }
        if (www.text == "E")
        {
            Toast.Instance.Show("Username Or Password Error");
            yield break;
        }
        //登陆成功
        SceneManager.LoadScene(int.Parse(www.text) < 10 ? "UserManage" : "BaseUnity");
    }
}
