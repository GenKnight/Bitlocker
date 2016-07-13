using BitLocker.Common.VSMsg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BitLocker.Tool
{
    public class VirtualPipe
    {

        public static bool isV2S = false;
        public static bool isS2V = false;

        private static VS_Msg S2V_Msg = new VS_Msg();
        private static VS_Msg V2S_Msg = new VS_Msg();

        public static void Send2V(VS_Msg msg)
        {
            while (isS2V)
            {
                Thread.Sleep(1);
            }
            S2V_Msg = msg;
            isS2V = true;
        }

        public static void Send2S(VS_Msg msg)
        {
            while(isV2S)
            {
                Thread.Sleep(1);
            }
            V2S_Msg = msg;
            isV2S = true;
        }

        public static VS_Msg GetS2VMessage()
        {
            isS2V = false;
            return S2V_Msg;
        }

        public static VS_Msg GetV2SMessage()
        {
            isV2S = false;
            return V2S_Msg;
        }


    }
}
