using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSystem1115
{
    class Spell
    {
        public static string GetChineseSpell(string strText)
        {
            byte[] ary = Encoding.GetEncoding("gb2312").GetBytes(strText);
            int chineseInt = ((short)ary[0]*256) + (short)ary[1];
            string letterCode = String.Empty;
            //编码范围对照表
            // 'A'; //45217..45252 
            // 'B'; //45253..45760 
            // 'C'; //45761..46317 
            // 'D'; //46318..46825 
            // 'E'; //46826..47009 
            // 'F'; //47010..47296 
            // 'G'; //47297..47613 

            // 'H'; //47614..48118 
            // 'J'; //48119..49061 
            // 'K'; //49062..49323 
            // 'L'; //49324..49895 
            // 'M'; //49896..50370 
            // 'N'; //50371..50613 
            // 'O'; //50614..50621 
            // 'P'; //50622..50905 
            // 'Q'; //50906..51386 

            // 'R'; //51387..51445 
            // 'S'; //51446..52217 
            // 'T'; //52218..52697 
            //没有U,V 
            // 'W'; //52698..52979 
            // 'X'; //52980..53640 
            // 'Y'; //53689..54480 
            // 'Z'; //54481..55289 

            #region    转字母
            if ((chineseInt >= 45217) && (chineseInt <= 45252))
            {
                letterCode = "A";
            }
            else if ((chineseInt >= 45253) && (chineseInt <= 45760))
            {
                letterCode = "B";
            }
            else if ((chineseInt >= 45761) && (chineseInt <= 46317))
            {
                letterCode = "C";
            }
            else if ((chineseInt >= 46318) && (chineseInt <= 46825))
            {
                letterCode = "D";
            }
            else if ((chineseInt >= 46826) && (chineseInt <= 47009))
            {
                letterCode = "E";
            }
            else if ((chineseInt >= 47010) && (chineseInt <= 47296))
            {
                letterCode = "F";
            }
            else if ((chineseInt >= 47297) && (chineseInt <= 47613))
            {
                letterCode = "G";
            }
            else if ((chineseInt >= 47614) && (chineseInt <= 48118))
            {
                letterCode = "H";
            }
            else if ((chineseInt >= 48119) && (chineseInt <= 49061))
            {
                letterCode = "J";
            }
            else if ((chineseInt >= 49062) && (chineseInt <= 49323))
            {
                letterCode = "K";
            }
            else if ((chineseInt >= 49324) && (chineseInt <= 49895))
            {
                letterCode = "L";
            }
            else if ((chineseInt >= 49896) && (chineseInt <= 50370))
            {
                letterCode = "M";
            }
            else if ((chineseInt >= 50371) && (chineseInt <= 50613))
            {
                letterCode = "N";
            }
            else if ((chineseInt >= 50614) && (chineseInt <= 50621))
            {
                letterCode = "O";
            }
            else if ((chineseInt >= 50622) && (chineseInt <= 50905))
            {
                letterCode = "P";
            }
            else if ((chineseInt >= 50906) && (chineseInt <= 51386))
            {
                letterCode = "Q";
            }
            else if ((chineseInt >= 51387) && (chineseInt <= 51445))
            {
                letterCode = "R";
            }
            else if ((chineseInt >= 51446) && (chineseInt <= 52217))
            {
                letterCode = "S";
            }
            else if ((chineseInt >= 52218) && (chineseInt <= 52697))
            {
                letterCode = "T";
            }
            else if ((chineseInt >= 52698) && (chineseInt <= 52979))
            {
                letterCode = "W";
            }
            else if ((chineseInt >= 52980) && (chineseInt <= 53640))
            {
                letterCode = "X";
            }
            else if ((chineseInt >= 53689) && (chineseInt <= 54480))
            {
                letterCode = "Y";
            }
            else if ((chineseInt >= 54481) && (chineseInt <= 55289))
            {
                letterCode = "Z";
            }
            #endregion
            return letterCode;
        }
    }
}
