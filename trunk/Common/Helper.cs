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
    /// ���ø���������
    /// </summary>
    public class Helper
    {
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //�ȼ���^[+-]?\d+[.]?\d+$
        private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w Ӣ����ĸ�����ֵ��ַ������� [a-zA-Z0-9] �﷨һ�� 
        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");


        #region �Զ������ļ�¼ID��
        /// <summary>
        /// �Զ������ļ�¼ID��
        /// </summary>
        public static string Id
        {
            get
            {
                return "ID" + DateTime.Now.ToString("yyMMddhhmmss") + DateTime.Now.Millisecond;
            }
        }
        #endregion


        #region �Զ������ļ�¼ID��
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


        //#region Jmail��������ʼ�
        ////
        ///// <summary>
        ///// �����ʼ�
        ///// </summary>
        ///// <param name="Subject">�ʼ�����</param>
        ///// <param name="body">�ʼ����ļ�����</param>
        ///// <param name="FromEmail">������</param>
        ///// <param name="ToEmail">�ռ���</param>
        ///// <param name="UserName">�����������˺�</param>
        ///// <param name="Password">��������������</param>
        //public static void JmailSendMail(string Subject, string Body, string FromEmail, string ToEmail, string UserName, string Password)
        //{
        //    myJmail.Message Jmail = new myJmail.Message();
        //    DateTime t = DateTime.Now;
        //    //String AddAttachment = this.FileUploadSubject.PostedFile.FileName;
        //    //Silent���ԣ��������Ϊtrue,JMail�����׳��������. JMail. Send( () ����ݲ����������true��false
        //    Jmail.Silent = true;
        //    //Jmail��������־��ǰ��loging��������Ϊtrue
        //    Jmail.Logging = true;
        //    //�ַ�����ȱʡΪ"US-ASCII"
        //    Jmail.Charset = "GB2312";
        //    //�ż���contentype. ȱʡ��"text/plain"�� : �ַ����������HTML��ʽ�����ʼ�, ��Ϊ"text/html"���ɡ�
        //    Jmail.ContentType = "text/html";
        //    //����ռ���
        //    Jmail.AddRecipient(ToEmail, "", "");
        //    Jmail.From = FromEmail;
        //    //�������ʼ��û���
        //    Jmail.MailServerUserName = UserName;
        //    //�������ʼ�����
        //    Jmail.MailServerPassWord = Password;
        //    //�����ʼ�����
        //    Jmail.Subject = Subject;
        //    //�ʼ���Ӹ���,(�฽���Ļ��������ټ�һ��Jmail.AddAttachment( "c:\\test.jpg",true,null);)�Ϳ��Ը㶨�ˡ���ע�ݣ����˸��������������Jmail.ContentType="text/html";ɾ������������ʼ���������롣
        //    //Jmail.AddAttachment(AddAttachment, true, null);
        //    //�ʼ�����
        //    Jmail.Body = Body;
        //    //Jmail���͵ķ���
        //    Jmail.Send("smtp.163.com", false);
        //    Jmail.Close();

        //}
        //#endregion


        #region System.Net.Mail�����ʼ�
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="Subject">�ʼ�����</param>
        /// <param name="body">�ʼ����ļ�����</param>
        /// <param name="FromEmail">������</param>
        /// <param name="ToEmail">�ռ���</param>
        /// <param name="UserName">�����������˺�</param>
        /// <param name="Password">��������������</param>
        public static void NetSendMail(string Subject, string Body, string FromEmail, string ToEmail, string UserName, string Password)
        {
            SmtpClient ObjSmtpClient = new SmtpClient();
            ObjSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;       //ָ�������ʼ����ͷ�ʽ
            ObjSmtpClient.Host = "smtp.163.com";                                 //ָ��SMTP������
            ObjSmtpClient.Credentials = new System.Net.NetworkCredential(FromEmail, Password);   //�û���������

            MailMessage ObjMailMessage = new MailMessage(FromEmail, ToEmail);
            ObjMailMessage.Subject = Subject;           //�����ʼ�����
            ObjMailMessage.Body = Body;                 //�����ʼ�����
            ObjMailMessage.BodyEncoding = System.Text.Encoding.Default;    //�����ʼ����ı���
            ObjMailMessage.IsBodyHtml = true;              //����ΪHTML��ʽ
            ObjMailMessage.Priority = MailPriority.High;   //���ȼ�
            //if (!string.IsNullOrEmpty(StrFileName)) ObjMailMessage.Attachments.Add(new Attachment(StrFileName));  //�ʼ��ĸ���
            ObjSmtpClient.Send(ObjMailMessage);
        }
        #endregion


        #region ������Ա�Ƿ��½
        /// <summary>
        /// ������Ա�Ƿ��½
        /// ���ڿͻ��˵Ĺ���Ա��½��ʶΪIsLogin
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


        #region ���Request��ѯ�ַ����ļ�ֵ�Ƿ�������

        /// <summary>
        /// ���Request��ѯ�ַ����ļ�ֵ���Ƿ������֣���󳤶�����
        /// </summary>
        /// <param name="req">Request</param>
        /// <param name="inputKey">Request�ļ�ֵ</param>
        /// <param name="maxLen">��󳤶�</param>
        /// <returns>����Request��ѯ�ַ���</returns>
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


        #region �Ƿ������ַ���
        /// <summary>
        /// �Ƿ������ַ���
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            Match m = RegNumber.Match(inputData);
            return m.Success;
        }
        #endregion


        #region �Ƿ������ַ��� �ɴ�������

        /// <summary>
        /// �Ƿ������ַ��� �ɴ�������
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            Match m = RegNumberSign.Match(inputData);
            return m.Success;
        }

        #endregion


        #region �Ƿ��Ǹ�����
        /// <summary>
        /// �Ƿ��Ǹ�����
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            Match m = RegDecimal.Match(inputData);
            return m.Success;
        }
        #endregion


        #region �Ƿ��Ǹ����� �ɴ�������
        /// <summary>
        /// �Ƿ��Ǹ����� �ɴ�������
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            Match m = RegDecimalSign.Match(inputData);
            return m.Success;
        }

        #endregion


        #region ���ļ��

        /// <summary>
        /// ����Ƿ��������ַ�
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsHasCHZN(string inputData)
        {
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }

        #endregion


        #region �ʼ���ַ
        /// <summary>
        /// �Ƿ��Ǹ����� �ɴ�������
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsEmail(string inputData)
        {
            Match m = RegEmail.Match(inputData);
            return m.Success;
        }

        #endregion


        #region ����ַ�����󳤶ȣ�����ָ�����ȵĴ�

        /// <summary>
        /// ����ַ�����󳤶ȣ�����ָ�����ȵĴ�
        /// </summary>
        /// <param name="sqlInput">�����ַ���</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <returns></returns>			
        public static string SqlText(string sqlInput, int maxLength)
        {
            if (sqlInput != null && sqlInput != string.Empty)
            {
                sqlInput = sqlInput.Trim();
                if (sqlInput.Length > maxLength)//����󳤶Ƚ�ȡ�ַ���
                    sqlInput = sqlInput.Substring(0, maxLength);
            }
            return sqlInput;
        }

        #endregion


        #region ���ַ����ı���ת������
        /// <summary>
        /// ��Html������б���
        /// </summary>
        /// <param name="strHtml">Ҫת�����ַ���</param>
        /// <returns>�������ַ���</returns>
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
        /// ��Html������н���
        /// </summary>
        /// <param name="strHtml">Ҫ������ַ���</param>
        /// <returns>�������ַ���</returns>
        public static string ToHtml(string strHtml)
        {
            return HttpContext.Current.Server.HtmlDecode(strHtml);
        }


        /// <summary>
        /// �ַ�������
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static string HtmlEncode(string inputData)
        {
            return HttpUtility.HtmlEncode(inputData);
        }


        /// <summary>
        /// ����Label��ʾEncode���ַ���
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
        /// �ַ�������
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>

        public static string InputText(string inputString, int maxLength)
        {
            StringBuilder retVal = new StringBuilder();

            // ����Ƿ�Ϊ��
            if ((inputString != null) && (inputString != String.Empty))
            {
                inputString = inputString.Trim();

                //��鳤��
                if (inputString.Length > maxLength)
                    inputString = inputString.Substring(0, maxLength);

                //�滻Σ���ַ�
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
                retVal.Replace("'", " ");// �滻������
            }
            return retVal.ToString();

        }


        /// <summary>
        /// ת���� HTML code
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
        ///����html�� ��ͨ�ı�
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


        #region ���ֽڽ�ȡ�ַ���

        /// <summary>
        /// ���ֽڽ�ȡ�ַ���
        /// </summary>
        /// <param name="sInString">�����ַ���</param>
        /// <param name="iCutLength">��ȡ�ֽ���</param>
        /// <returns>������ַ���</returns>
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

        #region ���ظ����ַ������ֽ���
        /// <summary>
        /// �����ַ����������ֽ���
        /// </summary>
        /// <param name="sInString">�����ַ���</param>
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

        #region ���ָ���ķָ��������ֽ�
        /// <summary>
        /// ���ָ���ķָ��������ֽڡ�����������
        /// </summary>
        /// <param name="strSource">Դ�ַ���</param>
        /// <param name="strSplit">ָ���ķָ���</param>
        /// <param name="byteLength">�ָ�����</param>
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
            int length = source.Length;//�ۼ��ֽ���

            int i = 0;
            //���ֽ���
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

        #region �û�������ʾ�Ի���
        /// <summary>
        /// �����ɹ���ʾ�Ի���
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
                //String cstext = "<script language='javascript'> ymPrompt.alert('http://www.qq.com',null,null,'ȷ��Ҫ�ύ��?',null);</script>";
                csText.Append("ymPrompt.alert('" + msg + "',null,null,'������ʾ'," + handler + ");");

                cs.RegisterStartupScript(cstype, csname, csText.ToString(), true);
            }

        }

        /// <summary>
        ///  ����ʧ����ʾ�Ի���
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
                //String cstext = "<script language='javascript'> ymPrompt.alert('http://www.qq.com',null,null,'ȷ��Ҫ�ύ��?',null);</script>";
                csText.Append("ymPrompt.errorInfo('" + msg + "',null,null,'������ʾ'," + handler + ");");

                cs.RegisterStartupScript(cstype, csname, csText.ToString(), true);
            }
        }

        /// <summary>
        /// ѯ�ʶԻ���ͼ
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
                //String cstext = "<script language='javascript'> ymPrompt.alert('http://www.qq.com',null,null,'ȷ��Ҫ�ύ��?',null);</script>";
                csText.Append("ymPrompt.confirmInfo('" + msg + "',null,null,'������ʾ'," + handler + ");");

                cs.RegisterStartupScript(cstype, csname, csText.ToString(), true);
            }
        }


        /// <summary>
        /// �û�������Ϣ���ض����û�������Ϣҳ����ʾ������Ϣ
        /// </summary>
        public static void ShowMessage(string msgString, string flag)
        {
            StringBuilder url = new StringBuilder("Message.aspx?Msg=");
            url.Append(msgString);
            url.Append("&Flag=" + flag);
            System.Web.HttpContext.Current.Response.Redirect(url.ToString(), false);
        }

        #endregion


        #region �ֽڴ�С��ʽ�� ������ B,Kb,MbΪ��λ���ַ���

        ///<summary>
        /// �����ֽڴ�С����������һ���ֽ�Ϊ��λ��ֵ��
        /// </summary>
        /// <param name="size">Ҫ������ֽڴ�С����λΪ���ֽڡ�</param>
        /// <returns>�����Ĵ�Сֵ</returns>
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


        #region ����һ���ļ��еĴ�С
        ///<summary>
        /// ����һ���ļ��еĴ�С(�ݹ�ʵ��)
        /// </summary>
        /// <param name="dirPath">�ļ��е�·��</param>
        /// <returns>����һ���ļ��еĴ�С����λ���ֽ�</returns>
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


        #region ����Զ���ļ�������(�������ļ���ַ)
        /// <summary>
        /// ����Զ���ļ�������(�������ļ���ַ)
        /// </summary>
        /// <remarks>����Զ���ļ�������(�������ļ���ַ)</remarks>
        /// <param name="strFilePath">Զ���ļ���ַ</param>
        /// <returns>���ر����ļ�·��</returns>
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


        #region ����Զ���ļ��е�ͼƬ������

        /// <summary>
        /// ����Զ��ͼƬ���
        /// </summary>
        /// <param name="news_Content">��Զ�̵�ַ���ƹ���������</param>
        /// <returns>���ؽ�Զ��ͼƬ��ַ�滻�ɱ���ͼƬ��ַ������</returns>
        public static string Pic_Remote(string news_Content)
        {
            string htmlStr = news_Content;
            string file = DateTime.Now.ToString("yyyyMM").Substring(0, 6);    //��ǰ����
            string day = DateTime.Now.ToString("dd"); //�������
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
        /// ����ͼƬ������
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
                        //�滻ԭͼƬ��ַ
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
        /// ��ȡ����ͼƬ��ǩ(img)��ͼƬ����
        /// </summary>
        /// <param name="htmlStr">��������</param>
        /// <returns>ͼƬ����</returns>

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
        /// ��ȡͼƬURL��ַ
        /// </summary>
        /// <param name="imgTagStr">�����ַ���</param>
        /// <returns>����ͼƬ�ĵ�ַ</returns>
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


        #region ͼƬ�ȱ�������
        /// <summary>
        /// ͼƬ�ȱ�������
        /// ����ͼ���Ŀ��������·��
        /// </summary>
        /// <param name="srcPath">ָ��ͼƬ����Դ(����·��)</param>
        /// <param name="destPath">ָ������ͼ���Ŀ��(���·��)</param>
        /// <param name="width">ָ��ͼƬ���ź�Ŀ��</param>
        /// <param name="height">ָ��ͼƬ���ź�ĸ߶�</param>
        /// <returns>��������ͼ�����·��</returns>
        public static string ThumImage(string srcPath, string destPath, double width, double height)
        {
            System.Drawing.Image img = new Bitmap(srcPath);

            //����ͼƬ��С����С��ԭͼ
            if (img.Width < width)
            {
                width = img.Width;
            }
            if (img.Height < height)
            {
                height = img.Height;
            }


            //ɾ���ĸ߶ȣ�����
            double cutWidth, cutHeight;
            cutWidth = (img.Width * height / img.Height - width); //����и�,�߶�����
            cutHeight = (img.Height * width / img.Width - height);//�߶��и�,�������
            byte flag = 0;//0 �ظߣ�1 �ؿ�
            //����Ľؿ���ָ����ͼ�Ŀ����أ�����ָԭͼ��
            //1. ��ԭͼ������ѡ������ͼ�ĸ߹̶�����������Ŀ��������ָ���Ŀ���ô�Ͱ��߹̶�����������Ŀ�����������ͼ���ٰ�������С��ȡ
            //2. ��ԭͼ������ѡ������ͼ�Ŀ�̶�����������ĸ��������ָ���ĸߣ���ô�Ͱ���̶�����������ĸ�����������ͼ���ٰ�������С��ȡ
            //3. ��Ϊ�����ֻ��ȡ{1,>1,<1}�������
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

            //���ø�����,���ٶȳ���ƽ���̶�
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //���ø�������ֵ��
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //�������ɫ
            g.Clear(Color.White);

            //�ӱ߿���
            //g.DrawRectangle(new Pen(Color.DarkGray), new Rectangle(0, 0, (int)width, (int)height));

            Rectangle point = new Rectangle((int)((width - thumImg.Width) / 2), (int)((height - thumImg.Height) / 2), thumImg.Width, thumImg.Height);
            Rectangle rect = new Rectangle(0, 0, thumImg.Width, thumImg.Height);
            g.DrawImage(thumImg, point, rect, GraphicsUnit.Pixel);

            //��ָ����λ�û�ͼƬ�߿���
            //g.DrawRectangle(new Pen(Color.Blue), point);
            g.Save();
            //destImg.Save(destPath, GetFormat(destPath));

            //����ͼƬ
            destImg.Save(HttpContext.Current.Server.MapPath(destPath), System.Drawing.Imaging.ImageFormat.Jpeg);

            thumImg.Dispose();
            img.Dispose();
            destImg.Dispose();

            //string thumPicPath = destPath;
            return destPath;
            //return true;
        }

        ///<summary>
        /// ��������ͼ
        /// </summary>
        /// <param name="originalImagePath">Դͼ·��������·�������ļ�����</param>
        /// <param name="thumbnailPath">����ͼ·��������·�������ļ�����</param>
        /// <param name="width">����ͼ���</param>
        /// <param name="height">����ͼ�߶�</param>
        /// <param name="height">��������ͼ·��</param>
        public static void ThumImage(string originalImagePath, string thumbnailPath, int width, int height, out string outthumbnailPath)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0; //����ͼ�ڻ����ϵ�X������ʼ��
            int y = 0; //����ͼ�ڻ����ϵ�Y������ʼ��
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            int dw = 0;
            int dh = 0;

            if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
            {
                //��ȸߴ��Կ�Ϊ׼
                dw = originalImage.Width * towidth / originalImage.Width;
                dh = originalImage.Height * toheight / originalImage.Width;
                x = 0;
                y = (toheight - dh) / 2;
            }
            else
            {
                //�߱ȿ���Ը�Ϊ׼
                dw = originalImage.Width * towidth / originalImage.Height;
                dh = originalImage.Height * toheight / originalImage.Height;
                x = (towidth - dw) / 2;
                y = 0;
            }

            //�½�һ��bmpͼƬ
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //�½�һ������
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //���ø�������ֵ��
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //���ø�����,���ٶȳ���ƽ���̶�
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //��ջ������԰�ɫ����ɫ���
            g.Clear(Color.White);

            //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������
            g.DrawImage(originalImage, new Rectangle(x, y, dw, dh),
             new Rectangle(0, 0, ow, oh),
             GraphicsUnit.Pixel);

            try
            {
                //��Jpeg��ʽ��������ͼ(KB��С)
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

        #region ȡ�õ�ǰʱ��Ķ��ƽ���
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

        #region /// ����html,js,css����
        /// <summary>
        /// ����html,js,css����
        /// </summary>
        /// <param name="html">��������</param>
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
            html = regex1.Replace(html, ""); //����<script></script>���
            html = regex2.Replace(html, ""); //����href=javascript: (<A>) ����
            html = regex3.Replace(html, " _disibledevent="); //���������ؼ���on...�¼�
            html = regex4.Replace(html, ""); //����iframe
            html = regex5.Replace(html, ""); //����frameset
            html = regex6.Replace(html, ""); //����frameset
            html = regex7.Replace(html, ""); //����frameset
            html = regex8.Replace(html, ""); //����frameset
            html = regex9.Replace(html, "");
            html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        }
        #endregion

        #region ��ȡ�ͻ���IP��ַ
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

        #region SQLע�����
        /// <summary> 
        ///SQLע�����
        /// </summary> 
        /// <param name="InText">Ҫ���˵��ַ��� </param> 
        /// <returns>����������ڲ���ȫ�ַ����򷵻�true </returns> 
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
        /// ��json��ʽ�Ĳ���ת����Dictionary����
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
        /// post�ύ
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
        /// get�ύ����
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
        /// ��ȡʱ���
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// ��ȡʱ���
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp(DateTime dt)
        {
            TimeSpan ts = dt - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// ����ͼƬˮӡ
        /// </summary>
        /// <param name="rSrcImgPath">ԭʼͼƬ������·��</param>    
        /// <param name="rMarkImgPath">ˮӡͼƬ������·��</param>    
        /// <param name="rMarkText">ˮӡ���֣�����ʾˮӡ������Ϊ�մ���</param>    
        /// <param name="rDstImgPath">����ϳɺ��ͼƬ������·��</param>    
        public void BuildWatermark(string rSrcImgPath, string rMarkImgPath, string rMarkText, string rDstImgPath)
        {
            //���£����룩��һ��ָ���ļ�������һ��Image ����Ȼ��Ϊ���� Width �� Height���������    
            //��Щ���ȴ��ᱻ��������һ����24 bits ÿ���صĸ�ʽ��Ϊ��ɫ���ݵ�Bitmap����    
            System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(rSrcImgPath);
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(72, 72);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //�����������ˮӡͼƬ��ˮӡͼƬ�Ѿ�������Ϊһ��BMP�ļ�������ɫ(A=0,R=0,G=255,B=0)��Ϊ������ɫ��    
            //��һ�Σ���Ϊ����Width ��Height����һ��������    
            System.Drawing.Image imgWatermark = new Bitmap(rMarkImgPath);
            int wmWidth = imgWatermark.Width;
            int wmHeight = imgWatermark.Height;

            //���������100%����ԭʼ��С����imgPhoto ��Graphics ����ģ�x=0,y=0��λ�á�    
            //�Ժ����еĻ�ͼ����������ԭ����Ƭ�Ķ�����    
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, phWidth, phHeight), 0, 0, phWidth, phHeight, GraphicsUnit.Pixel);

            //Ϊ����󻯰�Ȩ��Ϣ�Ĵ�С�����ǽ�����7�ֲ�ͬ�������С������������Ϊ���ǵ���Ƭ���ʹ�õĿ��ܵ�����С��    
            //Ϊ����Ч�������������ǽ�����һ���������飬���ű�����Щ����ֵ������ͬ��С�İ�Ȩ�ַ�����    
            //һ�����Ǿ����˿��ܵ�����С�����Ǿ��˳�ѭ���������ı�    
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

            //��Ϊ���е���Ƭ���и��ָ����ĸ߶ȣ����Ծ;����˴�ͼ��ײ���ʼ��5%��λ�ÿ�ʼ��    
            //ʹ��rMarkText�ַ����ĸ߶������������ַ������ʵ�Y�����ᡣ    
            //ͨ������ͼ�������������X�ᣬȻ����һ��StringFormat ��������StringAlignment ΪCenter��    
            int yPixlesFromBottom = (int)(phHeight * .05);
            float yPosFromBottom = ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));
            float xCenterOfImg = (phWidth / 2);
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            //���������Ѿ��������������λ��������ʹ��60%��ɫ��һ��Color(alphaֵ153)����һ��SolidBrush ��    
            //��ƫ���ұ�1���أ��ײ�1���صĺ���λ�û��ư�Ȩ�ַ�����    
            //���ƫ�뽫����������ӰЧ����ʹ��Brush�ظ�����һ�����̣���ǰһ�����Ƶ��ı���������ͬ�����ı���    
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));
            grPhoto.DrawString(rMarkText, crFont, semiTransBrush2, new PointF(xCenterOfImg + 1, yPosFromBottom + 1), StrFormat);

            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
            grPhoto.DrawString(rMarkText, crFont, semiTransBrush, new PointF(xCenterOfImg, yPosFromBottom), StrFormat);

            //����ǰ���޸ĺ����Ƭ����һ��Bitmap�������Bitmap���뵽һ���µ�Graphic����    
            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            Graphics grWatermark = Graphics.FromImage(bmWatermark);

            //ͨ������һ��ImageAttributes �������������������ԣ����Ǿ���ʵ����������ɫ�Ĵ����Դﵽ��͸����ˮӡЧ����    
            //����ˮӡͼ��ĵ�һ���ǰѱ���ͼ����Ϊ͸����(Alpha=0, R=0, G=0, B=0)������ʹ��һ��Colormap �Ͷ���һ��RemapTable���������    
            //����ǰ��չʾ�ģ��ҵ�ˮӡ������Ϊ100%��ɫ���������ǽ��ѵ������ɫ��Ȼ��ȡ��Ϊ͸����    
            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };

            //�ڶ�����ɫ���������ı�ˮӡ�Ĳ�͸���ԡ�    
            //ͨ��Ӧ�ð����ṩ�������RGBA�ռ��5x5�������������    
            //ͨ���趨�����С�������Ϊ0.3f���Ǿʹﵽ��һ����͸����ˮƽ�������ˮӡ����΢����ʾ��ͼ�����һЩ��    
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

            //����������ɫ������뵽imageAttributes �����������ھ�������Ƭ���ֱ��ϻ���ˮӡ�ˡ�    
            //���ǻ�ƫ��10���ص��ײ���10���ص���ߡ�    
            int markWidth;
            int markHeight;

            //mark��ԭ����ͼ��    
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

            //���Ĳ��轫��ʹ���µ�Bitmapȡ��ԭ����Image�� ��������Graphic����Ȼ���Image ���浽�ļ�ϵͳ��    
            imgPhoto = bmWatermark;
            grPhoto.Dispose();
            grWatermark.Dispose();
            imgPhoto.Save(rDstImgPath, ImageFormat.Jpeg);
            imgPhoto.Dispose();
            imgWatermark.Dispose();
        }

    }
}
