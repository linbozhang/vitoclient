using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIDeleteUser : MonoBehaviour {
    public InputField UserId;
    public Button btnDel;

    void OnEnable()
    {
        btnDel.onClick.AddListener(OnClick);
    }

    void OnDisable()
    {
        btnDel.onClick.RemoveListener(OnClick);
    }

    void OnClick()
    {
        string userids = UserId.text;
        int userid = 0;
        if (string.IsNullOrEmpty(userids))
        {
            Debug.LogError("userid is null");
            return;
        }

        if(System.Int32.TryParse(userids,out userid))
        {
            User.ActionDeleteUser(userid,User.CurToken);
        }
    }
	// Use this for initialization
	void Start () {
		
	}
}
