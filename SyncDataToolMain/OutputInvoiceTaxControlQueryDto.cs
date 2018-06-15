using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncDataToolMain
{
    public class OutputInvoiceTaxControlQueryDto
    {
        /// <summary>
        /// 固定为“c”代表普通发票
        /// 机动车:j,专票：s
        /// </summary>
        public string FPZL { get; set; }

        /// <summary>
        /// 发票代码
        /// </summary>
        public string FPDM { get; set; }

        /// <summary>
        /// 发票号码
        /// 因为金税版返回的数据可能不足8位，所以需要在前面补0
        /// </summary>
        public string FPHM
        {
            get;
            set;
        }

        /// <summary>
        /// 开票机号
        /// </summary>
        public string KPJH { get; set; }

        /// <summary>
        /// 销售订单编号
        /// </summary>
        public string XSDJBH { get; set; }

        /// <summary>
        /// 购方名称
        /// </summary>
        public string GFMC { get; set; }

        /// <summary>
        /// 购方税号
        /// </summary>
        public string GFSH { get; set; }

        /// <summary>
        /// 普/专：购方地址电话
        /// 机动车：车辆类型
        /// </summary>
        public string GFDZDH { get; set; }

        /// <summary>
        /// 普/专：购方银行账号
        /// //针对机动车是主管税务机关
        /// </summary>
        public string GFYHZH { get; set; }

        /// <summary>
        /// 销方名称
        /// </summary>
        public string XFMC { get; set; }

        /// <summary>
        /// 销方税号
        /// </summary>
        public string XFSH { get; set; }

        /// <summary>
        ///普/专： 销方地址电话
        /// 机动车：销方地址
        /// </summary>
        public string XFDZDH { get; set; }

        /// <summary>
        /// 普/专：销方银行账号
        /// 机动车：销方银行
        /// </summary>
        public string XFYHZH { get; set; }

        /// <summary>
        ///  销售编码  基本上没有用
        /// </summary>
        public string XSBM { get; set; }

        /// <summary>DYBZ
        ///  JZPZHM  YDXS  没用
        /// </summary>
        public string YDXS { get; set; }

        /// <summary>
        /// 加密版本号
        /// </summary>
        public string JMBBH { get; set; }

        /// <summary>
        ///  密文
        /// </summary>
        public string MW { get; set; }

        /// <summary>
        /// 开票日期
        /// </summary>
        public string KPRQ { get; set; }

        /// <summary>
        /// 所属月份
        /// </summary>
        public string SSYF { get; set; }

        /// <summary>
        /// 合计金额
        /// </summary>
        public string HJJE { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        public string SLV { get; set; }

        /// <summary>
        /// 合计税额
        /// </summary>
        public string HJSE { get; set; }

        /// <summary>
        /// 普/专：主要商品名称
        /// 机动车：商品编码
        /// </summary>
        public string ZYSPMC { get; set; }

        /// <summary>
        /// 商品税目
        /// </summary>
        public string SPSM { get; set; }

        /// <summary>
        ///  JZPZHM缴款凭证序号
        ///  YDXS  没用 
        /// </summary>
        public string JZPZHM { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }

        /// <summary>
        /// 开票人
        /// </summary>
        public string KPR { get; set; }

        /// <summary>
        /// 普/专：收款人
        /// 机动车：优惠政策信息
        /// </summary>
        public string SKR { get; set; }

        /// <summary>
        /// 复核人
        /// </summary>
        public string FHR { get; set; }

        /// <summary>
        ///  打印标志
        /// </summary>
        public string DYBZ { get; set; }

        /// <summary>
        /// 清单标志：true 有清单
        /// </summary>
        public string QDBZ { get; set; }

        /// <summary>
        ///  GFBH这个是开票软件自己使用的一个ID用来找购方信息的gfbh
        ///  购方编码
        /// </summary>
        public string GFBH { get; set; }

        /// <summary>
        ///  红字通知单
        ///  机动车：商品编号
        /// </summary>
        public string HZTZDH { get; set; }

        /// <summary>
        /// 税盘编号
        /// </summary>
        public string SPBH { get; set; }

        /// <summary>
        ///  作废标志
        /// </summary>
        public string ZFBZ { get; set; }

        /// <summary>
        ///  报税标志
        /// </summary>
        public string BSBZ { get; set; }

        /// <summary>
        ///  消费标志
        /// </summary>
        public string XFBZ { get; set; }

        /// <summary>
        /// 校验码（专票为空）
        /// </summary>
        public string JYM { get; set; }

        /// <summary>
        ///  报税期
        /// </summary>
        public string BSQ { get; set; }

        /// <summary>
        ///  下载标志
        /// </summary>
        public string XZBZ { get; set; }

        /// <summary>
        ///  凭证类别
        /// </summary>
        public string PZLB { get; set; }

        /// <summary>
        /// 凭证号码
        /// </summary>
        public string PZHM { get; set; }

        /// <summary>
        ///  凭证业务号
        /// </summary>
        public string PZYWH { get; set; }

        /// <summary>
        ///  凭证状态
        /// </summary>
        public string PZZT { get; set; }

        /// <summary>
        ///  凭证日期
        /// </summary>
        public string PZRQ { get; set; }

        /// <summary>
        ///  汉信码
        /// </summary>
        public string HXM { get; set; }

        /// <summary>
        ///  受理号
        /// </summary>
        public string SYH { get; set; }

        /// <summary>
        ///  申报标志
        /// </summary>
        public string SBBZ { get; set; }

        /// <summary>
        ///  营业税标志
        /// </summary>
        public string YYSBZ { get; set; }

        /// <summary>
        ///  网扣标志---已废弃
        /// </summary>
        public string WKBZ { get; set; }

        /// <summary>
        ///  单据标志---已废弃
        /// </summary>
        public string DJBZ { get; set; }

        /// <summary>
        ///  这个不知道  
        ///  ----机动车的合格证号
        /// </summary>
        public string CM { get; set; }

        /// <summary>
        ///   这个不清楚---已废弃
        /// </summary>
        public string DLGRQ { get; set; }

        /// <summary>
        ///  开户银行名称-销方
        ///  机动车是producing place 产地
        /// </summary>
        public string KHYHMC { get; set; }

        /// <summary>
        ///  开户银行账号-销方
        /// </summary>
        public string KHYHZH { get; set; }

        /// <summary>
        ///  百望也不知道
        ///  ---机动车的进口证明书
        /// </summary>
        public string TYDH { get; set; }

        /// <summary>
        ///  百望也不知道
        ///  ---机动车的商检单号
        /// </summary>
        public string QYD { get; set; }

        /// <summary>
        ///  百望也不知道
        ///   ---机动车的发动机号码
        /// </summary>
        public string ZHD { get; set; }

        /// <summary>
        ///  百望也不知道---感觉是车辆识别号vin   "XHD": "5YJSA7E18HF222122",
        /// </summary>
        public string XHD { get; set; }

        /// <summary>
        ///  百望也不知道
        ///  ---机动车的限乘人数
        /// </summary>
        public string MDD { get; set; }

        /// <summary>
        ///  机动车吨位
        /// </summary>
        public string YYZZH { get; set; }

        /// <summary>
        ///  货运编码
        ///   ---机动车的主管税务机关代码
        /// </summary>
        public string HYBM { get; set; }

        /// <summary>
        /// 机动车：品牌和型号
        /// 厂牌型号
        /// </summary>
        public string XFDZ { get; set; }

        /// <summary>
        /// 机动车：销方电话
        /// </summary>
        public string XFDH { get; set; }

        /// <summary>
        /// 机动车：机器编号，金税盘号
        /// </summary>
        public string JQBH { get; set; }

        /// <summary>
        /// 机动车：生产厂家名称
        /// </summary>
        public string SCCJMC { get; set; }

        /// <summary>
        /// 运输货物信息---已废弃
        /// </summary>
        public string YSHWXX { get; set; }

        /// <summary>
        /// 报税状态
        /// </summary>
        public string BSZT { get; set; }

        /// <summary>
        ///签名值
        /// </summary>
        public string SIGN { get; set; }

        /// <summary>
        /// 作废日期
        /// </summary>
        public string ZFRQ { get; set; }

        /// <summary>
        /// 机动车：完税凭证号码
        /// </summary>
        public string WSPZHM { get; set; }

        /// <summary>
        /// 机动车：蓝字号码代码  红字发票才有
        ///  "LZDMHM": "131001420652_00283092",
        ///  代码在前号码在后
        /// </summary>
        public string LZDMHM { get; set; }

        /// <summary>
        /// 报税参数---废弃
        /// </summary>
        public string BSCS { get; set; }

        /// <summary>
        ///  凭证WL业务号---废弃
        /// </summary>
        public string PZWLYWH { get; set; }

        /// <summary>
        /// 报税日志，报税状态：      "BSRZ": "【2018-01-03 14:51:52】 抄报清卡",
        /// </summary>
        public string BSRZ { get; set; }

        /// <summary>
        /// 电子受验号
        /// </summary>
        public string DZSYH { get; set; }

        /// <summary>
        /// 开票顺序号
        /// </summary>
        public string KPSXH { get; set; }

        /// <summary>
        /// 编码表编号，编码版本号
        /// </summary>
        public string BMBBBH { get; set; }

        /// <summary>
        /// 商品税目编码--无用
        /// </summary>
        public string SPSM_BM { get; set; }

        /// <summary>
        /// 未知---废弃
        /// </summary>
        public string YDBS { get; set; }

        /// <summary>
        /// 未知--废弃
        /// </summary>
        public string DKBDBS { get; set; }

    }
}
