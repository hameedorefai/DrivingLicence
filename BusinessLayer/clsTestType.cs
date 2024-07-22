﻿using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsTestType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public clsTestType.enTestType ID { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public float Fees { set; get; }

        public clsTestType()
        {
            this.ID = clsTestType.enTestType.VisionTest;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;
            Mode = enMode.AddNew;
        }
        public clsTestType(clsTestType.enTestType ID, string TestTypeTitel, string Description, float TestTypeFees)
        {
            this.ID = ID;
            this.Title = TestTypeTitel;
            this.Description = Description;

            this.Fees = TestTypeFees;
            Mode = enMode.Update;
        }

       static public DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }
        public bool EditTestTypeTypeTitle(int TestTypeID, string TestTypeTitle)
        {
            return false;
           // return clsApplicationTypesData.EditApplicationTypeTitle(TestTypeID, TestTypeTitle);
        }
        public bool EditTestTypeDescription(int TestTypeID, string TestTypeDescription)
        {
            return false;
            // return clsApplicationTypesData.EditApplicationTypeTitle(TestTypeID, TestTypeDescription);
        }
        public bool EditApplicationTypeFees(int TestTypeID, double TestTypeFees)
        {
            return clsTestTypesData.EditTestTypeFees(TestTypeID,TestTypeFees);
        }

        private bool _AddNewTestType()
        {
            this.ID = (clsTestType.enTestType)clsTestTypesData.AddNewTestType(this.Title, this.Description, this.Fees);
            return (this.Title != "");
        }
        private bool _UpdateTestType()
        {
            return clsTestTypesData.UpdateTestType((int)this.ID, this.Title, this.Description, this.Fees);
        }

        public static clsTestType Find(clsTestType.enTestType TestTypeID)
        {
            string Title = "", Description = ""; float Fees = 0;

            if (clsTestTypesData.GetTestTypeInfoByID((int)TestTypeID, ref Title, ref Description, ref Fees))

                return new clsTestType(TestTypeID, Title, Description, Fees);
            else
                return null;

        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestType();

            }

            return false;
        }

    }
}
