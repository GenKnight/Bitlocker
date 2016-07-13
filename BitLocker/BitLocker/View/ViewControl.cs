using BitLocker.Common.VSMsg;
using BitLocker.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BitLocker.View
{
    public class ViewControl
    {
        private static bool isListenThreadRun = false;
        private static Thread ListenServerThread;

        private MainWin _mainwin;

        public ViewControl(MainWin mainwin)
        {
            _mainwin = mainwin;
        }

        public void Send2Server(VS_Msg msg)
        {
            VirtualPipe.Send2S(msg);
        }

        public void StartListenServerThread()
        {
            ListenServerThread = new Thread(new ThreadStart(ListenThreadFun));
            isListenThreadRun = true;
            ListenServerThread.Start();
        }

        public void StopListenServerThread()
        {
            if(ListenServerThread != null)
            {
                try
                {
                    ListenServerThread.Abort();
                }
                catch(Exception ex)
                {
                    String err = ex.Message.ToString();
                }
            }

            isListenThreadRun = false;
        }


        private void ListenThreadFun()
        {
            while (isListenThreadRun)
            {
                if(VirtualPipe.isS2V)
                {
                    VS_Msg getmsg = VirtualPipe.GetS2VMessage();
                    switch(getmsg.type)
                    {
                        case MsgType.TYPE.SHOWLOG:
                            {
                                _mainwin.SetRichText(getmsg.content);
                            }
                            break;
                        case MsgType.TYPE.SETPROGRESS:
                            {
                                String[] getTemp = getmsg.content.Split(',');
                                if(getTemp.Length != 3)
                                {
                                    break;
                                }

                                bool isAdd = false;
                                int val = 0;
                                int max = 100;
                                if (getTemp[0].Equals("true")) isAdd = true;
                                bool flag = int.TryParse(getTemp[1], out val);
                                bool flag_ = int.TryParse(getTemp[2], out max);
                                if(flag && flag_)
                                {
                                    _mainwin.SetProgressBar(isAdd, val, max);
                                }
                                else
                                {
                                    String err = "val invalid";
                                }
                            }
                            break;
                        case MsgType.TYPE.EnableLock:
                            {
                                _mainwin.SetBtnEnbale();
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
