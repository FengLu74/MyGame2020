using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.ReferencePool;
using System;
using MGame.Resource;

public class loads : MonoBehaviour
{
    public Animator anim1;
    public Animator anim2;
    public string animName ;
    public Transform BattleRoot;
    public Transform UIRoot;
    public void OnClickAnimEvent()
    {
        //anim1.Play(string.IsNullOrEmpty(animName) ? "skill1" : animName);
        //anim2.Play(string.IsNullOrEmpty(animName) ? "move" : animName);
    }
    private void Start()
    {
        GameObject gameObject1 = AssetBundlesManager.Instance.LoadAsset<GameObject>("sp_1001003", "sp_1001003");
        if(gameObject1!=null)
        {
            Debug.Log("name = " + gameObject1.name);
            gameObject1 = Instantiate(gameObject1);
            gameObject1.transform.parent = BattleRoot;
            gameObject1.transform.localPosition = Vector3.zero;
            gameObject1.transform.localEulerAngles = Vector3.zero;
        }

        GameObject gameObject2 = AssetBundlesManager.Instance.LoadAsset<GameObject>("UITestPanel", "UITestPanel");
        if (gameObject1 != null)
        {
            Debug.Log("name = " + gameObject2.name);
            gameObject2 = Instantiate(gameObject2);
            gameObject2.transform.parent = UIRoot;
            gameObject2.transform.localPosition = Vector3.zero;
            gameObject2.transform.localEulerAngles = Vector3.zero;
        }
        //GameObject gameObject2 = AssetBundlesManager.Instance.LoadAsset<GameObject>("sp_1001006", "sp_1001006");
        Debug.Log("dsaaaaaaaaaaaaaaaaa");
    }
}

