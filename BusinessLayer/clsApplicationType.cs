using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsApplicationType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { set; get; }
        public string Title { set; get; }
        public float Fees { set; get; }


        public clsApplicationType()
        {
            this.ID = -1;
            this.Title = "";
            this.Fees = 0;
            Mode = enMode.AddNew;
        }
        public clsApplicationType(int ID, string ApplicationTypeTitle, float ApplicationTypeFees)
        {
            this.ID = ID;
            this.Title = ApplicationTypeTitle;
            this.Fees = ApplicationTypeFees;
            Mode = enMode.Update;
        }


        private bool _AddNewApplicationType()
        {
            this.ID = clsApplicationTypesData.AddNewApplicationType(Title, Fees);
            return (this.ID != -1);
        }
        private bool _UpdateApplicationType()
        {
            return clsApplicationTypesData.UpdateApplicationType(this.ID, this.Title, this.Fees);
        }

        static public DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesData.GetAllApplicationTypes();
        }
        public static clsApplicationType Find(int ID)
        {
            string Title = ""; float Fees = 0;

            if (clsApplicationTypesData.GetApplicationTypeInfoByID((int)ID, ref Title, ref Fees))

                return new clsApplicationType(ID, Title, Fees);
            else
                return null;

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplicationType();

            }

            return false;
        }


    }
}
