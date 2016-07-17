using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace YinanSoft.IDCardReader
{
    class IDCardReader
    {
        #region 民族表
        static public string[] Nation = {"无",	
		"汉族","蒙古族","回族","藏族","维吾尔族",
        "苗族","彝族","壮族","布依族","朝鲜族",
        "满族",
        "侗族",
        "瑶族",
        "白族",
        "土家族",
        "哈尼族",
        "哈萨克族",
        "傣族",
        "黎族",
        "傈僳族",
        "佤族",
        "畲族",
        "高山族",
        "拉祜族",
        "水族",
        "东乡族",
        "纳西族",
        "景颇族",
        "柯尔克孜族",
        "土族",
        "达斡尔族",
        "仫佬族",
        "羌族",
        "布朗族",
        "撒拉族",
        "毛难族",
        "仡佬族",
        "锡伯族",
        "阿昌族",
        "普米族",
        "塔吉克族",
        "怒族",
        "乌孜别克",
        "俄罗斯族",
        "鄂温克族",
        "崩龙族",
        "保安族",
        "裕固族",
        "京族",
        "塔塔尔族",
        "独龙族",
        "鄂伦春族",
        "赫哲族",
        "门巴族",
        "珞巴族",
        "其它",
        "外国血统"};
        #endregion

        #region 导入函数
        [DllImport("termb.dll", CharSet = CharSet.Auto)]
        public static extern int InitComm(int port);

        [DllImport("termb.dll", CharSet = CharSet.Auto)]
        public static extern int InitCommExt();

        [DllImport("termb.dll", CharSet = CharSet.Auto)]
        public static extern int CloseComm();

        [DllImport("termb.dll", CharSet = CharSet.Auto)]
        public static extern int Authenticate();

        [DllImport("termb.dll", CharSet = CharSet.Auto)]
        public static extern int Read_Content(int active);

        [DllImport("termb.dll", CharSet = CharSet.Auto)]
        public static extern int Read_Content_Path(ref string path, int active);

        [DllImport("termb.dll", CharSet = CharSet.Auto)]
        public static extern int GetSAMID(ref string id);

        //int __stdcall GetRFID(LPDWORD id);
        [DllImport("termb.dll", CharSet = CharSet.Auto)]
        public static extern int GetRFID(ref UInt32 id);

        //void __stdcall LedOnOff(int ledid, bool onoff);
        [DllImport("termb.dll", CharSet = CharSet.Auto)]
        public static extern void LedOnOff(Int32 ledid, bool onoff);

        //void __stdcall BuzzerOnOff(int onoff);
        [DllImport("termb.dll", CharSet = CharSet.Auto)]
        public static extern void BuzzerOnOff(int onoff);

        #endregion

        public static byte[] StrToByteArray(string str)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        public static string ByteArrayToStr(byte[] barr)
        {
            System.Text.UnicodeEncoding encoding = new UnicodeEncoding();
            //ASCIIEncoding encoding = new ASCIIEncoding();
            return encoding.GetString(barr);
        }
    }
}
