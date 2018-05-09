using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MainTest.TestModel
{

    [XmlRoot("output")]
    public class OutputObj
    {
        [XmlElementAttribute("returncode")]
        public string ReturnCode { get; set; }

        [XmlElementAttribute("returnmsg")]
        public string ReturnMsg { get; set; }
    }





    [XmlRoot("body")]
    public class DBQueryOutputObj
    {
        [XmlElement("output")]
        public OutputObj output
        {
            get;
            set;
        }

        [XmlElementAttribute("json")]
        public string json
        {
            get;
            set;
        }
    }

    public class BWRedLetterMX
    {
        /// <summary>
        /// 申请单单号 对应税控盘显示的编号
        /// "SQDDH":"5101981802026294"
        /// </summary>
        public string SQD_DH { get; set; }
        /// <summary>
        /// 申请单编号
        /// </summary>
        public string SQD_BH { get; set; }
        /// <summary>
        /// 商品序号
        /// </summary>
        public string SPXH { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string SPMC { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string DJ { get; set; }
        /// <summary>
        /// 商品数量，一般是负数
        ///  "SPSL": "-1",
        /// </summary>
        public string SPSL { get; set; }

        /// <summary>
        /// 税率
        ///  "SL": "0.17",
        /// </summary>
        public string SL { get; set; }
        /// <summary>
        /// 金额
        /// "JE": "-574214.67",
        /// </summary>
        public string JE { get; set; }
        /// <summary>
        /// 税额
        /// "SE": "-97616.49",
        /// </summary>
        public string SE { get; set; }
        /// <summary>
        /// 通知书编号
        /// </summary>
        public string TZSBH { get; set; }
        /// <summary>
        /// 含税标志
        /// "HSBZ":"0"
        /// </summary>
        public string HSBZ { get; set; }
        /// <summary>
        /// 规格型号
        ///   "GGXH": "小",
        ///   "GGXH":"5YJSA7E11HF196334"
        /// </summary>
        public string GGXH { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string JLDW { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string spbm { get; set; }
        /// <summary>
        /// 税收分类编码
        /// </summary>
        public string scbm { get; set; }
        /// <summary>
        /// 免税类型
        /// "mslx":""
        /// </summary>
        public string mslx { get; set; }
        /// <summary>
        /// 优惠政策标识
        /// "yhzcbz":"0"
        /// </summary>
        public string yhzcbz { get; set; }
        /// <summary>
        /// 优惠政策
        /// </summary>
        public string yhzc { get; set; }
    }


    public static class XmlSerializer
    {
        private static XmlSerializerNamespaces _namespaces = new XmlSerializerNamespaces(
            new XmlQualifiedName[] {
                new XmlQualifiedName(string.Empty, string.Empty)
            });

        public static string SaveToXml(object sourceObj, Type type)
        {
            if (sourceObj != null)
            {
                type = type != null ? type : sourceObj.GetType();

                string input = string.Empty;
                using (StringWriter txtWriter = new StringWriter())
                {
                    XmlWriterSettings setting = new XmlWriterSettings();
                    setting.Indent = false;
                    setting.NewLineChars = string.Empty;
                    setting.OmitXmlDeclaration = true;
                    setting.Encoding = Encoding.UTF8;
                    using (XmlWriter xmlWriter = XmlWriter.Create(txtWriter, setting))
                    {
                        System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
                        xmlSerializer.Serialize(xmlWriter, sourceObj, _namespaces);
                    }

                    var rtn = txtWriter.ToString();
                    var ele = XElement.Parse(rtn);
                    ele.Element("body").Element("input").RemoveAttributes();
                    return ele.ToString(SaveOptions.DisableFormatting);
                }
            }

            return string.Empty;
        }

        public static object LoadFromXml(string xmlStr, Type type)
        {
            object result = null;

            if (!string.IsNullOrEmpty(xmlStr))
            {
                var ele = XElement.Parse(xmlStr);
                xmlStr = ele.Element("body").Element("output").ToString();
                if (string.IsNullOrEmpty(xmlStr))
                {
                    return result;
                }

                using (StringReader txtReader = new StringReader(xmlStr))
                {
                    XmlReaderSettings setting = new XmlReaderSettings();

                    using (XmlReader xmlReader = XmlReader.Create(txtReader, setting))
                    {
                        xmlReader.ReadToDescendant("output");
                        System.Xml.Serialization.XmlSerializer xmlSerializer
                            = new System.Xml.Serialization.XmlSerializer(type, new XmlRootAttribute("output"));
                        result = xmlSerializer.Deserialize(xmlReader.ReadSubtree());
                    }
                }
            }

            return result;
        }
    }

    public class BaiWangVehicleFpplcxObj
    {
        /// <summary>
        /// 发票代码
        /// </summary>
        public string fpdm { get; set; }
        /// <summary>
        /// 发票号码
        /// </summary>
        public string fphm { get; set; }
        /// <summary>
        /// 应用类型代码
        /// </summary>
        public string yylxdm { get; set; }
        /// <summary>
        /// 开票日期
        /// </summary>
        public string kprq { get; set; }

        /// <summary>
        /// 开票日期 接口版本的kprq 查询有问题，重命名一个名称就能查出来开票日期
        /// </summary>
        public string kprq_New { get; set; }
        /// <summary>
        /// 税控码
        /// </summary>
        public string skm { get; set; }
        /// <summary>
        /// 购货单位
        /// </summary>
        public string ghdw { get; set; }
        /// <summary>
        /// 生产企业名称
        /// </summary>
        public string scqymc { get; set; }
        /// <summary>
        /// 证件类型
        /// //"zjlx":"企业","gfnsrsbh":"000000000000000000","zjhm":"",
        //"zjlx":"企业","gfnsrsbh":"","zjhm":"500105198910131213"  
        //"zjlx":"企业","gfnsrsbh":"915101142019983564","zjhm":"915101142019983564"
        //"zjlx":"个人","gfnsrsbh":"","zjhm":"510130196203140048"
        //"zjlx":"个人","gfnsrsbh":"91510114202602608K","zjhm":"91510114202602608K"  
        /// </summary>
        public string zjlx { get; set; }
        /// <summary>
        /// 购方纳税人识别号
        /// </summary>
        public string gfnsrsbh { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string zjhm { get; set; }
        /// <summary>
        /// 车辆类型
        /// </summary>
        public string cllx { get; set; }
        /// <summary>
        /// 厂牌型号
        /// </summary>
        public string cpxh { get; set; }
        /// <summary>
        /// 合格证号
        /// </summary>
        public string hgzh { get; set; }
        /// <summary>
        /// 进口证明书号
        /// </summary>
        public string jkzmsh { get; set; }
        /// <summary>
        /// 车架号码/车辆识别代号
        /// </summary>
        public string cjhm { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public string cd { get; set; }
        /// <summary>
        /// 商检单号---机动车独有的
        /// </summary>
        public string sjdh { get; set; }
        /// <summary>
        /// 发动机号码
        /// </summary>
        public string fdjhm { get; set; }

        /// <summary>
        /// 价税合计
        /// </summary>
        public string jshj { get; set; }
        /// <summary>
        /// 销方电话
        /// </summary>
        public string xf_dh { get; set; }
        /// <summary>
        /// 销方开户银行
        /// </summary>
        public string xf_khyh { get; set; }
        /// <summary>
        /// 销方账号
        /// </summary>
        public string xf_zh { get; set; }
        /// <summary>
        /// 销方地址
        /// </summary>
        public string xf_dz { get; set; }
        /// <summary>
        /// 销货单位名称
        /// </summary>
        public string xf_nsrmc { get; set; }
        /// <summary>
        /// 销方纳税人识别号
        /// </summary>
        public string xf_nsrsbh { get; set; }
        /// <summary>
        /// 增值税税率
        /// </summary>
        public string zzssl { get; set; }
        /// <summary>
        /// 增值税税额
        /// </summary>
        public string zzsse { get; set; }
        /// <summary>
        /// 不含税价
        /// </summary>
        public string bhsj { get; set; }
        /// <summary>
        /// 吨位
        /// </summary>
        public string dw { get; set; }
        /// <summary>
        /// 限乘人数
        /// </summary>
        public string xcrs { get; set; }
        /// <summary>
        /// 开票人
        /// </summary>
        public string kpr { get; set; }
        /// <summary>
        /// 原发票代码---如果是红票就会有
        /// </summary>
        public string yfpdm { get; set; }
        /// <summary>
        /// 原发票号码--如果是红票就会有
        /// </summary>
        public string yfphm { get; set; }
        /// <summary>
        /// 税控盘编号
        /// </summary>
        public string skpbh { get; set; }
        /// <summary>
        /// 主管税务机关代码
        /// </summary>
        public string zgswjg_dm { get; set; }
        /// <summary>
        /// 主管税务机关名称
        /// </summary>
        public string zgswjg_mc { get; set; }
        /// <summary>
        /// 完税凭证号码
        /// </summary>
        public string wspz_hm { get; set; }

        /// <summary>
        /// 车辆类型代码
        /// </summary>
        public string cllxdm { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 所属月份
        /// </summary>
        public string ssyf { get; set; }

        /// <summary>
        /// 发票状态
        /// </summary>
        public string fpzt { get; set; }
        /// <summary>
        ///  作废标志
        /// </summary>
        public string zfbz { get; set; }
        /// <summary>
        /// 作废日期
        /// </summary>
        public string zfrq { get; set; }
        /// <summary>
        /// 作废人
        /// </summary>
        public string zfr { get; set; }
        /// <summary>
        /// 传输标志
        /// </summary>
        ///  
        public string csbz { get; set; }
        /// <summary>
        /// 传输日期
        /// </summary>
        public string csrq { get; set; }
        /// <summary>
        ///  报税期
        /// </summary>
        public string bsq { get; set; }
        /// <summary>
        ///  报税标志
        /// </summary>
        public string bsbz { get; set; }
        /// <summary>
        ///  打印标志
        /// </summary>
        public string dybz { get; set; }
        /// <summary>
        ///  单据标志---已废弃？？
        /// </summary>
        public string djbz { get; set; }
        /// <summary>
        ///  网扣标志---已废弃？？
        /// </summary>
        public string wkbz { get; set; }
        /// <summary>
        ///  消费标志
        /// </summary>
        public string xfbz { get; set; }
        public string kz1 { get; set; }
        public string kz2 { get; set; }
        public string kz3 { get; set; }
        public string kz4 { get; set; }
        public string kz5 { get; set; }
        public string kz6 { get; set; }
        public string dyqz { get; set; }
        /// <summary>
        /// 签名值
        /// </summary>
        public string qmz { get; set; }
        /// <summary>
        /// 签名参数
        /// </summary>
        public string qmcs { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string spbm { get; set; }
        /// <summary>
        /// 税收分类编码
        /// </summary>
        public string scbm { get; set; }
        /// <summary>
        /// 免税类型
        /// </summary>
        public string mslx { get; set; }
        /// <summary>
        /// 优惠政策标识
        /// </summary>
        public string yhzcbz { get; set; }
        /// <summary>
        /// 商品版本号
        /// </summary>
        public string bbh { get; set; }
        /// <summary>
        /// 优惠政策名称
        /// </summary>
        public string yhzc { get; set; }
    }

    [XmlInclude(typeof(BaiwangDBQueryOutputObj))]
    [XmlInclude(typeof(BaiWangYbjszOutput))]
    [XmlRoot("output")]
    public abstract class BaiWangOutputObj
    {
        [XmlElementAttribute("returncode")]
        public string ReturnCode { get; set; }

        [XmlElementAttribute("returnmsg")]
        public string ReturnMsg { get; set; }
    }
    public class BaiwangDBQueryOutputObj : BaiWangOutputObj
    {
        [XmlElementAttribute("json")]
        public string json
        {
            get;
            set;
        }
    }

    //页边距设置输出
    public class BaiWangYbjszOutput : BaiWangOutputObj
    {
        //发票类型代码
        [XmlElementAttribute("fplxdm")]
        public string FPLXDM { get; set; }

        [XmlElementAttribute("fplgbw")]
        public string FPLGBW { get; set; }

        [XmlElementAttribute("dqfpdm")]
        public string DQFPDM { get; set; }

        [XmlElementAttribute("dqfphm")]
        public string DQFPHM { get; set; }

        [XmlElementAttribute("zsyfs")]
        public string ZSYFS { get; set; }

    }


    public class TestPagerService
    {
        public static void TestPagerSql(string sqlFormat, int pageSize)
        {
            int pageIndex = 0;
            string sql = string.Format(sqlFormat, pageSize, pageIndex * pageSize);
            Console.WriteLine(sql);

            while (true)
            {
                pageIndex++;
                sql = string.Format(sqlFormat, pageSize, pageIndex * pageSize);
                Console.WriteLine(sql);
                break;
            }

        }
 
    }
}
