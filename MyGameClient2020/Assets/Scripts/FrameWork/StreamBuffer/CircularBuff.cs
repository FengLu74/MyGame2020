using System;
using System.IO;

namespace FrameWork.StreamBuffer
{
    public class CircularBuff : Stream
    {
        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }
        public override bool CanWrite
        {
            get { return true; }
        }
        public override long Length
        {
            get
            {
                return 1;
            }
        }

        public override long Position { get ; set ; }



        public override int Read(byte[] buffer, int offset, int count)
        {
            return 0;
        }



        public override void Write(byte[] buffer, int offset, int count)
        {

        }
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }
        public override void Flush()
        {
            throw new NotImplementedException();
        }
        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }
    }
}
