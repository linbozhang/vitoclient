using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
public class UIAppList : MonoBehaviour {
    public GameObject appItemPrefab;
    public Transform itemParent;
    

    void OnEnable()
    {
        AppInfo.ActionGetAppList(User.CurToken,Callback);
    }
    void OnDisable()
    {
        for(int i = 0; i < itemParent.childCount; i++)
        {
            Destroy(itemParent.GetChild(i).gameObject);
        }
    }

    void Callback(bool success,string message)
    {
        if (success)
        {
            List<AppInfo> list=JsonConvert.DeserializeObject<List<AppInfo>>(message);
            for(int i = 0; i < list.Count; i++)
            {
                GameObject item=Instantiate<GameObject>(appItemPrefab);
                item.GetComponent<UIAppItem>().Init(list[i]);
                item.transform.SetParent(itemParent);
                item.transform.localRotation = Quaternion.identity;
                item.transform.localScale = Vector3.one;
            }
        }
    }
	// Use this for initialization
	void Start () {
		
	}


}
