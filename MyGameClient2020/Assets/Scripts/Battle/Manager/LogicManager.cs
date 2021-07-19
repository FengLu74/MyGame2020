﻿using Battle.Logic.MMath;

namespace Battle.Manager
{
    public class LogicManager
    {
        public static readonly LogicManager Instance = new LogicManager();  //在服务器上可提出来 通过 ThreadLocal<LogicManager> 保证每个客户端线程所有逻辑对象都是独立的
        private int _frameCountPerSecond;
        private Fix64 _secondPerFrame;
        private int _logicFrameCounter;
        public bool _pause;



        public int frameCountPerSecond
        {
            get
            {
                return _frameCountPerSecond;
            }
        }
        public int TimeToFrameCeil(Fix64 second)
        {
            Fix64 frame = second * frameCountPerSecond;
            return (int)Fix64.Ceiling(frame);
        }
    }
}
