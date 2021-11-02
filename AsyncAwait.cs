using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace AsyncAwait {
    class Program {
        private class File {
            public string name;
            public long size;

            public File(string name, long size) {
                this.name = name;
                this.size = size;
            }
        }

        static async Task<File> DownloadFileAsync(string URL, string FileName) {
            await Task.Run(
                () => {
                    WebClient Client = new WebClient();
                    Client.DownloadFile(URL, FileName);

                    Thread.Sleep(((new Random()).Next() % 30) * 1000);
                });
            long size = (new FileInfo(FileName)).Length;
            return new File(FileName, size);
        }
        static async Task Main(string[] args) {
            //https://content.chulaonline.net/NOF004/NOF004-chapter01.mp4
            //https://content.chulaonline.net/NOF004/NOF004-chapter02.mp4
            //https://content.chulaonline.net/NOF004/NOF004-chapter03.mp4
            //https://content.chulaonline.net/NOF004/NOF004-chapter04.mp4
            //https://content.chulaonline.net/NOF004/NOF004-chapter05.mp4

            List<Task<File>> DownloadTasks = new List<Task<File>>();

            while (true) {
                Console.Write("Download from: ");
                string url = Console.ReadLine().Trim();

                Console.Write("Save to: ");
                string file = Console.ReadLine().Trim();

                if (url == "" || file == "") {
                    await Task.WhenAll(DownloadTasks);
                    for (var i=0; i<DownloadTasks.Count; i++) {
                        Console.WriteLine(DownloadTasks[i].Result.name + "   " + DownloadTasks[i].Result.size);
                    }
                    break;
                }

                Task<File> task = DownloadFileAsync(url, file);
                DownloadTasks.Add(task);

                /*while (DownloadTasks.Count > 0) {
                    Task<File> finishedTask = await Task.WhenAny(DownloadTasks);
                    DownloadTasks.Remove(finishedTask);
                }*/
            }
        }
    }
}
