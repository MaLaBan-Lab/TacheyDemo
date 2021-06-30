using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Newtonsoft.Json;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;

namespace Tachey001.AccountModels
{
    public class SendFileTask
    {
        private bool _stopping = false;
        private Stream _file;
        public void DoSendFileTask()
        {
            var myAccount = new Account
            {
                Cloud = Credientials.Cloud,
                ApiKey = Credientials.ApiKey,
                ApiSecret = Credientials.ApiSecret
            };

            Cloudinary _cloudinary = new Cloudinary(myAccount);

            var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription("1-1", _file),
                PublicId = "1-1",
                Overwrite = true,
            };

            _cloudinary.UploadLarge(uploadParams);
        }

        public void Run(Stream file)
        {
            _file = file;
            var aThread = new Thread(TaskLoop);
            aThread.IsBackground = true;
            aThread.Priority = ThreadPriority.BelowNormal;  // 避免此背景工作拖慢 ASP.NET 處理 HTTP 請求.
            aThread.Start();
        }
        public void Stop()
        {
            _stopping = true;
        }
        private void Log(string msg)
        {
            System.IO.File.AppendAllText(@"D:\MVC\Tachey\Tachey\Tachey001\AccountModels\Log\Bglog.txt", msg + Environment.NewLine);
        }
        private void TaskLoop()
        {
            // 設定每一輪工作執行完畢之後要間隔幾分鐘再執行下一輪工作.
            const int LoopIntervalInMinutes = 1000 * 10 * 1;

            Log("TaskLoop on thread ID: " + Thread.CurrentThread.ManagedThreadId.ToString());
            while (!_stopping)
            {
                try
                {
                    DoSendFileTask();
                    DoSendFile();
                }
                catch (Exception ex)
                {
                    // 發生意外時只記在 log 裡，不拋出 exception，以確保迴圈持續執行.
                    Log(ex.ToString());
                }
                finally
                {
                    // 每一輪工作完成後的延遲.
                    System.Threading.Thread.Sleep(LoopIntervalInMinutes);
                }
            }
        }
        private void DoSendFile()
        {
            // 發送 email。這裡只固定輸出一筆文字訊息至 log 檔案，方便觀察測試結果。
            string msg = String.Format("DoSendFile() at {0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
            Log(msg);
        }
    }
}