using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
