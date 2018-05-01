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
    public DataInsert dataInsert;
    private void Start()
    {
        GameObject dbConn = new GameObject("DataInsert dbConn");
        dataInsert = dbConn.AddComponent<DataInsert>();
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
            SceneManager.LoadScene("Scenes/AdminSubsystem");
            yield break;
        }
        if (dataInsert.CheckIfStudentExists(InputUsername.text))
        {
            Debug.Log("Checking Password - Current Password: " +  DataInsert.inputPassword);
            if (InputPassword.text == DataInsert.inputPassword)
            {
                dataInsert.SetAllValues(InputUsername.text);
                Debug.Log("Success Loggin In!");
                SceneManager.LoadScene("Scenes/StudentSubsystem");
            }
            else
            {
                Debug.Log("Failed Logging on!");
            }
        }
    }
}
