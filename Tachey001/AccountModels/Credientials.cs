using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CloudinaryDotNet;

namespace Tachey001.AccountModels
{
    public class Credientials
    {
        public static Cloudinary Init()
        {
            return new Cloudinary(new Account() { Cloud = Cloud, ApiKey = ApiKey, ApiSecret = ApiSecret });
        }

        public const string Cloud = "dzaevdqyt";
        public const string ApiKey = "799872766428446";
        public const string ApiSecret = "eUQS5SOFf96sMx_bWRx9VTU1Wng";
    }
}