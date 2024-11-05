using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace WatersAD.Validator
{
    public class DataValidator : IDataValidator
    {

        public string NameError { get; set; } = "";
        public string EmailError { get; set; } = "";
        public string PhoneError { get; set; } = "";
        public string PasswordError { get; set; } = "";

        public string AddressError { get; set; } = "";

        private const string NameEmptyErrorMsg = "Por favor, informe o seu nome.";
        private const string NameInvalidErrorMsg = "Por favor, informe um nome válido.";
        private const string AddressEmptyErrorMsg = "Por favor, informe a sua morada.";
        private const string AddressInvalidErrorMsg = "Por favor, informe uma morada válida.";
        private const string EmailEmptyErrorMsg = "Por favor, informe um email.";
        private const string EmailInvalidErrorMsg = "Por favor, informe um email válido.";
        private const string PhoneEmptyErrorMsg = "Por favor, informe um telefone.";
        private const string PhoneInvalidErrorMsg = "Por favor, informe um telefone válido.";
        private const string PasswordEmptyErrorMsg = "Por favor, informe a senha.";
        private const string PasswordInvalidErrorMsg = "A senha deve conter pelo menos 8 caracteres, incluindo letras e números.";

        public Task<bool> Validate(string firstName, string lastName, string address, string phoneNumber)
        {
            var isFirstNameValid = ValidateFirstName(firstName);
            var isLastNameValid = ValidateFirstName(lastName);
            var isAddressValid = ValidateAddress(address);
            var isPhoneValid = ValidatePhoneNumber(phoneNumber);
            

            return Task.FromResult(isFirstNameValid && isLastNameValid && isPhoneValid && isAddressValid);
        }

        private bool ValidateAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                AddressError = AddressEmptyErrorMsg;
                return false;
            }

            if (address.Length < 3)
            {
                AddressError = AddressInvalidErrorMsg;
                return false;
            }

            NameError = "";
            return true;
        }

        private bool ValidateFirstName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                NameError = NameEmptyErrorMsg;
                return false;
            }

            if (name.Length < 3)
            {
                NameError = NameInvalidErrorMsg;
                return false;
            }

            NameError = "";
            return true;
        }

        public bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                EmailError = EmailEmptyErrorMsg;
                return false;
            }

            if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                EmailError = EmailInvalidErrorMsg;
                return false;
            }

            EmailError = "";
            return true;
        }

        private bool ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                PhoneError = PhoneEmptyErrorMsg;
                return false;
            }

            if (phoneNumber.Length < 9)
            {
                PhoneError = PhoneInvalidErrorMsg;
                return false;
            }

            PhoneError = "";
            return true;
        }

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                PasswordError = PasswordEmptyErrorMsg;
                return false;
            }

            if (password.Length < 8 || !Regex.IsMatch(password, @"[a-zA-Z]") || !Regex.IsMatch(password, @"\d"))
            {
                PasswordError = PasswordInvalidErrorMsg;
                return false;
            }

            PasswordError = "";
            return true;
        }
    }
}
