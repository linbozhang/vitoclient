using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISelectUser : MonoBehaviour {
    public InputField userName;
    public Button btnCommit;
    public Text txtResult;

    void OnEnable()
    {
        btnCommit.onClick.AddListener(OnClick);
    }
    void OnDisable()
    {
        btnCommit.onClick.RemoveListener(OnClick);
    }

    void OnClick()
    {
        string username = userName.text;
        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("用户名为空");
            return;
        }
        User.ActionSelectUser(username,User.CurToken,callback);
    }
    void callback(bool success,string result)
    {
        txtResult.text = result;
    }
	// Use this for initialization
	void Start () {
	    	
	}	
}
