using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Sockets.server
{
    public class SharpChannel : IDisposable
    {
        private Socket _socket;

        private bool _disposed;

        private object _disposedLock = new object();

        private SharpServer _server;

        private SocketAsyncEventArgs _targetSocketAsync;

        private EndPoint remoteIpEndPoint;

        public event Action Closed;
        public event Action<byte[]> Receive;

        public SharpChannel(SharpServer server, SocketAsyncEventArgs targetSocketAsync)
        {
            this._server = server;
            this._targetSocketAsync = targetSocketAsync;
            this._socket = (Socket)_targetSocketAsync.UserToken;
            this.remoteIpEndPoint = this._socket.RemoteEndPoint;
        }

        public void Dispose()
        {
            lock (_disposedLock)
            {
                if (_disposed) return;
                _disposed = true;
                try
                {
                    this._socket.Close();
                }
                catch (Exception)
                {

                }
            }
        }

        public void OnReceive(byte[] buffer)
        {
            Receive.Invoke(buffer);
        }

        public void Send(byte[] buffer)
        {
            _server.Send(_targetSocketAsync, buffer);
        }
    }
}
