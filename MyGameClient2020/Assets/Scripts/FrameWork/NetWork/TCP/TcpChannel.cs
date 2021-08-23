
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace FrameWork.NetWork
{
    public class TcpChannel : NetChannel
    {
        private Socket socket;
        private readonly MemoryStream memoryStream;

        public override void Send(MemoryStream stream)
        {

        }
        public override MemoryStream Stream
        {
            get
            {
                return memoryStream;
            }
        }
        public override void Start()
        {

        }
        public TcpChannel(IPEndPoint iPEndPoint,NetService service):base(service,ChannelType.Connect)
        {

        }
    }
}

