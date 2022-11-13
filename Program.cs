using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FFMPEGPoc
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DirectoryInfo d = new DirectoryInfo(@"Input");

            var files = d.GetFiles();

            var cmdTemplate = "ffmpeg -i \"Input\\{0}\" -b:a 128K -vn \"Output\\{1}\" ";
            List<string> lines = new List<string>();
            foreach (var file in files)
            {
                if (file.Name.Split('.').Length > 2) throw new ApplicationException("File Name Contains '.' please rename this file and try again: " + file.Name);
                lines.Add(String.Format(cmdTemplate, file.Name, file.Name.Split('.')[0] + ".mp3"));
            }

            await File.WriteAllLinesAsync("Runner.bat", lines, CancellationToken.None);

        }
    }
}
