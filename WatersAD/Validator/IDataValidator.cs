namespace WatersAD.Validator
{
    public interface IDataValidator
    {
        string NameError { get; set; }
        string EmailError { get; set; }
        string PhoneError { get; set; }
        string PasswordError { get; set; }

        string AddressError { get; set; }
        Task<bool> Validate(string firstName, string lastName, string address, string phoneNumber);

        bool ValidateEmail(string email);

        bool ValidatePassword(string password);
    }
}
