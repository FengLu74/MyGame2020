using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestShaderFeature : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Mutil;
    public Button MutilBtn;
    public GameObject Feature;
    public Button FeatureBtn;
    void Start()
    {

        SetBtnEvent(MutilBtn, OnMutileShaderEvent);
        SetBtnEvent(FeatureBtn, OnFeatureShaderEvent);
    }
    public void SetBtnEvent(Button button,Action action)
    {
        Button.ButtonClickedEvent btnEvent = button.onClick;
        if (btnEvent != null)
        {
            btnEvent.RemoveAllListeners();
            btnEvent.AddListener(
                () =>
                {
                    action();
                }
                );
        }
    }
    bool mutilStatus = false;
    // mutil shader
    void OnMutileShaderEvent()
    {
        Material _materialToggle = Mutil.GetComponent<Image>().material;

        //_materialToggle.EnableKeyword("T2");
        if (mutilStatus)
        {
            _materialToggle.EnableKeyword("UNITY_UI_ALPHACLIP");

        }
        else
        {
            _materialToggle.DisableKeyword("UNITY_UI_ALPHACLIP");
        }
        mutilStatus = !mutilStatus;
    }
    bool featureStatus = false;
    // Feature shader
    void OnFeatureShaderEvent()
    {
        Material _materialToggle = Feature.GetComponent<Image>().material;
        if (featureStatus)
        {

            _materialToggle.EnableKeyword("DISTORTENABLED");
        }
        else
        {
            _materialToggle.DisableKeyword("DISTORTENABLED");
        }
        featureStatus = !featureStatus;

    }
}
