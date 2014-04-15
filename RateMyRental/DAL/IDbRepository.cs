using RateMyRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateMyRental.DAL
{
    public interface IDbRepository : IDisposable
    {
        #region Account
        void AddUser(User user);
        void DeleteUser(int userID);
        User GetUserByID(int userID);
        User GetUserByUsername(string username);
        IEnumerable<User> GetAllUsers();
        void ActivateUser(int userID);
        bool CheckIfUserIsActive(int userID);
        void AddRegistration(Registration registration);
        void DeleteRegistration(int registrationID);
        Registration GetRegistrationByID(int registrationID);
        Registration GetRegistrationByToken(string token);
        IEnumerable<Registration> GetAllRegistrations();
        bool CheckIfEmailInUse(string email);
        void AddDomain(Domain domain);
        void DeleteDomain(int domainID);
        Domain GetDomainByID(int domainID);
        IEnumerable<Domain> GetAllDomains();
        List<string> GetAllDomainNames();
        void AddPasswordResetRequest(PasswordResetRequest prr);
        void DeletePasswordResetRequest(int prrID);
        void DeletePasswordResetRequestsForUser(int userID);
        PasswordResetRequest GetPasswordResetRequestByID(int prrID);
        PasswordResetRequest GetPasswordResetRequestByToken(string token);
        #endregion

        #region Resources
        void AddResourceHeading(ResourceHeading rh);
        IEnumerable<ResourceHeading> GetAllResourceHeadings();
        #endregion

        #region Misc
        void Save();
        #endregion
    }
}