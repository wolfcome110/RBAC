using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DotNetZipHelper
    {
        public static void Zip(string[] files)
        {
            using (ZipFile zip = new ZipFile(System.Text.Encoding.UTF8))
            {
                ////把这个PNG文件添加到zip档案的"images"目录下
                //zip.AddFile(@"D:\Work\MES.Wetown\新建 Microsoft Excel 工作表.xlsx", "images");
                ////然后把PDF文件添加到zip档案的"files"目录下，把ReadMe.txt放到根目录
                //zip.AddFile(@"D:\Work\MES.Wetown\新建 Microsoft Word 文档.docx", "files");
                //zip.AddFile("ReadMe.txt");
                //// 把zip档案保存为MyZipFile.zip
                //zip.Save("MyZipFile.zip");

            }
        }

        public static void UnZip()
        {
            using (ZipFile zip = new ZipFile("MyZipFile.zip"))
            {
                //zip.ExtractAll("c:\\myfolder", ExtractExistingFileAction.OverwriteSilently);
            }
        }
    }
}
