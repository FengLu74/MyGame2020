﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.ReferencePool;
using System;

public class loads : MonoBehaviour
{
    public Animator anim1;
    public Animator anim2;
    public string animName ;
    
    public void OnClickAnimEvent()
    {
        anim1.Play(string.IsNullOrEmpty(animName) ?"001": animName);
        anim2.Play(string.IsNullOrEmpty(animName) ? "move" : animName);
    }
    private void Start()
    {
        TestReferencePool tp = new TestReferencePool();
        TestReferencePool.C xxx =  tp.TestH();

        Debug.Log("  " + xxx.aa + " cc = " + xxx.cc + " dd = " + xxx.dd);
        Debug.Log(" typeof(T) = " + typeof(TestReferencePool.C)+ "  ");

        IReference re = (IReference)xxx;
        Type referenceType = re.GetType();
        Debug.Log(" GetType(T) = " + referenceType);

        tp.Rel(xxx);
        Debug.Log("  " + xxx.aa + " cc = " + xxx.cc + " dd = " + xxx.dd);



        Derieved d = new Derieved();
        IBase ib = d;
        ib.PrintName();

    }
}
public interface IBase
{
     void  PrintName();
}
public class Base : IBase
{
    public  void PrintName()
    {
        Debug.Log("Class name : Base");
    }
}
public class Derieved : Base
{
    public void PrintName()
    {
        Debug.Log("Class name : Derieved");
    }
}
