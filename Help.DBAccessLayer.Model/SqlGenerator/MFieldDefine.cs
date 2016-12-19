using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help.DBAccessLayer.Model.SqlGenerator
{
    /// <summary>
    /// 表字段定义
    /// </summary>
    public class MFieldDefine
    {
        /// <summary>
        /// 字段格式
        /// </summary>
        public string FieldFormat;
       
        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 字段中文名
        /// </summary>
        public string FieldNameCH
        {
            get;
            set;
        }

        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName
        {
            get;
            set;
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType
        {
            get;
            set;
        }

        /// <summary>
        /// 数据类型长度
        /// </summary>
        public int Length
        {
            get;
            set;
        }

        /// <summary>
        /// 是否可空
        /// </summary>
        public bool IsNullable
        {
            get;
            set;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public int PrimaryKeyIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 外部关系
        /// </summary>
        public string ForeignRelation
        {
            get;
            set;
        }

        /// <summary>
        /// 是否是唯一索引
        /// </summary>
        public bool IsUniqueIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 索引序号
        /// </summary>
        public int IndexNo
        {
            get;
            set;
        }

        /// <summary>
        /// 是否是自增长类型
        /// </summary>
        public bool IsAutoIncrement
        {
            get;
            set;
        }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue
        {
            get;
            set;
        }

        /// <summary>
        /// 值约束
        /// </summary>
        public string ValueConstraint
        {
            get;
            set;
        }

        /// <summary>
        /// 项目意义
        /// </summary>
        public string ProjectSignificance
        {
            get;
            set;
        }

        public int DigitalLength { get; set; }
    }
}
