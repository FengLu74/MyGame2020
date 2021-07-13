using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.ReferencePool
{
    /// <summary>
    /// 引用池信息
    /// </summary>
    public class ReferencePoolInfo
    {
        private readonly Type m_Type;
        private readonly int m_UnusedReferenceCount;
        private readonly int m_UsingReferenceCount;
        private readonly int m_AcquireReferenceCount;
        private readonly int m_ReleaseReferenceCount;
        private readonly int m_AddReferenceCount;
        private readonly int m_RemoveReferenceCount;
        /// <summary>
        /// 初始化引用池信息实例
        /// </summary>
        /// <param name="type"></param>
        /// <param name="unusedRefCount"></param>
        /// <param name="usingRefCount"></param>
        /// <param name="acquireRefCount"></param>
        /// <param name="releaseRefCount"></param>
        /// <param name="addRefCount"></param>
        /// <param name="removeRefCount"></param>
        public ReferencePoolInfo(Type type,int unusedRefCount,int usingRefCount,int acquireRefCount,int releaseRefCount,int addRefCount,int removeRefCount)
        {
            m_Type = type;
            m_UnusedReferenceCount = unusedRefCount;
            m_UsingReferenceCount = usingRefCount;
            m_AcquireReferenceCount = acquireRefCount;
            m_ReleaseReferenceCount = releaseRefCount;
            m_AddReferenceCount = addRefCount;
            m_RemoveReferenceCount = removeRefCount;
        }

        public Type Type
        {
            get { return m_Type; }
        }

        /// <summary>
        /// 获取未使用引用数量。
        /// </summary>
        public int UnusedReferenceCount
        {
            get
            {
                return m_UnusedReferenceCount;
            }
        }

        /// <summary>
        /// 获取正在使用引用数量。
        /// </summary>
        public int UsingReferenceCount
        {
            get
            {
                return m_UsingReferenceCount;
            }
        }

        /// <summary>
        /// 获取获取引用数量。
        /// </summary>
        public int AcquireReferenceCount
        {
            get
            {
                return m_AcquireReferenceCount;
            }
        }

        /// <summary>
        /// 获取归还引用数量。
        /// </summary>
        public int ReleaseReferenceCount
        {
            get
            {
                return m_ReleaseReferenceCount;
            }
        }

        /// <summary>
        /// 获取增加引用数量。
        /// </summary>
        public int AddReferenceCount
        {
            get
            {
                return m_AddReferenceCount;
            }
        }

        /// <summary>
        /// 获取移除引用数量。
        /// </summary>
        public int RemoveReferenceCount
        {
            get
            {
                return m_RemoveReferenceCount;
            }
        }

    }
}
