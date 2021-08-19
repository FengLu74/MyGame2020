using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.NetWork
{
    public abstract class NetService
    {
        public abstract NetChannel GetNetChannel(long id);

        private Action<NetChannel> acceptCallBack;

        public event Action<NetChannel> AcceptCallBack
        {
            add
            {
                acceptCallBack += value;

            }
            remove
            {
                acceptCallBack -= value;
            }
        }
        protected void OnAccept(NetChannel channel)
        {
            acceptCallBack.Invoke(channel);
        }

        public abstract NetChannel ConnectChannel(IPEndPoint ipEndPoint);

        public abstract NetChannel GetNetChannel(string address);

        public abstract void Remove(long channelId);

        public abstract void Update();

    }
}
