using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FFMPEGPoc
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo d = new DirectoryInfo(@"Input");

            var files = d.GetFiles();

            Process cli = new Process();

            cli.StartInfo.FileName = "cmd.exe";
            cli.StartInfo.RedirectStandardInput = true;
            cli.StartInfo.RedirectStandardOutput = true;
            cli.StartInfo.UseShellExecute = false;

            cli.Start();

            var cmdTemplate = "ffmpeg -i \"Input\\{0}\" -b:a 128K -vn \"Output\\{1}\" ";

            foreach (var file in files)
            {
                if (file.Name.Split('.').Length > 2) throw new ApplicationException("File Name Contains '.' please rename this file and try again: "+file.Name);
                var command = String.Format(cmdTemplate, file.Name, file.Name.Split('.')[0] + ".mp3");
                cli.StandardInput.WriteLine(command);
            }

            cli.StandardInput.Flush();
            cli.StandardInput.Close();
            Console.WriteLine(cli.StandardOutput.ReadToEnd());

        }
    }
}
