using MainTest.RedLetterQuery;
using Newtonsoft.Json;
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

        /// <summary>
        /// 解密之后再base64解密备注
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_desAndRemark_Click(object sender, EventArgs e)
        {
            this.resultText.BeginInvoke((Action)delegate
            {
                string filePath = @"./responseData/result01.txt";
                string res = ReadFile(filePath);

                try
                {
                    var resDes = DBQuery.AesDecrypt(res);


                    List<OutputInvoiceTaxControlQueryDto> result = this.DeserializeTaxControlQueryInfo<OutputInvoiceTaxControlQueryDto>(resDes);


                    if (result != null && result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            if (!string.IsNullOrEmpty(item.BZ))
                            {
                                item.BZ = this.DecodeBase64(Encoding.GetEncoding("gb2312"), item.BZ);
                            }
                        }
                    }

                    var newResultStr = JsonConvert.SerializeObject(result);

                    this.resultText.Text = newResultStr;
                }
                catch (Exception ex)
                {
                    this.resultText.Text = ex.Message.ToString();
                }


            }, null);
        }

        private List<T> DeserializeTaxControlQueryInfo<T>(string strData)
        {
            dynamic queryObj = JsonConvert.DeserializeObject(strData);

            // 处理这种情况 [{},{}]
            if (queryObj is Newtonsoft.Json.Linq.JArray)
            {
                return JsonConvert.DeserializeObject<List<T>>(strData);
            }

            // { 'data':[]} 处理这种数据的情况，报 out of index 的bug
            if (queryObj.data != null && queryObj.data is Newtonsoft.Json.Linq.JArray)
            {
                var dataList = queryObj.data as Newtonsoft.Json.Linq.JArray;

                if (dataList.Count == 0)
                {
                    return new List<T>();
                }
            }

            var content = queryObj.data != null && queryObj.data[0] is Newtonsoft.Json.Linq.JArray ? queryObj.data[0] : queryObj.data;
            var queryList = JsonConvert.DeserializeObject<List<T>>(Convert.ToString(content));
            return queryList;
        }


        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="codeName">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public string DecodeBase64(Encoding encode, string result)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }
    }
}
