using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIAppItem : MonoBehaviour {
    public Image appIcon;
    public Text appName;
    public Text versionId;
    public Text versionCode;
    public Text description;
    public Button btn;

    void OnEnable()
    {
        btn.onClick.AddListener(OnClick);
    }

    void OnDisable()
    {
        btn.onClick.RemoveListener(OnClick);
    }

    void OnClick()
    {

    }
    private AppInfo appInfo;
    public void Init(AppInfo info)
    {
        appInfo = info;
        appName.text = info.AppName;
        versionCode.text = info.VersionCode.ToString();
        versionId.text = info.VersionId.ToString();
        description.text = info.Description;
    }
	// Use this for initialization
	void Start () {
		
	}

}
