using System;
using System.Net.Sockets;
namespace Terraria
{
    public class ClientSock
    {
        public bool active = false;
        public bool locked = false;
        public NetworkStream networkStream;
        public byte[] readBuffer;
        public int state = 0;
        public int statusCount;
        public int statusMax;
        public string statusText;
        public TcpClient tcpClient = new TcpClient();
        public int timeOut = 0;
        public byte[] writeBuffer;
        public void ClientReadCallBack(System.IAsyncResult ar)
        {
            if (!Netplay.disconnect)
            {
                int num = this.networkStream.EndRead(ar);
                if (num == 0)
                {
                    Netplay.disconnect = true;
                    Main.statusText = "Lost connection";
                }
                else
                {
                    if (Main.ignoreErrors)
                    {
                        try
                        {
                            NetMessage.RecieveBytes(this.readBuffer, num, 256);
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        NetMessage.RecieveBytes(this.readBuffer, num, 256);
                    }
                }
            }
            this.locked = false;
        }
        public void ClientWriteCallBack(System.IAsyncResult ar)
        {
            messageBuffer messageBuffer = NetMessage.buffer[256];
            messageBuffer.spamCount--;
        }
    }
}
