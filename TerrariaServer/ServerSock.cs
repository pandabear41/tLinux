using System;
using System.Net.Sockets;
namespace Terraria
{
    public class ServerSock
    {
        public bool active = false;
        public bool announced = false;
        public Socket clientSocket;
        public bool kill = false;
        public bool locked = false;
        public string name = "Anonymous";
        public NetworkStream networkStream;
        public string oldName = "";
        public byte[] readBuffer;
        public int state = 0;
        public int statusCount;
        public int statusMax;
        public string statusText = "";
        public string statusText2;
        public TcpClient tcpClient = new TcpClient();
        public bool[,] tileSection = new bool[Main.maxTilesX / 200, Main.maxTilesY / 150];
        public int timeOut = 0;
        public int whoAmI = 0;
        public byte[] writeBuffer;
        public void Reset()
        {
            for (int i = 0; i < Main.maxSectionsX; i++)
            {
                for (int j = 0; j < Main.maxSectionsY; j++)
                {
                    this.tileSection[i, j] = false;
                }
            }
            if (this.whoAmI < 255)
            {
                Main.player[this.whoAmI] = new Player();
            }
            this.timeOut = 0;
            this.statusCount = 0;
            this.statusMax = 0;
            this.statusText2 = "";
            this.statusText = "";
            this.name = "Anonymous";
            this.state = 0;
            this.locked = false;
            this.kill = false;
            this.active = false;
            NetMessage.buffer[this.whoAmI].Reset();
            if (this.networkStream != null)
            {
                this.networkStream.Close();
            }
            if (this.tcpClient != null)
            {
                this.tcpClient.Close();
            }
        }
        public void ServerReadCallBack(System.IAsyncResult ar)
        {
            int num = 0;
            if (!Netplay.disconnect)
            {
                try
                {
                    num = this.networkStream.EndRead(ar);
                }
                catch
                {
                }
                if (num == 0)
                {
                    this.kill = true;
                }
                else
                {
                    if (Main.ignoreErrors)
                    {
                        try
                        {
                            NetMessage.RecieveBytes(this.readBuffer, num, this.whoAmI);
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        NetMessage.RecieveBytes(this.readBuffer, num, this.whoAmI);
                    }
                }
            }
            this.locked = false;
        }
        public void ServerWriteCallBack(System.IAsyncResult ar)
        {
            messageBuffer messageBuffer = NetMessage.buffer[this.whoAmI];
            messageBuffer.spamCount--;
            if (this.statusMax > 0)
            {
                this.statusCount++;
            }
        }
    }
}
