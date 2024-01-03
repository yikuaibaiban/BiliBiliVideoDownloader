// /*==========================================================
// Copyright (c) 2021
// BiliBiliVideoDownloader - All rights reserved
// Create By : ykbb
// Create with Jetbrains Rider.
// ===========================================================
// File description:
// ===========================================================
// Date            Name                 Description of Change
// 2021-02-22      ykbb
// ==========================================================*/

using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BiliBiliVideoDownloader.Utils
{
    /// <summary>
    /// url地址下载
    /// </summary>
    public static class UrlSourceDownloader
    {
        /// <summary>
        /// 地址源码下载器
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="encoding"></param>
        /// <param name="method"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string GetDownloadSource(Uri uri, Encoding encoding, string cookies = "", string method = "GET",
            string contentType = "application/x-www-form-urlencoded")
        {
            var request = (HttpWebRequest) WebRequest.Create(uri);
            request.Method = method;
            request.ContentType = contentType;
            request.Headers.Add("Accept-Encoding", "gzip");
            request.ServerCertificateValidationCallback += ServerCertificateValidationCallback;
            request.Accept =
                "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            request.UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.182 Safari/537.36 Edg/88.0.705.74";
            if (!string.IsNullOrEmpty(cookies))
                request.Headers.Add("Cookie", cookies);

            //=============POST===============
            // Encoding encoding = Encoding.UTF8;
            // byte[] postData = encoding.GetBytes(postDataStr);
            // request.ContentLength = postData.Length;
            // Stream myRequestStream = request.GetRequestStream();
            // myRequestStream.Write(postData, 0, postData.Length);
            // myRequestStream.Close();
            //=============POST end===============

            using var response = (HttpWebResponse) request.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK) return string.Empty;

            var responseStream = response.GetResponseStream();

            if (responseStream == null) return string.Empty;

            if (response.ContentEncoding.ToLower().Contains("gzip"))
            {
                using var stream = new GZipStream(responseStream, CompressionMode.Decompress);
                using var streamReader = new StreamReader(stream, encoding);
                return streamReader.ReadToEnd();
            }

            if (response.ContentEncoding.ToLower().Contains("deflate"))
            {
                using var stream = new DeflateStream(responseStream, CompressionMode.Decompress);
                using var reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }

            using var defaultReader = new StreamReader(responseStream, encoding);

            return defaultReader.ReadToEnd();
        }

        private static bool ServerCertificateValidationCallback(object sender, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslpolicyerrors)
        {
            return true;
        }
    }
}