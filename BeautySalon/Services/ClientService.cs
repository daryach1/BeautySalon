using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BeautySalon.Services
{
    class ClientService: IDataErrorInfo
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string this[string columnName]
        {
            get 
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "LastName":
                        if (!Regex.IsMatch(LastName, "^[a-zA-Zа-яА-Я ]*$"))
                            error = "Введите только буквы, пробел или дефис";
                        break;
                    case "FirstName":
                        if (!Regex.IsMatch(FirstName, "^[a-zA-Zа-яА-Я ]*$"))
                            error = "Введите только буквы, пробел или дефис";
                        break;
                    case "Patronymic":
                        if (!Regex.IsMatch(Patronymic, "^[a-zA-Zа-яА-Я ]*$"))
                            error = "Введите только буквы, пробел или дефис";
                        break;
                    case "Email":
                        if (!Regex.IsMatch(Email, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                            error = "Введите правильный email";
                        break;
                    default:
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
