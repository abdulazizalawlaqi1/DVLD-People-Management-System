using System;
using System.Data;
using System.Xml.Linq;
using DVLD_DataAccess;


namespace DVLD_Buisness
{
    public  class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PersonID { set; get; }
        public int Gender { set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }
        public string Email { set; get; }
        public string Phone { set; get; }
        public int Nationality { set; get; }
        public DateTime DateOfBirth { set; get; }

        public clsCountry CountryInfo;

        public string ImagePath
        {
            get { return _ImagePath; }
            set
            {
                _ImagePath = value;
            }
        }

        private string _ImagePath;
        public string Address { set; get; }




        public clsPerson()

        {
            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.Gender = 0;
            this.Email = "";
            this.Phone = "";
            this.Nationality = -1;
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.ImagePath = "";

            Mode = enMode.AddNew;

        }

        private clsPerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
          int Gender, DateTime DateOfBirth, int Nationality, string Phone, string Email, string Address, string ImagePath)

        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.DateOfBirth = DateOfBirth;
            this.Nationality = Nationality;
            this.Phone = Phone;
            this.Email = Email;
            this.Address = Address;
            this.CountryInfo = clsCountry.Find(Nationality);
            this.ImagePath = ImagePath;

            Mode = enMode.Update;

        }

        private bool _AddNewPerson()
        {
            //call DataAccess Layer 

            this.PersonID = clsPersonData.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.Gender,
               this.DateOfBirth, this.Nationality, this.Phone, this.Email, this.Address,
               this.ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            //call DataAccess Layer 

            return clsPersonData.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.Gender,
               this.DateOfBirth, this.Nationality, this.Phone, this.Email, this.Address,
               this.ImagePath);

        }

        public static clsPerson FindByPersonID(int PersonID)
        {
            int Gender = 0, Nationality = 0;
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", ImagePath = "", Address = "";
            DateTime DateOfBirth = DateTime.Now;


            if (clsPersonData.GetPeopleInfoByPersonID(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
          ref Gender, ref DateOfBirth, ref Nationality, ref Phone, ref Email, ref Address, ref ImagePath))

                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, Gender, DateOfBirth, Nationality, Phone, Email, Address, ImagePath);
            else
                return null;
        }

        public static clsPerson FindByNationalNo(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", ImagePath = "", Address = "";
            DateTime DateOfBirth = DateTime.Now;
            int PersonID = -1, Gender = 0, Nationality = 0;


            if (clsPersonData.GetPeopleInfoByNationalNo(ref PersonID, NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
          ref Gender, ref DateOfBirth, ref Nationality, ref Phone, ref Email, ref Address, ref ImagePath))

                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, Gender, DateOfBirth, Nationality, Phone, Email, Address, ImagePath);
            else
                return null;
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
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();

            }


            return false;
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        public static bool isPersonExist(int PersonID)
        {
            return clsPersonData.IsPersonExist(PersonID);
        }

        public static bool isPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExist(NationalNo);
        }
    }
}
