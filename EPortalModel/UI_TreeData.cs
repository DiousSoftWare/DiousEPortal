using System;
using System.Collections.Generic;
using System.Text;

namespace EPortalModel
{
    public class MainMenus
    {
        private string funcID;
        public string FuncID
        {
            get { return funcID; }
            set { funcID = value; }
        }

        private string parentID;
        public string ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }

        private string funcNam;
        public string FuncNam
        {
            get { return funcNam; }
            set { funcNam = value; }
        }

        private string frmNam;
        public string FrmNam
        {
            get { return frmNam; }
            set { frmNam = value; }
        }

    }

    public class FltSlt
    {
        private string fSltID;
        public string FSltID
        {
            get { return fSltID; }
            set { fSltID = value; }
        }

        private string fparentID;
        public string FParentID
        {
            get { return fparentID; }
            set { fparentID = value; }
        }

        private string fSltName;
        public string FSltName
        {
            get { return fSltName; }
            set { fSltName = value; }
        }

        private string fUsrID;
        public string FUsrID
        {
            get { return fUsrID; }
            set { fUsrID = value; }
        }

        private string fFrmName;
        public string FFrmName
        {
            get { return fFrmName; }
            set { fFrmName = value; }
        }

        private int fSltType;
        public int FSltType
        {
            get { return fSltType; }
            set { fSltType = value; }
        }
    }
}