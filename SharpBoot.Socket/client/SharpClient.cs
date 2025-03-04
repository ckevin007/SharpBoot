using SharpBoot.Sockets.common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoot.Sockets.client
{
    public class SharpClient : IDisposable
    {
        private readonly int bufferSize = 8192;
        private MTask receiveTask;
        private Socket _socket;
        private readonly IPEndPoint remoteEndPoint;
        public bool AutoReconnect { get; set; }
        public bool IsConnected { get; set; }
        public event Action OnConnected;
        public event Action OnClosed;
        public event Action OnConnecting;
        private readonly object sockerConnectingLock = new object();
        private bool isConnecting = false;
        public event Action<byte[]> OnReceiveBytes;
        private bool disposed;
        public SharpClient(string ip, int port, bool autoReconnect = true)
        {
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            AutoReconnect = autoReconnect;
        }

        private async Task<bool> ConnectIngAsync()
        {
            if (IsConnected) return true;
            int connectTimes = 0;
            while (true)
            {
                connectTimes++;
                try
                {
                    _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    OnConnecting?.Invoke();
                    await _socket.ConnectAsync(remoteEndPoint);
                    if (_socket.Connected)
                    {
                        StartLoopReceive();
                    }
                    IsConnected = _socket.Connected;
                    OnConnected?.Invoke();
                    return IsConnected;
                }
                catch (Exception e)
                {
                    if (!AutoReconnect)
                    {
                        return false;
                    }
                }
            }
        }

        public async Task<bool> ConnectAsync()
        {
            lock (sockerConnectingLock)
            {
                if (isConnecting) return false;
                isConnecting = true;
            }
            bool flag = await ConnectIngAsync();
            isConnecting = false;
            return flag;
        }



        public virtual void Dispose()
        {
            if (disposed) return;
            disposed = true;
            AutoReconnect = false;
            _socket?.Dispose();
        }


        private async Task BeClosed()
        {
            IsConnected = false;
            OnClosed?.Invoke();
            if (AutoReconnect) await ConnectAsync();
        }

        private void StartLoopReceive()
        {
            receiveTask?.Cancel();
            receiveTask = new MTask();
            var flag = receiveTask.CreateNewTaskFlag();
            receiveTask.Action = (async () =>
            {
                while (flag.Continue)
                {
                    if (!_socket.Connected)
                    {
                        await BeClosed();
                        return;
                    }
                    var buffer = new byte[bufferSize].AsMemory();
                    int count = 0;
                    try
                    {
                        count = await _socket.ReceiveAsync(buffer, SocketFlags.None);
                    }
                    catch (Exception e)
                    {
                        await BeClosed();
                        return;
                    }
                    if (count == 0)
                    {
                        await BeClosed();
                        return;
                    }
                    if (count < bufferSize)
                    {
                        OnReceiveBytes(buffer.ToArray().AsSpan().Slice(0, count).ToArray());
                    }
                    else
                    {
                        OnReceiveBytes(buffer.ToArray());
                    }
                }
            });
            receiveTask.Start();
        }

        public async Task<bool> Send(byte[] buffer)
        {
            if (!IsConnected) return false;
            try
            {
                while (buffer != null && buffer.Length > 0)
                {
                    int count = await _socket.SendAsync(buffer.AsMemory(), SocketFlags.None);
                    int leftCount = buffer.Length - count;
                    if (leftCount == 0) return true;
                    buffer = buffer.AsSpan()[count..leftCount].ToArray();
                }
            }
            catch (Exception e)
            {
                await BeClosed();
            }
            return false;
        }
    }
}
