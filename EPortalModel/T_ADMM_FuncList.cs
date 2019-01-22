using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;

namespace EPortalModel 
{
 
    
	public class T_ADMM_FuncList
	{
		private string funcID;
		public string FuncID
		{
			get { return funcID; }
			set { funcID = value; }
		}
	
		private string funcNam;
		public string FuncNam
		{
			get { return funcNam; }
			set { funcNam = value; }
		}
	
		private string parentID;
		public string ParentID
		{
			get { return parentID; }
			set { parentID = value; }
		}
	
		private int levels;
		public int Levels
		{
			get { return levels; }
			set { levels = value; }
		}
	
		private string frmNam;
		public string FrmNam
		{
			get { return frmNam; }
			set { frmNam = value; }
		}
	
		private string showType;
		public string ShowType
		{
			get { return showType; }
			set { showType = value; }
		}

        private string ParentName;
        public string ParentNam
        {
            get { return ParentName; }
            set { ParentName = value; }
        }

        private string GrpID;
        public string GroupID
        {
            get { return GrpID; }
            set { GrpID = value; }
        }
    }
}