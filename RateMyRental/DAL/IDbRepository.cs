using RateMyRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

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
        ResourceHeading GetResourceHeadingByID(int ID);
        void AddResourceHeading(ResourceHeading rh);
        void DeleteResourceHeading(int ID);
        void UpdateResourceHeading(ResourceHeading rh);
        IEnumerable<ResourceHeading> GetAllResourceHeadings();
        List<SelectListItem> GetResourceHeadingList();
        List<string> GetCurrentResourceHeadingsList();
        Resource GetResourceByID(int resourceID);
        IEnumerable<Resource> GetAllResources();
        void AddResource(Resource resource);
        void DeleteResource(int resourceID);
        void UpdateResource(Resource resource);
        List<string> GetAllowedExtensionsList();
        bool CheckForResourceHeading(string resourceHeadingTitle);
        bool CheckForResourceDependencies(int resourceHeadingID);
        #endregion

        #region Misc
        void Save();
        #endregion
    }
}