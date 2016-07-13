using BitLocker.Common.VSMsg;
using BitLocker.Tool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BitLocker.Server
{
    public class ServerControl
    {
        private static bool isListenRun = false;
        private static Thread ListenViewThread;

        public ServerControl()
        {

        }

        public void Send2View(VS_Msg msg)
        {
            VirtualPipe.Send2V(msg);
        }

        public void StartListenThread()
        {
            ListenViewThread = new Thread(new ThreadStart(ListenThreadFun));
            isListenRun = true;
            ListenViewThread.Start();
        }

        public void StopListenThread()
        {
            if (ListenViewThread != null)
            {
                try
                {
                    ListenViewThread.Abort();
                }
                catch(Exception ex)
                {
                    String err = ex.Message.ToString();
                }
            }

            isListenRun = false;
        }

        private void ListenThreadFun()
        {
            while (isListenRun)
            {
                if(VirtualPipe.isV2S)
                {
                    VS_Msg getmsg = VirtualPipe.GetV2SMessage();
                    switch(getmsg.type)
                    {
                        case MsgType.TYPE.GETALL:
                            {
                                //启动磁盘信息服务,获取加密的磁盘
                                GetDiskServer getdiskServer = new GetDiskServer();
                                ArrayList bitdisk = getdiskServer.GetAllBitLockerDisk();
                                String resultString = "@加密磁盘列表:";
                                foreach(String name in bitdisk)
                                {
                                    resultString += ( "[" + name + "] ");
                                }

                                VS_Msg msg = new VS_Msg();
                                msg.type = MsgType.TYPE.SHOWLOG;
                                msg.content = resultString;
                                Program.servercontrol.Send2View(msg);
                                msg = new VS_Msg();
                                msg.type = MsgType.TYPE.EnableLock;
                                Program.servercontrol.Send2View(msg);
                                msg = new VS_Msg();
                                msg.type = MsgType.TYPE.SHOWLOG;
                                msg.content = "@:磁盘列表识别完毕,加锁操作执行完毕!";
                                Program.servercontrol.Send2View(msg);
                            }
                            break;
                        default:
                            break;
                    }
                }

                Thread.Sleep(10);
            }
        }




    }
}
