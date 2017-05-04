using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Linq;

namespace 卡密生成器
{
    public class 卡密生成
    {
        private int nums = 0;
        private List<string> cardList = new List<string>();
        public List<string> Make(string qz, string hz, int num)
        {
            nums = num;
            ThreadMake(qz,hz,num);
            return cardList;

        }
        /// <summary>
        /// 卡密生成 根据时间做规则 来生成 确保 不会重复
        /// </summary>
        /// <param name="num">生成数量</param>
        /// <returns>string集合</returns>
        public void ThreadMake(string qz,string hz,int num)
        {
            Random rd = new Random();
            for (int i = 0; i < num; i++)
            {
                cardList.Add(qz+rd.Next(1000, 9999).ToString()+GetTime()+ rd.Next(100, 999).ToString()+hz);
            }
            cardList = cardList.Distinct().ToList();
            ///如果有重复的
            if (cardList.Count != nums)
            {
                ThreadMake(qz,hz,num - cardList.Count);
            }
        }
        public static string GetTime()
        {
            string time = DateTime.Now.ToString("yyMMddHHmmssf");
            return time;
        }
    }
}
