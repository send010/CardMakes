using HDataCenter.IBll;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using FeyEncrypt;


namespace HDataCenter.Hospital
{



    public class Rootobject
    {
        public List<HuaYanDan> HuaYanDanList { get; set; }
    }

    public class HuaYanDan
    {
        public Mod_Hyxx mod_hyxx { get; set; }
        public List<Modlist_Hymx> modList_hymx { get; set; }
    }
    public class Mod_Hyxx
    {
        public DateTime sampleda { get; set; }
        public string instrid { get; set; }
        public string sampleno { get; set; }
        public string sampletype { get; set; }
        public string feetype { get; set; }
        public string srcdepno { get; set; }
        public string srcdocno { get; set; }
        public DateTime requestda { get; set; }
        public string requestno { get; set; }
        public string checker1 { get; set; }
        public string checker2 { get; set; }
        public string userno { get; set; }
        public string patno { get; set; }
        public string patna { get; set; }
        public string sex { get; set; }
        public string pattype { get; set; }
        public string bedno { get; set; }
        public int patage { get; set; }
        public string ageunit { get; set; }
        public string diagnose { get; set; }
        public string clinicapp { get; set; }
        public string printflag { get; set; }
        public string seqno { get; set; }
        public DateTime reportda { get; set; }
        public DateTime confirmda { get; set; }
        public string confirmman { get; set; }
        public string bk { get; set; }
        public string reserve { get; set; }
        public string mn { get; set; }
        public string chargeflag { get; set; }
        public string confirmmanna { get; set; }
        public object itemnana { get; set; }
    }
    public class Modlist_Hymx
    {
        public string ITEMNO { get; set; }
        public string ITEMNA { get; set; }
        public string NUMVAL { get; set; }
        public string PRINTVAL { get; set; }
        public string VAL1 { get; set; }
        public object VAL2 { get; set; }
        public string VAL3 { get; set; }
        public string LMTFLAG { get; set; }
        public string REF1 { get; set; }
        public string REPORTSEQ { get; set; }
        public string UNIT { get; set; }
    }

    /// <summary>
    /// 接收数据类
    /// </summary>
    public class IPaitentInfo
    {
        public string BRBM { get; set; }
        public string JSDW { get; set; }
        public string BRXM { get; set; }
        public string BRXB { get; set; }
        public string CSNY { get; set; }
        public string LXDZ { get; set; }
        public string SFZH { get; set; }

        public string GDDH { get; set; }
        public string YDDH { get; set; }
        public string BLHM { get; set; }
        public string JZKH { get; set; }
        public string ZTBZ { get; set; }
        public string FMXM { get; set; }
        public string FMSFZH { get; set; }

        public string HJDZ { get; set; }
        public string FMLXDZ { get; set; }
        public string PYDM { get; set; }
        public string YHDM { get; set; }
        public string ZHYE { get; set; }
        public string JSLB { get; set; }
        public string ZHBM { get; set; }

        public string BDZT { get; set; }
        public string YXSJ { get; set; }
        public string ZHJZSJ { get; set; }
        public string YBPB { get; set; }
        public string KZPB { get; set; }
    }
    public class IExam
    {
        public int ITEMNO { get; set; }
        public string PRINTVAL { get; set; }
        public string REPORTSEQ { get; set; }

    }
    public class Info
    {
        public string modList_hymx { get; set; }

    }
    
    public class Accessor
    {
        public Accessor()
        {

        }
        /// <summary>
        /// 磁条卡，扫描枪
        /// </summary>
        /// <param name="hispatientid"></param>
        /// <returns></returns>
        public  JsonObject GetPatientInfo(string hispatientid)
        {
            return new JsonObject() { code = "A00000", msg = "操作成功", data = "" };
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="hispatientid">刷卡时的卡号，</param>
        /// <returns>his系统中的病人标识，其他接口方法均要用到</returns>
        private static string GetPidByhispatientid(string hispatientid)
        {
            string lclass = "TJJK";
            string laction = "GetBRBM";
            string linput = string.Format("\"JZKH\":\"{0}\"", hispatientid);
            linput = "{" + linput + "}";
            string lcode = FeyEncrypt.Encrypt.DoEncrypt(linput);
            string outputst = string.Empty;
            string[] arg = new string[5];
            arg[0] = lclass;
            arg[1] = laction;
            arg[2] = linput;
            arg[3] = lcode;
            arg[4] = outputst;

            object d = WSHelper.Instance.InvokeWebService("DoAction", arg);
            string hispatid;

            IPaitentInfo mode = new IPaitentInfo();
            IPaitentInfo p = JsonConvert.DeserializeObject<IPaitentInfo>(arg[4]);
            hispatid = p.BRBM;
            return hispatid;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="menzhenhao">手机号码</param>
        /// <param name="msgcontent">发送内容</param>
        /// <returns></returns>
        public  JsonObject SendMsm(string menzhenhao, string msgcontent)
        {
            try
            {

                string lsclass = "GY_SMS";
                string lsaction = "sendMessageByLxdhImm";
                string lsinput = string.Format("\"lxdh\":\"{0}\", \"content\":\"{1}\"", menzhenhao, msgcontent);
                lsinput = "[{" + lsinput + "}]";

                string lscode = FeyEncrypt.Encrypt.DoEncrypt(lsinput);
                string outputstr = string.Empty;
                string[] args = new string[5];
                args[0] = lsclass;
                args[1] = lsaction;
                args[2] = lsinput;
                args[3] = lscode;
                args[4] = outputstr ;
                int r = Convert.ToInt32( WSHelper.Instance.InvokeWebService("DoAction", args));

                if(r==1)
                    return new JsonObject() { code = "A00000", msg = "发送成功", data = menzhenhao };
                else
                    return new JsonObject() { code = "B00003", msg = "发送失败", data = menzhenhao };
            }
            catch (Exception ex)
            {

                return new JsonObject() { code = "B00003", msg = ex.Message, data = menzhenhao };
            }
        }

        /// <summary>
        /// ic卡      
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public  string LookupVisitNo(string cardId)
        {
            try
            {
                return cardId;
            }
            catch
            {
                return null;
            }
        }

        public JsonObject GetDiagnoseInfo(string hispatientid)
        {
            try
            {
                throw new Exception("未实现检验接口");
            }
            catch (Exception ex)
            {
                return new JsonObject() { code = "B00003", msg = ex.Message, data = null };
            }
        }
        public  JsonObject GetLisResultByBarCode(string barcode)  //检验的条码号
        {
            try
            {
                throw new Exception("未实现检验接口");
            }
            catch (Exception ex)
            {
                return new JsonObject() { code = "B00003", msg = ex.Message, data = null };
            }
        }


        public  JsonObject UpLoadExaminItems(string exammodel)
        {
            throw new Exception("未实现开项目接口");

        }
        
    }
}
