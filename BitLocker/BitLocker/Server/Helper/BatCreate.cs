using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BitLocker.Server.Helper
{
    public class BatCreate
    {

        public static String CreateBATFile(ArrayList coreString)
        {
            String DirectoryName = Program.currentPath + "Temp\\";
            String filename = "Set_" + DateTime.Now.ToFileTime() + ".bat";

            if (!Directory.Exists(DirectoryName))
            {
                Directory.CreateDirectory(DirectoryName);
            }

            if (!File.Exists(filename))
            {
                try
                {
                    FileStream myFs = new FileStream(DirectoryName + filename, FileMode.Create);
                    StreamWriter mySw = new StreamWriter(myFs);

                    mySw.WriteLine("@echo off");

                    //mySw.WriteLine("C:");
                    //mySw.WriteLine("cd C:\\Windows\\System32\\");

                    foreach(String core in coreString)
                    {
                        mySw.WriteLine(core + " > " + DirectoryName + filename.Substring(0, filename.Length - 4) + ".log");
                    }
                    mySw.WriteLine("cls");

                    mySw.Close();
                    myFs.Close();
                }
                catch(Exception)
                {

                }
            }


            return DirectoryName + filename;
        }


    }
}
