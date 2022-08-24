using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace WebApplMVC5
{
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string source, string alt, string title, int height = 400, int width = 400)
        {
            var imgTag = new TagBuilder("image");
            imgTag.MergeAttribute("src", source);
            imgTag.MergeAttribute("alt", alt);
            imgTag.MergeAttribute("title", title);
            imgTag.MergeAttribute("width", width.ToString());
            imgTag.MergeAttribute("height", height.ToString());
            return MvcHtmlString.Create(imgTag.ToString(TagRenderMode.SelfClosing));
        }
        public static string IsNullChnageToEmpty(this string inputString)
        {
            if (inputString == null)
            {
                inputString = string.Empty;
                return inputString;
            }
            else
            {
                return inputString;
            }
        }
        public static string ChangeFirstLetterUpper(this string inputString)
        {
            char[] charArray = inputString.ToCharArray();
            charArray[0] = char.IsUpper(charArray[0]) ? charArray[0] : char.ToUpper(charArray[0]);
            return new string(charArray);
        }

        public static string ChangeFirstLetterLower(this string inputString)
        {
            char[] charArray = inputString.ToCharArray();
            charArray[0] = char.IsLower(charArray[0]) ? charArray[0] : char.ToLower(charArray[0]);
            return new string(charArray);
        }


        public const int ImageMinimumBytes = 512;
        public static bool IsImageValid(this HttpPostedFileBase postedFile, int imageMaximumBytes)
        {


            if (postedFile == null || postedFile.ContentLength < 0)
            {
                return false;
            }
            //coverting filesize from kb to bytes 
            if (postedFile.ContentLength > imageMaximumBytes * 1024)
            {
                return false;
            }
            //  Check the image Content types

            if (!string.Equals(postedFile.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }


            //  Check the image extension

            var postedFileExtension = Path.GetExtension(postedFile.FileName);
            if (!string.Equals(postedFileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".png", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".gif", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }


            //  Attempt to read the file and check the first bytes

            try
            {
                if (!postedFile.InputStream.CanRead)
                {
                    return false;
                }

                //   Check whether the image size exceeding the limit or not

                if (postedFile.ContentLength < ImageMinimumBytes)
                {
                    return false;
                }

                byte[] buffer = new byte[ImageMinimumBytes];
                postedFile.InputStream.Read(buffer, 0, ImageMinimumBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }


            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image


            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {

                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                postedFile.InputStream.Position = 0;
            }

            return true;
        }

        public static bool IsFileValid(this HttpPostedFileBase postedFile, int imageMaximumBytes)
        {
            if (postedFile == null || postedFile.ContentLength < 0)
            {
                return false;
            }
            if (postedFile.ContentLength > imageMaximumBytes * 1024)
            {
                return false;
            }
            if (!string.Equals(postedFile.ContentType, "application/pdf", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "application/x-pdf", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "application/msword", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "text/plain", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "application/zip", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "application/x-zip-compressed", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "application/xml", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "application/vnd.openxmlformats-officedocument.presentationml.presentation", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "application/vnd.ms-excel", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(postedFile.ContentType, "application/vnd.ms-powerpoint", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            var postedFileExtension = Path.GetExtension(postedFile.FileName);
            if (!string.Equals(postedFileExtension, ".pdf", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".doc", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".docx", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".xml", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".xls", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".xlsx", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".ppt", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".pptx", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".zip", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".txt", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            MimeType mimeType = new MimeType();
            byte[] FileBytes = new byte[postedFile.ContentLength];
            string mime=mimeType.GetMimeType(FileBytes, postedFile.FileName);
            if(!string.Equals(mime, postedFile.ContentType, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }



            return true;
        }

        public static string StripHtml(this HtmlHelper helper, string inputString)
        {
            // Will this simple expression replace all tags???
            var tagsExpression = new Regex(@"</?.+?>");
            return tagsExpression.Replace(inputString, " ");
        }
        private static string StripHtmlTag(string inputString)
        {
            // Will this simple expression replace all tags???
            var tagsExpression = new Regex(@"</?.+?>");
            return tagsExpression.Replace(inputString, " ");
        }
        public static String UrlDecode(this HtmlHelper helper, String str)
        {
            return HttpUtility.UrlDecode(str);
        }
        public static String UrlEncode(this HtmlHelper helper, String str)
        {
            return HttpUtility.UrlEncode(str);
        }
        public static String UrlSanitizer(this HtmlHelper helper, String str)
        {
            var sanitizer = new HtmlSanitizer();
            return sanitizer.Sanitize(str);
        }
        public static string TruncateAtWord(this HtmlHelper htmlHelper, string value, int length)
        {

            value = StripHtmlTag(value);
            int maxlength = value.Length;
            if (value == null || value.Length < length || value.IndexOf(" ", length) == -1)
                return value;
            if (length < maxlength)
            {
                return value.Substring(0, value.IndexOf(" ", length)) + "...";
            }
            else
            {
                return value.Substring(0, value.IndexOf(" ", length));
            }

        }

    }
    public class MimeType
    {
        private static readonly byte[] BMP = { 66, 77 };
        private static readonly byte[] DOC = { 208, 207, 17, 224, 161, 177, 26, 225 };
        private static readonly byte[] EXE_DLL = { 77, 90 };
        private static readonly byte[] GIF = { 71, 73, 70, 56 };
        private static readonly byte[] ICO = { 0, 0, 1, 0 };
        private static readonly byte[] JPG = { 255, 216, 255 };
        private static readonly byte[] MP3 = { 255, 251, 48 };
        private static readonly byte[] OGG = { 79, 103, 103, 83, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0 };
        private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };
        private static readonly byte[] PNG = { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82 };
        private static readonly byte[] RAR = { 82, 97, 114, 33, 26, 7, 0 };
        private static readonly byte[] SWF = { 70, 87, 83 };
        private static readonly byte[] TIFF = { 73, 73, 42, 0 };
        private static readonly byte[] TORRENT = { 100, 56, 58, 97, 110, 110, 111, 117, 110, 99, 101 };
        private static readonly byte[] TTF = { 0, 1, 0, 0, 0 };
        private static readonly byte[] WAV_AVI = { 82, 73, 70, 70 };
        private static readonly byte[] WMV_WMA = { 48, 38, 178, 117, 142, 102, 207, 17, 166, 217, 0, 170, 0, 98, 206, 108 };
        private static readonly byte[] ZIP_DOCX = { 80, 75, 3, 4 };

        public string GetMimeType(byte[] file, string fileName)
        {

            string mime = "application/octet-stream"; //DEFAULT UNKNOWN MIME TYPE

            //Ensure that the filename isn't empty or null
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return mime;
            }

            //Get the file extension
            string extension = Path.GetExtension(fileName) == null
                                   ? string.Empty
                                   : Path.GetExtension(fileName).ToUpper();

            //Get the MIME Type
            if (file.Take(2).SequenceEqual(BMP))
            {
                mime = "image/bmp";
            }
            else if (file.Take(8).SequenceEqual(DOC))
            {
                mime = "application/msword";
            }
            else if (file.Take(2).SequenceEqual(EXE_DLL))
            {
                mime = "application/x-msdownload"; //both use same mime type
            }
            else if (file.Take(4).SequenceEqual(GIF))
            {
                mime = "image/gif";
            }
            else if (file.Take(4).SequenceEqual(ICO))
            {
                mime = "image/x-icon";
            }
            else if (file.Take(3).SequenceEqual(JPG))
            {
                mime = "image/jpeg";
            }
            else if (file.Take(3).SequenceEqual(MP3))
            {
                mime = "audio/mpeg";
            }
            else if (file.Take(14).SequenceEqual(OGG))
            {
                if (extension == ".OGX")
                {
                    mime = "application/ogg";
                }
                else if (extension == ".OGA")
                {
                    mime = "audio/ogg";
                }
                else
                {
                    mime = "video/ogg";
                }
            }
            else if (file.Take(7).SequenceEqual(PDF))
            {
                mime = "application/pdf";
            }
            else if (file.Take(16).SequenceEqual(PNG))
            {
                mime = "image/png";
            }
            else if (file.Take(7).SequenceEqual(RAR))
            {
                mime = "application/x-rar-compressed";
            }
            else if (file.Take(3).SequenceEqual(SWF))
            {
                mime = "application/x-shockwave-flash";
            }
            else if (file.Take(4).SequenceEqual(TIFF))
            {
                mime = "image/tiff";
            }
            else if (file.Take(11).SequenceEqual(TORRENT))
            {
                mime = "application/x-bittorrent";
            }
            else if (file.Take(5).SequenceEqual(TTF))
            {
                mime = "application/x-font-ttf";
            }
            else if (file.Take(4).SequenceEqual(WAV_AVI))
            {
                mime = extension == ".AVI" ? "video/x-msvideo" : "audio/x-wav";
            }
            else if (file.Take(16).SequenceEqual(WMV_WMA))
            {
                mime = extension == ".WMA" ? "audio/x-ms-wma" : "video/x-ms-wmv";
            }
            else if (file.Take(4).SequenceEqual(ZIP_DOCX))
            {
                mime = extension == ".DOCX" ? "application/vnd.openxmlformats-officedocument.wordprocessingml.document" : "application/x-zip-compressed";
            }

            return mime;
        }


    }
}

