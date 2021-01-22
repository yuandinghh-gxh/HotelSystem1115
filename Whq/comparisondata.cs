using System;
using System.Collections.Generic;

namespace Whq {
	class comparisondata {
        public int id { get; set; }            //index
        public string tempnum { get; set; }  //硬件 编号
		public  string cabinetnum { get; set; }   // 机柜编号
		public DevComponents.DotNetBar.LabelX lcabinetnum;
		public DevComponents.DotNetBar.LabelX ltempnow;
		public DevComponents.DotNetBar.LabelX ltempl;
		public DevComponents.DotNetBar.LabelX ltemph;
		public DevComponents.DotNetBar.LabelX ltempalac;  //报警次数
		public DateTime time;
		//public static implicit operator comparisondata(Dictionary<int, comparisondata>.ValueCollection v) {
		//	throw new NotImplementedException();
		//}

		//    public comparisondata Value { get; internal set; }
	}
	public struct    scdata {
		public int id { get; set; }            //index
		public string tempnum { get; set; }  //硬件 编号
		public string cabinetnum { get; set; }   // 机柜编号
		public DevComponents.DotNetBar.LabelX lcabinetnum;
		public DevComponents.DotNetBar.LabelX ltempnow;
		public DevComponents.DotNetBar.LabelX ltempl;
		public DevComponents.DotNetBar.LabelX ltemph;
		public DevComponents.DotNetBar.LabelX ltempalac;  //报警次数
		public DateTime time;
		public bool newdata;

	}
}
