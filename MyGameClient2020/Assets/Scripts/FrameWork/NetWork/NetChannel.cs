using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.NetWork
{
    public enum ChannelType
    {
        Connect,
        Accept,
    }
    public abstract class NetChannel
    {
        public ChannelType ChannelType { get; }

        public NetService Service { get; }

        public abstract MemoryStream Stream { get; }

        public int Error { get; set; }

        public IPEndPoint RemoteAddress { get; protected set; }

        private Action<NetChannel, int> errorCallback;
        
        public event Action<NetChannel,int> ErrorCallback
        {
            add
            {
                errorCallback += value;
            }
            remove
            {
                errorCallback -= value;
            }
        }
        private Action<MemoryStream> readCallback;
        private event Action<MemoryStream> ReadCallback
        {
            add
            {
                readCallback += value;
            }
            remove
            {
                readCallback -= value;
            }
        }

        protected void OnRead(MemoryStream memoryStream)
        {
            readCallback.Invoke(memoryStream);
        }
       protected void OnError(int e)
        {
            Error = e;
            errorCallback.Invoke(this, e);
        }

        protected NetChannel(NetService service,ChannelType channelType)
        {
            ChannelType = channelType;
            Service = service;
        }
        public abstract void Start();

        public abstract void Send(MemoryStream stream);

        public void Dispose()
        {

            Service.Remove(1);
        }
        
    }
}
