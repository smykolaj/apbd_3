using System;

namespace LegacyApp
{

    public interface IValidator
    {
        bool NameIsFilled(String firstName, String lastName);
        bool EmailIsCorrect(String email);
        bool AgeIsAppropriate(DateTime dateOfBirth);
        bool CreditLimitExists(Client client, User user);
    }

    public class Validator : IValidator
    {
        public bool NameIsFilled(String firstName, String lastName)
        {
            return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName);
        }

        public bool EmailIsCorrect(String email)
        {
            return email.Contains("@") || email.Contains(".");
        }

        public bool AgeIsAppropriate(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            return age >= 21;
        }

        public bool CreditLimitExists(Client client, User user)
        {
            switch (client.Type)
            {
                case "VeryImportantClient" : 
                    user.HasCreditLimit = false;
                    break;
                case "ImportantClient" :
                    using (var userCreditService = new UserCreditService())
                    {
                        var creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                        creditLimit *= 2;
                        user.CreditLimit = creditLimit;
                    }
                    break;
                default :
                    user.HasCreditLimit = true;
                    using (var userCreditService = new UserCreditService())
                    {
                        int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                        user.CreditLimit = creditLimit;
                    }
                    break;
            }
            
            return !user.HasCreditLimit || user.CreditLimit >= 500;
        }

        
    }


    public class UserService
    {
        private readonly IValidator _validator;
        public UserService()
        {
            _validator = new Validator();
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            
            if (!_validator.NameIsFilled(firstName, lastName))
                return false;
            if (!_validator.EmailIsCorrect(email))
                return false;
            if (!_validator.AgeIsAppropriate(dateOfBirth))
                return false;
            

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            
            if (!_validator.CreditLimitExists(client, user))
                return false;
            

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
