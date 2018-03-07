using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Toggle))]
public class UIToggleContent : MonoBehaviour {
    public GameObject content;
    private Toggle _toggle;
    public Toggle toggle
    {
        get
        {
            if (_toggle == null)
                _toggle = GetComponent<Toggle>();
            return _toggle;
        }
    }

    void OnEnable()
    {
        toggle.onValueChanged.AddListener(OnValueChange);
    }
    void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(OnValueChange);
    }

    void OnValueChange(bool isOn)
    {
        if (content != null)
        {
            content.SetActive(isOn);
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
}
