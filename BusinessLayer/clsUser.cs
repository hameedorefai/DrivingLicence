using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLayer.clsPerson;

namespace BusinessLayer
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int UserID { set; get; }
        public int PersonID { set; get; }
        public clsPerson PersonInfo;
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }

        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;
            Mode = enMode.AddNew;
        }

        public clsUser(int userId, int personID, string username, string password, bool IsActive)
        {
            this.UserID = userId;
            this.PersonID = personID;
            this.PersonInfo = clsPerson.Find(personID);
            this.UserName = username;
            this.Password = password ;
            this.IsActive = IsActive;
            Mode = enMode.Update;
        }

        static private bool _CheckConfirmedPassword(string NewPassword, string ConfirmedNewPassword)
        {
            return NewPassword == ConfirmedNewPassword;
        }
        public bool isActive(int ID)
        {
            return clsUserData.isActive(ID);
        }
        public bool isActive(string username)
        {
            return clsUserData.isActive(username);
        }
        public static clsUser FindUserByID(int userID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            if (clsUserData.GetUserInfoByUserID(userID, ref PersonID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(userID,PersonID,UserName, Password, IsActive);
                // this.UserID = userID;
            }
                return null;
            //return this;
        }
        public static clsUser FindByPersonID(int personID)
        {
            int userID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            if (clsUserData.GetUserInfoByPersonID(personID, ref userID, ref UserName, ref Password, ref IsActive))
            {
                // this.PersonID = personID;
                return new clsUser(userID, personID, UserName, Password, IsActive);

            }
            return null;
            // return this;
        }
        public static clsUser FindByUsernameAndPassword(string UserName, string Password)
        {
            int UserID = -1;
            int PersonID = -1;
            bool IsActive = false;
            //Password = clsHashingPassword.HashPassword( Password);

            if (clsUserData.GetUserInfoByUsernameAndPassword( ref UserName, ref Password, ref PersonID, ref UserID, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            return null;
        }
        public static DataTable initializeUserDataTable()
        {
            return clsUserData.initializeUserDataTable();
        }
        public bool Activate(int userID)
        {
           return clsUserData.Activate(userID);
        }
        public bool Activate(string username)
        {
            return clsUserData.Activate(username);
        }
        public bool DeActivate(int userID)
        {
            return clsUserData.DeActivate(userID);
        }
        public bool DeActivate(string username)
        {
            return clsUserData.DeActivate(username);
        }
        public static bool UpdatePassword(int userID, string CurrentPassword,string NewPassword
            ,string confirmedNewPassword)
        {
            if (clsUserData.CheckUserIDAndPassword(userID, CurrentPassword))
            {
                if (_CheckConfirmedPassword(NewPassword, confirmedNewPassword))
                {
                    return clsUserData.UpdatePassword(userID, NewPassword);
                }
                return false;
            }
            else
                return false;
        }
        public static bool DeleteUser(int userID)
        {
            return clsUserData.DeleteUser(userID);
        }
        /*public */private int AddNewUserByDataTable(string username, string password,string confirmedpassword,bool isActive)
        {
            if (_CheckConfirmedPassword(password, confirmedpassword))
            {
                try
                {
                    DataTable dataTable = clsUserData.initializeUserDataTable();
                    DataRow newRow = dataTable.NewRow();
                    newRow["PersonID"] = this.PersonID;
                    newRow["UserName"] = username;
                    newRow["Password"] = password;
                    newRow["IsActive"] = isActive;
                    return clsUserData.AddNewUser(newRow);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in clsUser: " + ex.Message);
                }
            }
            return -1;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        /*public */private int AddNewUser(string username, string password, string confirmedpassword, bool isActive)
        {
            if (_CheckConfirmedPassword(password, confirmedpassword))
            {
                return clsUserData.AddNewUser(this.PersonID, username, password, isActive);
            }
            return -1;
        }
        private bool _AddNewUser()
        {
            this.Password = clsHashingPassword.HashPassword( this.Password);

            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (UserID != -1);
        }
        private bool _UpdateUser()
        {
            this.Password = clsHashingPassword.HashPassword(this.Password);
            return clsUserData.UpdateUser(this.UserID,this.PersonID, this.UserName, this.Password, this.IsActive);
        }
        /*public */private bool UpdateUser(int UserID,int PersonID, string username, string password, string confirmedpassword, bool isActive)
        {
            if (_CheckConfirmedPassword(password, confirmedpassword))
            {
                return clsUserData.UpdateUser(UserID,PersonID, username, password, isActive);
            }
            return false;
        }

        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }
        public static bool IsUserExist(string username)
        {
            return clsUserData.IsUserExist(username);
        }
        public static bool isUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }

    }
}
