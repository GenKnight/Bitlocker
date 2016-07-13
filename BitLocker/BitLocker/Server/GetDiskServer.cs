using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BitLocker.Server.Helper;
using System.Threading;
using System.Diagnostics;
using BitLocker.Common.VSMsg;

namespace BitLocker.Server
{
    public class GetDiskServer
    {
        public ArrayList AllBitLockerInfo;

        public ArrayList GetAllBitLockerDisk()
        {
            AllBitLockerInfo = new ArrayList();

            //获取所有磁盘
            ArrayList AllDiskInfo = new ArrayList();
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach(DriveInfo di in allDrives)
            {
                AllDiskInfo.Add(di.Name.Substring(0, di.Name.Length - 1));
            }

            
            //查询该磁盘是否为BitLocker加密盘
            foreach (String name in AllDiskInfo)
            {
                //通知当前界面信息和进度条
                VS_Msg showmsg = new VS_Msg();
                showmsg.type = MsgType.TYPE.SHOWLOG;
                showmsg.content = "检测" + name + "盘...";
                Program.servercontrol.Send2View(showmsg);
                VS_Msg probarmsg = new VS_Msg();
                probarmsg.type = MsgType.TYPE.SETPROGRESS;
                probarmsg.content = "true,0" + "," + AllDiskInfo.Count;
                Program.servercontrol.Send2View(probarmsg);

                //直接启动方式
                String result = ExcludeBitLockerCMD("manage-bde", " -status " + name, "");

                //处理日志
                result = result.Replace(" ", "");

                //判断类型
                if (!result.Contains("加密方法:无"))
                {
                    if (result.Contains("错误"))
                    {
                        continue;
                    }

                    VS_Msg showOkMsg = new VS_Msg();
                    showOkMsg.type = MsgType.TYPE.SHOWLOG;
                    showOkMsg.content = name + "是BitLocker加密盘";
                    Program.servercontrol.Send2View(showOkMsg);

                    //debug
                    //if(name.ToLower().Contains("f"))
                    //{
                    //    showOkMsg = new VS_Msg();
                    //    showOkMsg.type = MsgType.TYPE.SHOWLOG;
                    //    showOkMsg.content = name + "暂不加锁";
                    //    Program.servercontrol.Send2View(showOkMsg);
                    //    continue;
                    //}

                    //判断当前盘是否已锁定
                    if(!result.Contains("锁定状态:已锁定"))
                    {
                        VS_Msg showOkMsg_ = new VS_Msg();
                        showOkMsg_.type = MsgType.TYPE.SHOWLOG;
                        showOkMsg_.content = "开始对" + name + "进行加锁操作...";
                        Program.servercontrol.Send2View(showOkMsg_);

                        //当前盘没有被锁定,执行加锁
                        String result_ = ExcludeBitLockerCMD("manage-bde", " -lock " + name, "");
                        result_ = result_.Replace(" ", "");
                        if(result_.Contains("现在已锁定"))
                        {
                            showOkMsg_ = new VS_Msg();
                            showOkMsg_.type = MsgType.TYPE.SHOWLOG;
                            showOkMsg_.content = "加锁成功";
                            Program.servercontrol.Send2View(showOkMsg_);
                        }
                        else
                        {
                            showOkMsg_ = new VS_Msg();
                            showOkMsg_.type = MsgType.TYPE.SHOWLOG;
                            showOkMsg_.content = "加锁失败,错误：\r\n" + result_;
                            Program.servercontrol.Send2View(showOkMsg_);
                        }
                    }
                    else
                    {
                        showOkMsg = new VS_Msg();
                        showOkMsg.type = MsgType.TYPE.SHOWLOG;
                        showOkMsg.content = name + "磁盘已加锁";
                        Program.servercontrol.Send2View(showOkMsg);
                    }

                    AllBitLockerInfo.Add(name);
                }

                //创建脚本启动方式
                /*
                //创建执行脚本
                ArrayList setList = new ArrayList();
                setList.Add("manage-bde -status " + name);
                String filename = BatCreate.CreateBATFile(setList);

                //执行脚本
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = filename;
                try
                {
                    Process.Start(psi);
                }
                catch(Exception)
                {

                }

                Thread.Sleep(1000);
                //解析日志文件
                String logname = filename.Substring(0, filename.Length - 4) + ".log";
                StreamReader sr = null;
                try
                {
                    sr = new StreamReader(logname, Encoding.Default);
                    String cLine = "";
                    String result = "";
                    while((cLine = sr.ReadLine()) != null)
                    {
                        result += cLine;
                    }
                    sr.Close();

                    //处理日志
                    result = result.Replace(" ", "");

                    //判断类型
                    if (!result.Contains("加密方法:无"))
                    {
                        if (result.Contains("错误: BitLocker 无法打开卷"))
                        {
                            continue;
                        }
                        AllBitLockerInfo.Add(name);
                    }

                }
                catch(Exception)
                {

                }

                //清理日志文件和脚本文件
                try
                {
                    File.Delete(filename);
                    File.Delete(logname);
                }
                catch(Exception){}
                */

            }

            return AllBitLockerInfo;
        }

        //执行BitLock
        private String ExcludeBitLockerCMD(String cmd, String args, String action)
        {
            CMDProHelper cmdpro = new CMDProHelper();
            String runfalg = cmdpro.RunCmd(cmd, args, action);

            int waittimes = 5;
            while (cmdpro.GetCMDOutString().Length < 80)
            {
                //等待字符串结束
                if (waittimes <= 0)
                {
                    break;
                }
                Thread.Sleep(1000);
                waittimes--;
            }

            cmdpro.StopCMD();
            String result = cmdpro.GetCMDOutString();
            return result;
        }

    }
}
