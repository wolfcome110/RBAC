using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Net.Mail;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;



namespace EF.Common
{
    /// <summary>
    /// 常用辅助操作类
    /// </summary>
    public class Helper
    {
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //等价于^[+-]?\d+[.]?\d+$
        private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");


        #region 自动产生的记录ID号
        /// <summary>
        /// 自动产生的记录ID号
        /// </summary>
        public static string Id
        {
            get
            {
                return "ID" + DateTime.Now.ToString("yyMMddhhmmss") + DateTime.Now.Millisecond;
            }
        }
        #endregion


        #region 自动产生的记录ID号
        public static string GetRandomCode(int length, string mode)
        {
            string str = null;
            switch (mode)
            {
                case "1":
                    str = "1234567890";
                    break;
                case "2":
                    str = "abcdefghijklmnopqrstuvwxyz";
                    break;
                case "3":
                    str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;
                case "4":
                    str = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    break;
                case "5":
                    str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                    break;
                case "6":
                    str = "abcdefghijklmnopqrstuvwxyz1234567890";
                    break;
                default:
                    str = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
                    break;
            }

            Random rnd = new Random();

            string randString = "";
            int len = str.Length - 1;
            for (int i = 0; i < length; i++)
            {
                int num = rnd.Next(0, len);
                randString += str[num];
            }
            return randString;
        }
        #endregion


        //#region Jmail组件发送邮件
        ////
        ///// <summary>
        ///// 发送邮件
        ///// </summary>
        ///// <param name="Subject">邮件标题</param>
        ///// <param name="body">邮件正文件内容</param>
        ///// <param name="FromEmail">发件人</param>
        ///// <param name="ToEmail">收件人</param>
        ///// <param name="UserName">发件人邮箱账号</param>
        ///// <param name="Password">发件人邮箱密码</param>
        //public static void JmailSendMail(string Subject, string Body, string FromEmail, string ToEmail, string UserName, string Password)
        //{
        //    myJmail.Message Jmail = new myJmail.Message();
        //    DateTime t = DateTime.Now;
        //    //String AddAttachment = this.FileUploadSubject.PostedFile.FileName;
        //    //Silent属性：如果设置为true,JMail不会抛出例外错误. JMail. Send( () 会根据操作结果返回true或false
        //    Jmail.Silent = true;
        //    //Jmail创建的日志，前提loging属性设置为true
        //    Jmail.Logging = true;
        //    //字符集，缺省为"US-ASCII"
        //    Jmail.Charset = "GB2312";
        //    //信件的contentype. 缺省是"text/plain"） : 字符串如果你以HTML格式发送邮件, 改为"text/html"即可。
        //    Jmail.ContentType = "text/html";
        //    //添加收件人
        //    Jmail.AddRecipient(ToEmail, "", "");
        //    Jmail.From = FromEmail;
        //    //发件人邮件用户名
        //    Jmail.MailServerUserName = UserName;
        //    //发件人邮件密码
        //    Jmail.MailServerPassWord = Password;
        //    //设置邮件标题
        //    Jmail.Subject = Subject;
        //    //邮件添加附件,(多附件的话，可以再加一条Jmail.AddAttachment( "c:\\test.jpg",true,null);)就可以搞定了。［注］：加了附件，讲把上面的Jmail.ContentType="text/html";删掉。否则会在邮件里出现乱码。
        //    //Jmail.AddAttachment(AddAttachment, true, null);
        //    //邮件内容
        //    Jmail.Body = Body;
        //    //Jmail发送的方法
        //    Jmail.Send("smtp.163.com", false);
        //    Jmail.Close();

        //}
        //#endregion


        #region System.Net.Mail发送邮件
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="Subject">邮件标题</param>
        /// <param name="body">邮件正文件内容</param>
        /// <param name="FromEmail">发件人</param>
        /// <param name="ToEmail">收件人</param>
        /// <param name="UserName">发件人邮箱账号</param>
        /// <param name="Password">发件人邮箱密码</param>
        public static void NetSendMail(string Subject, string Body, string FromEmail, string ToEmail, string UserName, string Password)
        {
            SmtpClient ObjSmtpClient = new SmtpClient();
            ObjSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;       //指定电子邮件发送方式
            ObjSmtpClient.Host = "smtp.163.com";                                 //指定SMTP服务器
            ObjSmtpClient.Credentials = new System.Net.NetworkCredential(FromEmail, Password);   //用户名和密码

            MailMessage ObjMailMessage = new MailMessage(FromEmail, ToEmail);
            ObjMailMessage.Subject = Subject;           //发送邮件主题
            ObjMailMessage.Body = Body;                 //发送邮件内容
            ObjMailMessage.BodyEncoding = System.Text.Encoding.Default;    //发送邮件正文编码
            ObjMailMessage.IsBodyHtml = true;              //设置为HTML格式
            ObjMailMessage.Priority = MailPriority.High;   //优先级
            //if (!string.IsNullOrEmpty(StrFileName)) ObjMailMessage.Attachments.Add(new Attachment(StrFileName));  //邮件的附件
            ObjSmtpClient.Send(ObjMailMessage);
        }
        #endregion


        #region 检查管理员是否登陆
        /// <summary>
        /// 检查管理员是否登陆
        /// 存于客户端的管理员登陆标识为IsLogin
        /// </summary>
        /// <returns></returns>
        public static void IsLogin()
        {
            HttpCookie cookies = HttpContext.Current.Request.Cookies["IsLogin"];
            if (cookies == null)
            {
                HttpContext.Current.Response.Redirect("/", true);
            }

            if (cookies.Value == "false")
            {
                HttpContext.Current.Response.Redirect("/", true);
            }

            return;
        }

        #endregion


        #region 检查Request查询字符串的键值是否是数字

        /// <summary>
        /// 检查Request查询字符串的键值，是否是数字，最大长度限制
        /// </summary>
        /// <param name="req">Request</param>
        /// <param name="inputKey">Request的键值</param>
        /// <param name="maxLen">最大长度</param>
        /// <returns>返回Request查询字符串</returns>
        public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)
        {
            string retVal = string.Empty;
            if (inputKey != null && inputKey != string.Empty)
            {
                retVal = req.QueryString[inputKey];
                if (null == retVal)
                    retVal = req.Form[inputKey];
                if (null != retVal)
                {
                    retVal = SqlText(retVal, maxLen);
                    if (!IsNumber(retVal))
                        retVal = string.Empty;
                }
            }
            if (retVal == null)
                retVal = string.Empty;
            return retVal;
        }

        #endregion


        #region 是否数字字符串
        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            Match m = RegNumber.Match(inputData);
            return m.Success;
        }
        #endregion


        #region 是否数字字符串 可带正负号

        /// <summary>
        /// 是否数字字符串 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            Match m = RegNumberSign.Match(inputData);
            return m.Success;
        }

        #endregion


        #region 是否是浮点数
        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            Match m = RegDecimal.Match(inputData);
            return m.Success;
        }
        #endregion


        #region 是否是浮点数 可带正负号
        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            Match m = RegDecimalSign.Match(inputData);
            return m.Success;
        }

        #endregion


        #region 中文检测

        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsHasCHZN(string inputData)
        {
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }

        #endregion


        #region 邮件地址
        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string inputData)
        {
            Match m = RegEmail.Match(inputData);
            return m.Success;
        }

        #endregion


        #region 检查字符串最大长度，返回指定长度的串

        /// <summary>
        /// 检查字符串最大长度，返回指定长度的串
        /// </summary>
        /// <param name="sqlInput">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>			
        public static string SqlText(string sqlInput, int maxLength)
        {
            if (sqlInput != null && sqlInput != string.Empty)
            {
                sqlInput = sqlInput.Trim();
                if (sqlInput.Length > maxLength)//按最大长度截取字符串
                    sqlInput = sqlInput.Substring(0, maxLength);
            }
            return sqlInput;
        }

        #endregion


        #region 对字符串的编码转换操作
        /// <summary>
        /// 对Html代码进行编码
        /// </summary>
        /// <param name="strHtml">要转换的字符串</param>
        /// <returns>编码后的字符串</returns>
        public static string ToString(string strHtml)
        {
            //StringBuilder html = new StringBuilder(strHtml);
            //html.Replace("<", "&lt;");
            //html.Replace(">", "&gt:");
            //html.Replace((char)9, " ");
            //html.Replace((char)13, "<br>");
            //html.Replace((char)39, "&#39;");
            //html.Replace((char)34, "&quot;");
            //html.Replace((char)32, "&nbsp;");

            return HttpContext.Current.Server.HtmlEncode(strHtml);

        }

        /// <summary>
        /// 对Html代码进行解码
        /// </summary>
        /// <param name="strHtml">要解码的字符串</param>
        /// <returns>解码后的字符串</returns>
        public static string ToHtml(string strHtml)
        {
            return HttpContext.Current.Server.HtmlDecode(strHtml);
        }


        /// <summary>
        /// 字符串编码
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static string HtmlEncode(string inputData)
        {
            return HttpUtility.HtmlEncode(inputData);
        }


        /// <summary>
        /// 设置Label显示Encode的字符串
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="txtInput"></param>
        public static void SetLabel(Label lbl, string txtInput)
        {
            lbl.Text = HtmlEncode(txtInput);
        }


        public static void SetLabel(Label lbl, object inputObj)
        {
            SetLabel(lbl, inputObj.ToString());
        }


        /// <summary>
        /// 字符串清理
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>

        public static string InputText(string inputString, int maxLength)
        {
            StringBuilder retVal = new StringBuilder();

            // 检查是否为空
            if ((inputString != null) && (inputString != String.Empty))
            {
                inputString = inputString.Trim();

                //检查长度
                if (inputString.Length > maxLength)
                    inputString = inputString.Substring(0, maxLength);

                //替换危险字符
                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '"':
                            retVal.Append("&quot;");
                            break;
                        case '<':
                            retVal.Append("&lt;");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(inputString[i]);
                            break;
                    }
                }
                retVal.Replace("'", " ");// 替换单引号
            }
            return retVal.ToString();

        }


        /// <summary>
        /// 转换成 HTML code
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string</returns>
        public static string Encode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }


        /// <summary>
        ///解析html成 普通文本
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string</returns>
        public static string Decode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }
        #endregion


        #region 按字节截取字符串

        /// <summary>
        /// 按字节截取字符串
        /// </summary>
        /// <param name="sInString">处理字符串</param>
        /// <param name="iCutLength">截取字节数</param>
        /// <returns>处理后字符串</returns>
        public static string CutString(string sInString, int iCutLength)
        {
            if (sInString == null || sInString.Length == 0 || iCutLength <= 0)
            {
                return "";
            }
            int iCount = System.Text.Encoding.Default.GetByteCount(sInString);
            if (iCount > iCutLength)
            {
                int iLength = 0;
                for (int i = 0; i < sInString.Length; i++)
                {
                    int iCharLength = System.Text.Encoding.Default.GetByteCount(new char[] { sInString[i] });
                    iLength += iCharLength;
                    if (iLength == iCutLength)
                    {
                        sInString = sInString.Substring(0, i + 1);
                        break;
                    }
                    else if (iLength > iCutLength)
                    {
                        sInString = sInString.Substring(0, i);
                        break;
                    }
                }
            }
            return sInString;
        }

        #endregion

        #region 返回给定字符串的字节数
        /// <summary>
        /// 输入字符串，返回字节数
        /// </summary>
        /// <param name="sInString">输入字符串</param>
        /// <returns></returns>
        public static int GetByteCount(string sInString)
        {
            if (sInString == null || sInString.Length == 0)
            {
                return 0;
            }

            return System.Text.Encoding.Default.GetByteCount(sInString);
        }
        #endregion

        #region 添加指定的分隔符，按字节
        /// <summary>
        /// 添加指定的分隔符，按字节。返加数据组
        /// </summary>
        /// <param name="strSource">源字符串</param>
        /// <param name="strSplit">指定的分隔符</param>
        /// <param name="byteLength">分隔长度</param>
        /// <returns></returns>
        public static string[] AddByte(string strSource, char strSplit, int byteLength)
        {
            if (string.IsNullOrEmpty(strSplit.ToString()))
            {
                strSplit = '|';
            }

            if (byteLength <= 0)
            {
                byteLength = 20;
            }

            string source = strSource;
            string sStr = "";
            int length = source.Length;//累加字节数

            int i = 0;
            //总字节数
            int iCount = System.Text.Encoding.Default.GetByteCount(source);
            int iLength = 0;

            for (int j = 0; j < length; j++)
            {
                int iCharLength = System.Text.Encoding.Default.GetByteCount(new char[] { source[j] });
                iLength += iCharLength;

                if (iLength % byteLength == 0)
                {
                    sStr += source.Substring(i, j - i + 1) + strSplit.ToString();
                    i = j + 1;
                    iLength = 0;
                }
                else
                {
                    if (iLength % byteLength == 1 && iLength != 1)
                    {
                        sStr += source.Substring(i, j - i) + strSplit.ToString();
                        i = j;
                        iLength = 0;
                    }
                }
            }
            sStr += source.Substring(i);
            return sStr.Split(strSplit);
        }
        #endregion

        #region 用户操作提示对话框
        /// <summary>
        /// 操作成功提示对话框
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="page"></param>
        public static void Alert(string msg, System.Web.UI.Page page, string handler)
        {
            String csname = "PopupScript";
            Type cstype = page.GetType();

            ClientScriptManager cs = page.ClientScript;

            StringBuilder csText = new StringBuilder();
            if (!cs.IsStartupScriptRegistered(cstype, csname))
            {
                //String cstext = "<script language='javascript'> ymPrompt.alert('http://www.qq.com',null,null,'确认要提交吗?',null);</script>";
                csText.Append("ymPrompt.alert('" + msg + "',null,null,'友情提示'," + handler + ");");

                cs.RegisterStartupScript(cstype, csname, csText.ToString(), true);
            }

        }

        /// <summary>
        ///  操作失败提示对话框
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="page"></param>
        public static void Error(string msg, System.Web.UI.Page page, string handler)
        {
            String csname = "PopupScript";
            Type cstype = page.GetType();

            ClientScriptManager cs = page.ClientScript;

            StringBuilder csText = new StringBuilder();

            if (!cs.IsStartupScriptRegistered(cstype, csname))
            {
                //String cstext = "<script language='javascript'> ymPrompt.alert('http://www.qq.com',null,null,'确认要提交吗?',null);</script>";
                csText.Append("ymPrompt.errorInfo('" + msg + "',null,null,'友情提示'," + handler + ");");

                cs.RegisterStartupScript(cstype, csname, csText.ToString(), true);
            }
        }

        /// <summary>
        /// 询问对话框图
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="page"></param>
        public static void Confirm(string msg, System.Web.UI.Page page, string handler)
        {
            String csname = "PopupScript";
            Type cstype = page.GetType();

            ClientScriptManager cs = page.ClientScript;

            StringBuilder csText = new StringBuilder();

            if (!cs.IsStartupScriptRegistered(cstype, csname))
            {
                //String cstext = "<script language='javascript'> ymPrompt.alert('http://www.qq.com',null,null,'确认要提交吗?',null);</script>";
                csText.Append("ymPrompt.confirmInfo('" + msg + "',null,null,'友情提示'," + handler + ");");

                cs.RegisterStartupScript(cstype, csname, csText.ToString(), true);
            }
        }


        /// <summary>
        /// 用户操作信息，重定向到用户操作信息页，提示操作信息
        /// </summary>
        public static void ShowMessage(string msgString, string flag)
        {
            StringBuilder url = new StringBuilder("Message.aspx?Msg=");
            url.Append(msgString);
            url.Append("&Flag=" + flag);
            System.Web.HttpContext.Current.Response.Redirect(url.ToString(), false);
        }

        #endregion


        #region 字节大小格式化 返回以 B,Kb,Mb为单位的字符串

        ///<summary>
        /// 计算字节大小函数，传入一以字节为单位的值。
        /// </summary>
        /// <param name="size">要计算的字节大小，单位为“字节”</param>
        /// <returns>计算后的大小值</returns>
        public static string FormateSize(long size)
        {
            const double baseKB = 1024.00, baseMB = 1024 * 1024.00;
            string strSize = "";
            if (size < baseKB)
            {
                strSize = size.ToString() + " B";
            }
            if (size > baseKB && size < baseMB)
            {
                strSize = string.Format("{0:0.##} KB", (size / baseKB));
            }
            else if (size > baseMB)
            {
                strSize = string.Format("{0:0.##} MB", (size / baseMB));
            }
            return strSize;
        }
        #endregion


        #region 计算一个文件夹的大小
        ///<summary>
        /// 计算一个文件夹的大小(递归实现)
        /// </summary>
        /// <param name="dirPath">文件夹的路径</param>
        /// <returns>返回一个文件夹的大小，单位是字节</returns>
        public static long GetDirectorySize(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                return 0;
            }

            long len = 0;
            DirectoryInfo di = new DirectoryInfo(dirPath);
            foreach (FileInfo fi in di.GetFiles())
            {
                len += fi.Length;
            }
            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                for (int i = 0; i < dis.Length; i++)
                {
                    len += GetDirectorySize(dis[i].FullName);
                }
            }
            return len;
        }
        #endregion


        #region 保存远程文件到本地(单个的文件地址)
        /// <summary>
        /// 保存远程文件到本地(单个的文件地址)
        /// </summary>
        /// <remarks>保存远程文件到本地(单个的文件地址)</remarks>
        /// <param name="strFilePath">远程文件地址</param>
        /// <returns>返回本地文件路径</returns>
        public static string SaveRemoteFile(string strFilePath)
        {
            string localFoldPath = "/UpLoadFile/RemoteFile/" + DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd");
            string extFileName = strFilePath.Substring(strFilePath.LastIndexOf("."));
            string fileName = DateTime.Now.ToString("yyyyMMddhhmmssfffff").Substring(0, 17) + extFileName;
            string localFileNamePath = localFoldPath + "/" + fileName;

            if (!Directory.Exists(HttpContext.Current.Server.MapPath(localFoldPath)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(localFoldPath));
            }
            WebClient wc = new WebClient();
            wc.DownloadFile(strFilePath, HttpContext.Current.Server.MapPath(localFileNamePath));

            return localFileNamePath;
        }

        #endregion


        #region 保存远程文件中的图片到本地

        /// <summary>
        /// 保存远程图片入口
        /// </summary>
        /// <param name="news_Content">从远程地址复制过来的内容</param>
        /// <returns>返回将远程图片地址替换成本地图片地址的内容</returns>
        public static string Pic_Remote(string news_Content)
        {
            string htmlStr = news_Content;
            string file = DateTime.Now.ToString("yyyyMM").Substring(0, 6);    //当前年月
            string day = DateTime.Now.ToString("dd"); //当天号数
            string path = "/UpLoadFile/RemoteFile/" + file + "/" + day;
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
            }
            string returnValue = "";
            returnValue = SaveUrlPics(htmlStr, path, file, day);
            return returnValue;
        }

        /// <summary>
        /// 下载图片到本地
        /// </summary>
        /// <param name="strHTML"></param>
        /// <param name="path"></param>
        /// <param name="nowyymm"></param>
        /// <param name="nowdd"></param>
        /// <param name="ddl"></param>
        /// <returns></returns>
        public static string SaveUrlPics(string strHTML, string path, string nowyymm, string nowdd)
        {
            string[] imgurlAry = GetImgTag(strHTML);
            try
            {
                for (int i = 0; i < imgurlAry.Length; i++)
                {
                    //WebRequest req = WebRequest.Create(imgurlAry[i]);
                    string fileName = DateTime.Now.ToString("yyyyMMddhhmmssfffff").Substring(0, 17);
                    WebClient wc = new WebClient();
                    if (imgurlAry[i] != "")
                    {
                        wc.DownloadFile(imgurlAry[i], HttpContext.Current.Server.MapPath(path) + "/" + fileName + imgurlAry[i].Substring(imgurlAry[i].LastIndexOf(".")));
                        //替换原图片地址
                        string imgPath = "/UpLoadFile/RemoteFile/" + nowyymm + "/" + nowdd;
                        //ddl.Items.Add(imgPath + "/" + fileName + imgurlAry[i].Substring(imgurlAry[i].LastIndexOf(".")));
                        //ddl.Items.Add(new ListItem(fileName + imgurlAry[i].Substring(imgurlAry[i].LastIndexOf(".")), imgPath + "/" + fileName + imgurlAry[i].Substring(imgurlAry[i].LastIndexOf("."))));

                        strHTML = strHTML.Replace(imgurlAry[i], imgPath + "/" + fileName + imgurlAry[i].Substring(imgurlAry[i].LastIndexOf(".")));
                    }
                }
            }
            catch
            {
                //return ex.Message;
            }
            return strHTML;
        }


        /// <summary>
        /// 获取包含图片标签(img)的图片集合
        /// </summary>
        /// <param name="htmlStr">给定内容</param>
        /// <returns>图片集合</returns>

        public static string[] GetImgTag(string htmlStr)
        {
            Regex regObj = new Regex("<img.+?>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string[] strAry = new string[regObj.Matches(htmlStr).Count];
            int i = 0;
            foreach (Match matchItem in regObj.Matches(htmlStr))
            {
                strAry[i] = GetImgUrl(matchItem.Value);
                i++;
            }
            return strAry;
        }


        /// <summary>
        /// 获取图片URL地址
        /// </summary>
        /// <param name="imgTagStr">给定字符串</param>
        /// <returns>返回图片的地址</returns>
        public static string GetImgUrl(string imgTagStr)
        {
            string str = "";
            Regex regObj = new Regex("http://.+.(?:jpg|gif|bmp|png)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match matchItem in regObj.Matches(imgTagStr))
            {
                str = matchItem.Value;
            }
            return str;
        }

        #endregion


        #region 图片等比例缩放
        /// <summary>
        /// 图片等比例缩放
        /// 缩略图存放目标采用相对路径
        /// </summary>
        /// <param name="srcPath">指定图片的来源(绝对路径)</param>
        /// <param name="destPath">指定缩略图存放目标(相对路径)</param>
        /// <param name="width">指定图片缩放后的宽度</param>
        /// <param name="height">指定图片缩放后的高度</param>
        /// <returns>返回缩略图的相对路径</returns>
        public static string ThumImage(string srcPath, string destPath, double width, double height)
        {
            System.Drawing.Image img = new Bitmap(srcPath);

            //生成图片大小必需小于原图
            if (img.Width < width)
            {
                width = img.Width;
            }
            if (img.Height < height)
            {
                height = img.Height;
            }


            //删除的高度，与宽度
            double cutWidth, cutHeight;
            cutWidth = (img.Width * height / img.Height - width); //宽度切割,高度缩放
            cutHeight = (img.Height * width / img.Width - height);//高度切割,宽度缩放
            byte flag = 0;//0 截高，1 截宽
            //这里的截宽是指缩略图的宽将被截，不是指原图，
            //1. 按原图比例，选择缩略图的高固定，计算出来的宽如果大于指定的宽，那么就按高固定，计算出来的宽来生成缩略图，再按给定大小截取
            //2. 按原图比例，选择缩略图的宽固定，计算出来的高如果大于指定的高，那么就按宽固定，计算出来的高来生成缩略图，再按给定大小截取
            //3. 因为长宽比只能取{1,>1,<1}三种情况
            flag = (byte)(cutHeight <= cutWidth ? 0 : 1);
            //System.Drawing.Image.GetThumbnailImageAbort myCallback=new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
            System.Drawing.Image thumImg;
            if (flag == 0)
            {
                thumImg = new Bitmap(img, (int)width, (int)height + (int)cutHeight); //img.GetThumbnailImage((int)width, (int)height + (int)cutHeight, myCallback, IntPtr.Zero);
            }
            else
            {
                thumImg = new Bitmap(img, (int)width + (int)cutWidth, (int)height);// img.GetThumbnailImage((int)width + (int)cutWidth, (int)height, myCallback, IntPtr.Zero);
            }

            System.Drawing.Bitmap destImg = new Bitmap((int)width, (int)height);
            Graphics g = Graphics.FromImage(destImg);

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //面板添充白色
            g.Clear(Color.White);

            //加边框线
            //g.DrawRectangle(new Pen(Color.DarkGray), new Rectangle(0, 0, (int)width, (int)height));

            Rectangle point = new Rectangle((int)((width - thumImg.Width) / 2), (int)((height - thumImg.Height) / 2), thumImg.Width, thumImg.Height);
            Rectangle rect = new Rectangle(0, 0, thumImg.Width, thumImg.Height);
            g.DrawImage(thumImg, point, rect, GraphicsUnit.Pixel);

            //在指定的位置画图片边框线
            //g.DrawRectangle(new Pen(Color.Blue), point);
            g.Save();
            //destImg.Save(destPath, GetFormat(destPath));

            //保存图片
            destImg.Save(HttpContext.Current.Server.MapPath(destPath), System.Drawing.Imaging.ImageFormat.Jpeg);

            thumImg.Dispose();
            img.Dispose();
            destImg.Dispose();

            //string thumPicPath = destPath;
            return destPath;
            //return true;
        }

        ///<summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径，含文件名）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径，含文件名）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="height">返回缩略图路径</param>
        public static void ThumImage(string originalImagePath, string thumbnailPath, int width, int height, out string outthumbnailPath)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0; //缩略图在画布上的X放向起始点
            int y = 0; //缩略图在画布上的Y放向起始点
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            int dw = 0;
            int dh = 0;

            if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
            {
                //宽比高大，以宽为准
                dw = originalImage.Width * towidth / originalImage.Width;
                dh = originalImage.Height * toheight / originalImage.Width;
                x = 0;
                y = (toheight - dh) / 2;
            }
            else
            {
                //高比宽大，以高为准
                dw = originalImage.Width * towidth / originalImage.Height;
                dh = originalImage.Height * toheight / originalImage.Height;
                x = (towidth - dw) / 2;
                y = 0;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以白色背景色填充
            g.Clear(Color.White);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(x, y, dw, dh),
             new Rectangle(0, 0, ow, oh),
             GraphicsUnit.Pixel);

            try
            {
                //以Jpeg格式保存缩略图(KB最小)
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                outthumbnailPath = thumbnailPath;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }


        #endregion

        #region 取得当前时间的二制进数
        public static string GetDateBit()
        {
            string moth = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string hour = DateTime.Now.ToString("hh");
            string minute = DateTime.Now.ToString("mm");
            string second = DateTime.Now.ToString("ss");

            StringBuilder bit = new StringBuilder();
            bit.Append(Convert.ToString(int.Parse(moth), 2).PadLeft(4, '0'));
            bit.Append(Convert.ToString(int.Parse(day), 2).PadLeft(5, '0'));
            bit.Append(Convert.ToString(int.Parse(hour), 2).PadLeft(5, '0'));
            bit.Append(Convert.ToString(int.Parse(minute), 2).PadLeft(5, '0'));
            bit.Append(Convert.ToString(int.Parse(second), 2).PadLeft(6, '0'));

            return bit.ToString();
        }
        #endregion

        #region /// 过滤html,js,css代码
        /// <summary>
        /// 过滤html,js,css代码
        /// </summary>
        /// <param name="html">参数传入</param>
        /// <returns></returns>
        public static string ReplaceHtml(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            html = regex6.Replace(html, ""); //过滤frameset
            html = regex7.Replace(html, ""); //过滤frameset
            html = regex8.Replace(html, ""); //过滤frameset
            html = regex9.Replace(html, "");
            html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        }
        #endregion

        #region 获取客户端IP地址
        public static string GetIpAddress()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            return result;
        }
        #endregion

        public static string MD5(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").Trim();
        }

        #region SQL注入过滤
        /// <summary> 
        ///SQL注入过滤
        /// </summary> 
        /// <param name="InText">要过滤的字符串 </param> 
        /// <returns>如果参数存在不安全字符，则返回true </returns> 
        public static bool SqlFilter(string InText)
        {
            string word = "and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join|cmd";
            if (InText == null)
                return false;
            foreach (string i in word.Split('|'))
            {
                if ((InText.ToLower().IndexOf(i + " ") > -1) || (InText.ToLower().IndexOf(" " + i) > -1))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 将json格式的参数转化成Dictionary类型
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ConvertToDictionary(string post)
        {
            post = post.Replace("\"", "'").Replace("\\", "\\\\");
            JObject jo = (JObject)JsonConvert.DeserializeObject(post);
            Dictionary<string, object> parList = new Dictionary<string, object>();
            foreach (var item in jo)
            {
                parList.Add(item.Key, item.Value.ToString().Replace("\"", ""));
            }
            return parList;
        }

        /// <summary>
        /// post提交
        /// </summary>
        /// <param name="url">http://www.qiandabao.com</param>
        /// <param name="postData">a=1&b=2</param>
        /// <returns></returns>
        private string PostData(string url, string postData)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);

            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();

            newStream.Write(data, 0, data.Length);
            newStream.Close();

            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.Default);
            string content = reader.ReadToEnd();
            reader.Close();
            return content;
        }

        /// <summary>
        /// get提交数据
        /// </summary>
        /// <param name="url">http://www.qiandabao.com</param>
        /// <returns></returns>
        private string GetData(string url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();
            reader.Close();
            return content;
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp(DateTime dt)
        {
            TimeSpan ts = dt - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 创建图片水印
        /// </summary>
        /// <param name="rSrcImgPath">原始图片的物理路径</param>    
        /// <param name="rMarkImgPath">水印图片的物理路径</param>    
        /// <param name="rMarkText">水印文字（不显示水印文字设为空串）</param>    
        /// <param name="rDstImgPath">输出合成后的图片的物理路径</param>    
        public void BuildWatermark(string rSrcImgPath, string rMarkImgPath, string rMarkText, string rDstImgPath)
        {
            //以下（代码）从一个指定文件创建了一个Image 对象，然后为它的 Width 和 Height定义变量。    
            //这些长度待会被用来建立一个以24 bits 每像素的格式作为颜色数据的Bitmap对象。    
            System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(rSrcImgPath);
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(72, 72);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //这个代码载入水印图片，水印图片已经被保存为一个BMP文件，以绿色(A=0,R=0,G=255,B=0)作为背景颜色。    
            //再一次，会为它的Width 和Height定义一个变量。    
            System.Drawing.Image imgWatermark = new Bitmap(rMarkImgPath);
            int wmWidth = imgWatermark.Width;
            int wmHeight = imgWatermark.Height;

            //这个代码以100%它的原始大小绘制imgPhoto 到Graphics 对象的（x=0,y=0）位置。    
            //以后所有的绘图都将发生在原来照片的顶部。    
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, phWidth, phHeight), 0, 0, phWidth, phHeight, GraphicsUnit.Pixel);

            //为了最大化版权信息的大小，我们将测试7种不同的字体大小来决定我们能为我们的照片宽度使用的可能的最大大小。    
            //为了有效地完成这个，我们将定义一个整型数组，接着遍历这些整型值测量不同大小的版权字符串。    
            //一旦我们决定了可能的最大大小，我们就退出循环，绘制文本    
            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();
            for (int i = 0; i < 7; i++)
            {
                crFont = new Font("arial", sizes[i], FontStyle.Bold);
                crSize = grPhoto.MeasureString(rMarkText, crFont);
                if ((ushort)crSize.Width < (ushort)phWidth)
                    break;
            }

            //因为所有的照片都有各种各样的高度，所以就决定了从图象底部开始的5%的位置开始。    
            //使用rMarkText字符串的高度来决定绘制字符串合适的Y坐标轴。    
            //通过计算图像的中心来决定X轴，然后定义一个StringFormat 对象，设置StringAlignment 为Center。    
            int yPixlesFromBottom = (int)(phHeight * .05);
            float yPosFromBottom = ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));
            float xCenterOfImg = (phWidth / 2);
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            //现在我们已经有了所有所需的位置坐标来使用60%黑色的一个Color(alpha值153)创建一个SolidBrush 。    
            //在偏离右边1像素，底部1像素的合适位置绘制版权字符串。    
            //这段偏离将用来创建阴影效果。使用Brush重复这样一个过程，在前一个绘制的文本顶部绘制同样的文本。    
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));
            grPhoto.DrawString(rMarkText, crFont, semiTransBrush2, new PointF(xCenterOfImg + 1, yPosFromBottom + 1), StrFormat);

            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
            grPhoto.DrawString(rMarkText, crFont, semiTransBrush, new PointF(xCenterOfImg, yPosFromBottom), StrFormat);

            //根据前面修改后的照片创建一个Bitmap。把这个Bitmap载入到一个新的Graphic对象。    
            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            Graphics grWatermark = Graphics.FromImage(bmWatermark);

            //通过定义一个ImageAttributes 对象并设置它的两个属性，我们就是实现了两个颜色的处理，以达到半透明的水印效果。    
            //处理水印图象的第一步是把背景图案变为透明的(Alpha=0, R=0, G=0, B=0)。我们使用一个Colormap 和定义一个RemapTable来做这个。    
            //就像前面展示的，我的水印被定义为100%绿色背景，我们将搜到这个颜色，然后取代为透明。    
            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };

            //第二个颜色处理用来改变水印的不透明性。    
            //通过应用包含提供了坐标的RGBA空间的5x5矩阵来做这个。    
            //通过设定第三行、第三列为0.3f我们就达到了一个不透明的水平。结果是水印会轻微地显示在图象底下一些。    
            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
            float[][] colorMatrixElements = {     
                                             new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},    
                                             new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},    
                                             new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},    
                                             new float[] {0.0f,  0.0f,  0.0f,  0.3f, 0.0f},    
                                             new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}    
                                        };
            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
            imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            //随着两个颜色处理加入到imageAttributes 对象，我们现在就能在照片右手边上绘制水印了。    
            //我们会偏离10像素到底部，10像素到左边。    
            int markWidth;
            int markHeight;

            //mark比原来的图宽    
            if (phWidth <= wmWidth)
            {
                markWidth = phWidth - 10;
                markHeight = (markWidth * wmHeight) / wmWidth;
            }
            else if (phHeight <= wmHeight)
            {
                markHeight = phHeight - 10;
                markWidth = (markHeight * wmWidth) / wmHeight;
            }
            else
            {
                markWidth = wmWidth;
                markHeight = wmHeight;
            }
            int xPosOfWm = ((phWidth - markWidth) - 10);
            int yPosOfWm = 10;
            grWatermark.DrawImage(imgWatermark, new Rectangle(xPosOfWm, yPosOfWm, markWidth, markHeight), 0, 0, wmWidth, wmHeight, GraphicsUnit.Pixel, imageAttributes);

            //最后的步骤将是使用新的Bitmap取代原来的Image。 销毁两个Graphic对象，然后把Image 保存到文件系统。    
            imgPhoto = bmWatermark;
            grPhoto.Dispose();
            grWatermark.Dispose();
            imgPhoto.Save(rDstImgPath, ImageFormat.Jpeg);
            imgPhoto.Dispose();
            imgWatermark.Dispose();
        }

    }
}
