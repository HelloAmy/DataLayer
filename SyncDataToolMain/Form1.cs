using MainTest.RedLetterQuery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncDataToolMain
{
    public partial class readFileData : Form
    {
        public readFileData()
        {
            InitializeComponent();
        }




        private void BWEncrypt_Click(object sender, EventArgs e)
        {
            this.resultText.BeginInvoke((Action)delegate
            {
                if (this.inputText.Text != null)
                {
                    this.resultText.Text = DBQuery.AesEncrypt(this.inputText.Text.ToString());
                }
                else
                {
                    this.resultText.Text = string.Empty;
                }
            }, null);

        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="path">文件路径和文件名</param>
        /// <returns>文件内容</returns>
        public static string ReadFile(string path)
        {
            string str = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    str = reader.ReadToEnd();
                    return str;
                }
            }
            catch (Exception ex)
            {
                Console.Write("读文件错误:" + ex.Message.ToString());
            }

            return str;
        }

        private void BWDecrypt_Click(object sender, EventArgs e)
        {


            this.resultText.BeginInvoke((Action)delegate
            {
                string filePath = "./responseData/result01.txt";
                string res = ReadFile(filePath);

                if (this.inputText.Text != null)
                {
                    try
                    {
                        this.resultText.Text = DBQuery.AesDecrypt(res);
                    }
                    catch (Exception ex)
                    {
                        this.resultText.Text = ex.Message.ToString();
                    }
                }
                else
                {
                    this.resultText.Text = string.Empty;
                }
            }, null);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.resultText.BeginInvoke((Action)delegate
            {
                string filePath = @"C:\Julius_J_Zhu\09DataLayer\SyncDataToolMain\responseData\result02.txt";
                string res = ReadFile(filePath);

                res = res.Replace(@"\r\n", System.Environment.NewLine);

                this.resultText.Text = res;

            }, null);
        }

        private void btn_DesLog_Click(object sender, EventArgs e)
        {
            this.resultText.BeginInvoke((Action)delegate
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory;
                string fileName = "log" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                this.Des(fileName);
                this.resultText.Text = "解密完成！！,文件名:" + fileName;
            }, null);
        }


        public void Des(string fileName)
        {
            Regex r = new Regex("<mw>.*</mw>");
            string filePath = @"./responseData/log.txt";
            string str = ReadFile(filePath);

            var collection = r.Matches(str);

            StringBuilder ret = new StringBuilder();

            var index = 1;

            foreach (var mat in collection)
            {
                string value = mat.ToString();

                if (value != null)
                {
                    string realValue = value.Replace("<mw>", string.Empty).Replace("</mw>", string.Empty);
                    string resultValue = DBQuery.AesDecrypt(realValue);
                    str = str.Replace(value, "<mw>" + resultValue + "</mw>");
                }

                index++;
            }

            r = new Regex("<json>.*</json>");
            collection = r.Matches(str);

            foreach (var mat in collection)
            {
                string value = mat.ToString();

                if (value != null)
                {
                    string realValue = value.Replace("<json>", string.Empty).Replace("</json>", string.Empty);

                    string resultValue = DBQuery.AesDecrypt(realValue);

                    str = str.Replace(value, "<json>" + resultValue + "</json>");
                }
            }

            SaveFile(str, fileName);
            // 写入文件
        }

        private void SaveFile(string str, string fileName)
        {
            using (FileStream fs2 = new FileStream(fileName, FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(fs2);

                //开始写入
                sw.Write(str);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
            }
        }

        private void btn_desDecrypt_Click(object sender, EventArgs e)
        {
            this.resultText.BeginInvoke((Action)delegate
            {
                if (this.inputText.Text == null)
                {
                    this.resultText.Text = string.Empty;
                }
                else
                {
                    string res = this.inputText.Text.ToString();

                    if (this.inputText.Text != null)
                    {
                        try
                        {
                            this.resultText.Text = DBQuery.AesDecrypt(res);
                        }
                        catch (Exception ex)
                        {
                            this.resultText.Text = ex.Message.ToString();
                        }
                    }
                    else
                    {
                        this.resultText.Text = string.Empty;
                    }
                }
            }, null);
        }

    }
}
