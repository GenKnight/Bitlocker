using BitLocker.Common.VSMsg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BitLocker.View
{
    public partial class MainWin : Form
    {
        public MainWin()
        {
            InitializeComponent();

            //this.StartBtn.Enabled = false;

            Thread showThread = new Thread(new ThreadStart(InilizeCoreFun));
            showThread.Start();
        }

        private void InilizeCoreFun()
        {
            Thread.Sleep(1000); //等待窗口创建完毕

            SetRichText("程序名：BitLocker");
            SetRichText("作者：PureCity");
            SetRichText("版本：1.0");
            SetRichText("时间：2016/07/14");
            SetRichText("版本说明：当前版本仅支持中文版操作系统，且目前仅有微软自带BitLocker的加锁功能！\r\n建议反馈请Email至jincitykasto@126.com，谢谢！");
            SetRichText("--------------------------------------------------------------------");

        }

        
        /// <summary>
        /// 默认发送获取加密盘的信息
        /// </summary>
        private void ShowBitLockerDiskThreadFun()
        {
            SetRichText("@:正在识别所有的BitLocker加密磁盘,请稍后......");
            VS_Msg msg = new VS_Msg();
            msg.type = MsgType.TYPE.GETALL;
            Program.viewcontrol.Send2Server(msg);
        }

        public delegate void SetBtnEnableDelegate();
        public void SetBtnEnbale()
        {
            if (StartBtn.InvokeRequired)
            {
                SetBtnEnableDelegate outdelegate = new SetBtnEnableDelegate(SetBtnEnbale);
                this.BeginInvoke(outdelegate, new object[] { });
                return;
            }
            else
            {
                StartBtn.Enabled = true;
            }
        }


        public delegate void OutRichTextDelegate(String content);
        public void SetRichText(String content)
        {
            if(LogRichBox.InvokeRequired)
            {
                OutRichTextDelegate outdelegate = new OutRichTextDelegate(SetRichText);
                this.BeginInvoke(outdelegate, new object[] {content});
                return;
            }
            else
            {
                LogRichBox.AppendText(content);
                LogRichBox.AppendText("\r\n");

                //让文本框获取焦点 
                LogRichBox.Focus();
                //设置光标的位置到文本尾 
                LogRichBox.Select(LogRichBox.TextLength, 0);
                //滚动到控件光标处 
                LogRichBox.ScrollToCaret();
            }
        }


        public delegate void OutProgressBarDelegate(bool add, int val, int max);
        public void SetProgressBar(bool add, int val, int max)
        {
            if(ClosePrograssBar.InvokeRequired)
            {
                OutProgressBarDelegate outdelegate = new OutProgressBarDelegate(SetProgressBar);
                this.BeginInvoke(outdelegate, new object[] {add, val, max});
                return;
            }
            else
            {
                ClosePrograssBar.Maximum = max;

                if(add)
                {
                    ClosePrograssBar.Value += 1;
                }
                else
                {
                    ClosePrograssBar.Value = val;
                }
                
            }
        }

        private void LockClick(object sender, EventArgs e)
        {
            SetProgressBar(false, 0, 0);    //重置

            Thread showThread = new Thread(new ThreadStart(ShowBitLockerDiskThreadFun));
            showThread.Start();

            StartBtn.Enabled = false;
        }


    }
}
