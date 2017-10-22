using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLS_Quality
{
    public enum Options
    {
        Select = 0,
        Insert = 1,
        Update = 2,
        Delete = 3,
    }

    /// <summary>
    /// 工单的执行状态
    /// </summary>
    public enum WorkOrderStatus
    {
        /// <summary>
        /// 未开始
        /// </summary>
        NotStart,
        /// <summary>
        /// 正在进行
        /// </summary>
        Starting,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause,
        /// <summary>
        /// 完成
        /// </summary>
        Completed
    }

    /// <summary>
    /// 工艺指导说明文档审核状态
    /// </summary>
    public enum DocumentCheckStatus
    {

        /// <summary>
        /// 未审
        /// </summary>
        CheckNo,
        /// <summary>
        /// 待审
        /// </summary>
        CheckIng,
        /// <summary>
        /// 已审
        /// </summary>
        Checked
    }

    /// <summary>
    /// 工艺指导说明文档下发状态
    /// </summary>
    public enum DocumentReleaseStatus
    {
        /// <summary>
        /// 未下发
        /// </summary>
        ReleaseNo,
        /// <summary>
        /// 已下发
        /// </summary>
        Released,
        /// <summary>
        /// 撤回
        /// </summary>
        Revocation
    }

    public enum DocumentOperationType
    {
        /// <summary>
        /// 添加
        /// </summary>
        Add,
        /// <summary>
        /// 修改
        /// </summary>
        Edit,
        /// <summary>
        /// 删除
        /// </summary>
        Delete,
        /// <summary>
        /// 保存
        /// </summary>
        Save,
        /// <summary>
        /// 配置
        /// </summary>
        Config,
        /// <summary>
        /// 下载
        /// </summary>
        Download,
        /// <summary>
        /// 撤回
        /// </summary>
        Revocation,
        /// <summary>
        /// 审核
        /// </summary>
        Check,
        /// <summary>
        /// 下发
        /// </summary>
        Send,
        /// <summary>
        /// 预览
        /// </summary>
        Preview
    }

    /// <summary>
    /// 检查结果
    /// </summary>
    public enum CheckResult
    {
        /// <summary>
        /// 不合格
        /// </summary>
        Disqualification,
        /// <summary>
        /// 合格
        /// </summary>
        Qualified,
        /// <summary>
        /// 部份合格
        /// </summary>
        Partqualified
    }

    /// <summary>
    /// 处理方式（返工、返修、报废）
    /// </summary>
    public enum HandleType
    {
        /// <summary>
        /// 返工
        /// </summary>
        Redo = 0,
        /// <summary>
        /// 返修
        /// </summary>
        Repair,
        /// <summary>
        /// 报废
        /// </summary>
        Scrap
    }

    /// <summary>
    /// 条码类型
    /// </summary>
    public class BarCodeType
    {
        /// <summary>
        /// 铜排
        /// </summary>
        public const string DT = "DT";
        public const string DT_Text = "铜排";
        /// <summary>
        /// 型材
        /// </summary>
        public const string XC = "XC";
        public const string XC_TEXT = "型材";
        /// <summary>
        /// 连接器
        /// </summary>
        public const string LJQ = "LJQ";
        public const string LJQ_TEXT = "连接器";
        /// <summary>
        /// 插接箱
        /// </summary>
        public const string CJX = "CJX";
        public const string CJX_TEXT = "插接箱";
        /// <summary>
        /// 插接箱名牌
        /// </summary>
        public const string CJXMP = "CJXMP";
        public const string CJXMP_TEXT = "插接箱名牌";
        /// <summary>
        /// 母线
        /// </summary>
        public const string MP = "MP";
        public const string MP_Text = "铜排";
    }

    /// <summary>
    /// 功能单元编码规则
    /// </summary>
    public class FunUnits
    {
        /// <summary>
        /// 电流
        /// </summary>
        public static Dictionary<string, string> Electricity
        {
            get
            {
                Dictionary<string, string> _Units = new Dictionary<string, string>();
                _Units.Add("004", "40A");
                _Units.Add("006", "63A");
                _Units.Add("008", "80A");
                _Units.Add("010", "100A");
                _Units.Add("012", "125A");
                _Units.Add("014", "140A");
                _Units.Add("016", "160A");
                _Units.Add("020", "200A");
                _Units.Add("025", "250A");
                _Units.Add("031", "315A");
                _Units.Add("040", "400A");
                _Units.Add("050", "500A");
                _Units.Add("063", "630A");
                _Units.Add("080", "800A");
                _Units.Add("100", "1000A");
                _Units.Add("125", "1250A");
                _Units.Add("135", "1350A");
                _Units.Add("160", "1600A");
                _Units.Add("200", "2000A");
                _Units.Add("230", "2300A");
                _Units.Add("250", "2500A");
                _Units.Add("300", "3000A");
                _Units.Add("315", "3150A");
                _Units.Add("320", "3200A");
                _Units.Add("350", "3500A");
                _Units.Add("380", "3800A");
                _Units.Add("400", "4000A");
                _Units.Add("450", "4500A");
                _Units.Add("500", "5000A");
                _Units.Add("630", "6300A");
                return _Units;
            }
        }

        /// <summary>
        /// 防护等级
        /// </summary>
        public static Dictionary<string, string> ProtectionLevel
        {
            get
            {
                Dictionary<string, string> _Units = new Dictionary<string, string>();
                _Units.Add("54", "IP54");
                _Units.Add("65", "IP65");
                _Units.Add("66", "IP66");
                return _Units;
            }
        }

        /// <summary>
        /// 产品系列
        /// </summary>
        public static Dictionary<string, string> ProSeries
        {
            get
            {
                Dictionary<string, string> _Units = new Dictionary<string, string>();
                _Units.Add("LVC", "LV");
                _Units.Add("LVA", "LV");
                _Units.Add("LVS", "LVS");
                _Units.Add("WP2", "WP2");
                return _Units;
            }
        }

        /// <summary>
        /// 流水号数码
        /// </summary>
        public static Dictionary<string, string> TypeNO
        {
            get
            {
                Dictionary<string, string> _Units = new Dictionary<string, string>();
                _Units.Add("LM", "01");
                _Units.Add("LVC", "02");
                _Units.Add("CJX", "03");
                _Units.Add("SDX", "04");
                _Units.Add("GM-D", "05");
                _Units.Add("LVS", "06");
                _Units.Add("GM-N", "07");
                _Units.Add("MM", "08");
                _Units.Add("PH", "09");
                return _Units;
            }
        }
    }

}
