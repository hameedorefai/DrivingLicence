using System;
using System.Data;
using System.Xml.Linq;
using DataLayer;


namespace BusinessLayer
{

    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PersonID;
        public string FirstName;
        public string SecondName;
        public string ThirdName;
        public string LastName;
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }
        public string NationalNo;
        public DateTime DateOfBirth;
        public short Gendor;
        public string Address;
        public string Phone;
        public string Email;    
        public int NationalityCountryID;

        public clsCountry CountryInfo;

        private string _ImagePath;

        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public clsPerson()
        {

            PersonID = -1;
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            NationalNo = "";
            Gendor = 0;
            DateOfBirth = DateTime.Now;
            Email = "";
            NationalityCountryID = -1;
            Address = "";
            Phone = "";
            ImagePath = "";
             Mode = enMode.AddNew;
            
    }

    public clsPerson(int PersonID, string FirstName, string SecondName, string ThirdName,
                                string LastName, string NationalNo, byte Gendor, DateTime DateOfBirth,
                                    string Email, int NationalityCountryID, string Address, string Phone, string ImagePath)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.Gendor = Gendor;
            this.DateOfBirth = DateOfBirth;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
          //  CountryName =  clsCountry.(PersonID);
            this.Address = Address;
            this.Phone = Phone;
            this.ImagePath = ImagePath;
            CountryInfo = clsCountry.Find(NationalityCountryID);
             Mode = enMode.AddNew;

        }

        public DataTable initializePersonDataTable()
        {
            return clsPersonData.initializePersonDataTable();
        }

        static public clsPerson Find(int PersonID)
        {
            clsPerson Person = new clsPerson();
            if (
            clsPersonData.GetPersonInformationByID(PersonID, ref Person.FirstName, ref Person.SecondName,
               ref Person.ThirdName, ref Person.LastName, ref Person.NationalNo, ref Person.Gendor,
               ref Person.DateOfBirth, ref Person.Email, ref Person.NationalityCountryID, ref Person.Address,
               ref Person.Phone, ref Person._ImagePath)
                )
            {
                Person.CountryInfo = clsCountry.Find(PersonID);
                Person.PersonID = PersonID;
            }
            return Person;
        }
        static public clsPerson Find(string NationalNo)
        {
            clsPerson Person = new clsPerson();
            if (clsPersonData.GetPersonInformationByNationalNo(NationalNo, ref Person.PersonID, ref Person.FirstName, ref Person.SecondName,
                ref Person.ThirdName, ref Person.LastName, ref Person.Gendor,
                ref Person.DateOfBirth, ref Person.Email, ref Person.NationalityCountryID, ref Person.Address,
                ref Person.Phone, ref Person._ImagePath)
              )
            {
                Person.CountryInfo = clsCountry.Find(Person.PersonID);
                Person.NationalNo = NationalNo;
            }
            return Person;
        }
        private bool _AddNewPerson(DataTable tbPerson)
        {
            this.PersonID = clsPersonData.AddNewPerson(tbPerson);
            return (this.PersonID != -1);
        }
        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.FirstName,this.SecondName,this.ThirdName,
                this.LastName,this.NationalNo, this.DateOfBirth, this.Gendor,
                this.Address, this.Phone, this.Email,this.NationalityCountryID,this.ImagePath);
            return (this.PersonID != -1);
        }



        private DataTable __UpdatePerson(clsPerson Person, DataTable dtPerson)
        {
            if (dtPerson == null)
                return dtPerson;
            dtPerson.Rows[0]["DateOfBirth"] = Person.DateOfBirth;
            dtPerson.Rows[0]["Gendor"] = Person.Gendor;
            DataRow drPerson = dtPerson.Rows[0];

            // Update fields unconditionally
            drPerson["NationalNo"] = string.IsNullOrEmpty((string)drPerson["NationalNo"]) ? Person.NationalNo : drPerson["NationalNo"];
            drPerson["FirstName"] = string.IsNullOrEmpty((string)drPerson["FirstName"]) ? Person.FirstName : drPerson["FirstName"];
            drPerson["SecondName"] = string.IsNullOrEmpty((string)drPerson["SecondName"]) ? Person.SecondName : drPerson["SecondName"];
            drPerson["ThirdName"] = string.IsNullOrEmpty((string)drPerson["ThirdName"]) ? Person.ThirdName : drPerson["ThirdName"];
            drPerson["LastName"] = string.IsNullOrEmpty((string)drPerson["LastName"]) ? Person.LastName : drPerson["LastName"];
         //   drPerson["DateOfBirth"] = (DateTime)drPerson["DateOfBirth"] == default(DateTime) ? Person.DateOfBirth : drPerson["DateOfBirth"];
          //  drPerson["Gendor"] = (byte)drPerson["Gendor"] == 0 ? Person.Gendor : drPerson["Gendor"];
            drPerson["Address"] = string.IsNullOrEmpty((string)drPerson["Address"]) ? Person.Address : drPerson["Address"];
            drPerson["Phone"] = string.IsNullOrEmpty((string)drPerson["Phone"]) ? Person.Phone : drPerson["Phone"];
            drPerson["Email"] = string.IsNullOrEmpty((string)drPerson["Email"]) ? Person.Email : drPerson["Email"];
          //  drPerson["NationalityCountryID"] = (int)drPerson["NationalityCountryID"] == 0 ? Person.NationalityCountryID : drPerson["NationalityCountryID"];
            drPerson["ImagePath"] = string.IsNullOrEmpty((string)drPerson["ImagePath"]) ? Person.ImagePath : drPerson["ImagePath"];

            return dtPerson;
        }
        public bool _UpdatePersonInfo(int PersonID,DataTable dtPerson)
        {

            // هان لازمك تعمل تشيك للكولمز اللي انعملها انتر فقط فضلت فارغة أثناء الانبوت.. هاي رح تخليها زي ما 
            // أما اللي فيها داتا ولو حرف واحد فرح تستبدل الجديد بالقديم ومن ثم رح تحوّلها وترسلها عالداتااكسس وتعمل ابديت

            clsPerson Person = Find(PersonID);
            dtPerson = __UpdatePerson(Person,dtPerson);
            return clsPersonData.UpdatePerson(PersonID, dtPerson);
        }
        private bool _UpdatePerson()
        {
            //call DataAccess Layer 

            return clsPersonData.UpdatePerson(
                this.PersonID, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.NationalNo, this.DateOfBirth, this.Gendor,
                this.Address, this.Phone, this.Email,
                  this.NationalityCountryID, this.ImagePath);
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;
                case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }
        static public bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        static public bool isExistsByNationalNo(string nationalNo)
        {
         return clsPersonData.IsPersonExist(nationalNo);
        }
        public bool IsPersonExist(int PersonID )
        {
            return clsPersonData.IsPersonExist(PersonID);
        }

        static public DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }
    }
}

