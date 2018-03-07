using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAddApp : MonoBehaviour {
    public InputField inputAppName;
    public InputField inputVersionId;
    public InputField inputVersionCode;
    public InputField inputIconPath;
    public InputField inputDescription;
    public Button btnAdd;

    void OnEnable()
    {
        btnAdd.onClick.AddListener(OnClick);
    }

    void OnDisable()
    {
        btnAdd.onClick.RemoveListener(OnClick);
    }

    void OnClick()
    {
        string appname = inputAppName.text;
        string versionId = inputVersionId.text;
        string versionCode = inputVersionCode.text;
        string iconPath = inputIconPath.text;
        string description = inputDescription.text;
        if(string.IsNullOrEmpty(appname)
            ||string.IsNullOrEmpty(versionId)
            ||string.IsNullOrEmpty(versionCode)
            )
        {
            Debug.LogError("some value is null");
            return;
        }
        int realCode = System.Int32.Parse(versionCode);
        AppInfo.ActionAddApp(new AppInfo(
            appname,versionId, realCode, iconPath, description
            ), User.CurToken);
    }

	// Use this for initialization
	void Start () {
		
	}
}
