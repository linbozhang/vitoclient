using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIAddUser : MonoBehaviour {
    public InputField UserName;
    public InputField UserPwd;
    public Toggle UserType;
    public InputField UserPwdCheck;
    public Button btnCommit;

    void OnEnable()
    {
        btnCommit.onClick.AddListener(OnClick);
    }
    void OnDisable()
    {
        btnCommit.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        string userName = UserName.text;
        string userPwd = UserPwd.text;
        string userPwdCheck = UserPwdCheck.text;
        
        if(string.IsNullOrEmpty(userName)||string.IsNullOrEmpty(userPwd)||string.IsNullOrEmpty(userPwdCheck))
        {
            Debug.LogError("用户名或密码为空");
            return;
        }

        if (userPwd != userPwdCheck)
        {
            Debug.LogError("密码不一致");
            return;
        }

        int userType = UserType.isOn ? 1 : 0;
        User.ActionAddUser(new User(userName,userPwd,userType),User.CurToken);
    }

    
}
