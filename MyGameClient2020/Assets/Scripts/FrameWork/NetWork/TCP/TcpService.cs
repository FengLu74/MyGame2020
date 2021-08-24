using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.NetWork
{
    public sealed class TcpService : NetService
    {
        public override NetChannel ConnectChannel(IPEndPoint ipEndPoint)
        {
            throw new NotImplementedException();
        }

        public override NetChannel GetNetChannel(long id)
        {
            throw new NotImplementedException();
        }

        public override NetChannel GetNetChannel(string address)
        {
            throw new NotImplementedException();
        }

        public override void Remove(long channelId)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
