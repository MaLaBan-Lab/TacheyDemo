using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace Tachey001.Repository
{
    public class MemoryCacheRepository
    {
        public MemoryCacheRepository()
        {
            MemoryCacheRepository.connection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(ConfigurationOptions.Parse(connStr));
            });
        }
        private static Lazy<ConnectionMultiplexer> connection;

        public static ConnectionMultiplexer _conn
        {
            get
            {
                return connection.Value;
            }
        }
        /// <summary>
        /// 設定快取資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set<T>(string key, T value) where T : class
        {
            var db = _conn.GetDatabase();

            db.StringSet(key, ObjectToJsonStr(value), TimeSpan.FromDays(1));
        }
        /// <summary>
        /// 取得指定快取資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key) where T : class
        {
            var db = _conn.GetDatabase();

            return JsonStrToObject<T>(db.StringGet(key));
        }
        /// <summary>
        /// 刪除指定快取資料
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            var db = _conn.GetDatabase();

            db.KeyDelete(key);
        }
        /// <summary>
        /// 位元陣列轉物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arrBytes"></param>
        /// <returns></returns>
        private T JsonStrToObject<T>(string str) where T : class
        {
            if (str == null) return null;

            return JsonConvert.DeserializeObject<T>(str); ;
        }
        /// <summary>
        /// 物件轉位元陣列
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string ObjectToJsonStr(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private static string connStr
        {
            get
            {
                return "redis-14549.c1.asia-northeast1-1.gce.cloud.redislabs.com:14549,password=NllKANpZtfpwk0vwvwkHARJf27U05IoU";
            }
        }
    }
}