using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILogin : MonoBehaviour {
    public InputField UserName;
    public InputField Pwd;
    public Button btnLogin;
    public GameObject other;
    void OnEnable()
    {
        btnLogin.onClick.AddListener(OnClick);
    }

    void OnDisable()
    {
        btnLogin.onClick.RemoveListener(OnClick);
    }

    void OnClick()
    {
        string username = UserName.text;
        string pwd = Pwd.text;
        User.Login(username,pwd,Callback);
    }
    void Callback(bool success,string msg)
    {
        if (success)
        {
            Destroy(gameObject);
            other.SetActive(true);
            //gameObject.SetActive(false);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
}
