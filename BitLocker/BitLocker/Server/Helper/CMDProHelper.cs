using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace BitLocker.Server.Helper
{
    public class CMDProHelper
    {

        private Process proc = null;
        private bool isReadEnd = false;
        private String GetOutPut = null;

        public CMDProHelper()
        {
            proc = new Process();
        }


        public bool GetReadFinishTag()
        {
            return isReadEnd;
        }

        public String GetCMDOutString()
        {
            if(GetOutPut == null)
            {
                GetOutPut = "";
            }
            return GetOutPut;
        }


        public String RunCmd(String cmd, String args, String action)
        {
            String outStr = null;
            isReadEnd = false;
            GetOutPut = "";

            try
            {
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.FileName = cmd;
                proc.StartInfo.Arguments = args;

                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;

                proc.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                proc.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);  

                
                proc.Start();
                //proc.StandardInput.WriteLine(action);

                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();

                
                //Thread.Sleep(5000); //等待获取信息
                //proc.StandardInput.WriteLine("exit");
                
                //outStr = proc.StandardOutput.ReadToEnd();

                //StreamReader reader = proc.StandardOutput;//截取输出流
                //String outStr = reader.ReadLine();//每次读取一行
                //while (!reader.EndOfStream)
                //{
                //    //PrintThrendInfo(outStr);
                //    outStr = reader.ReadLine();
                //}
                
            }
            catch(Exception ex)
            {
                outStr = ex.Message.ToString();
            }

            return outStr;
        }

        public void StopCMD()
        {
            try
            {
                proc.CancelOutputRead();
                proc.CancelErrorRead();
            }
            catch(Exception)
            {

            }
            try
            {
                proc.CloseMainWindow();
            }
            catch(Exception){}

            proc.Close();

            isReadEnd = true;
        }


        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if(e.Data != null)
            {
                GetOutPut += e.Data;
            }
        }

        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if(e.Data != null)
            {
                GetOutPut += e.Data;
            }
        }

    }
}
