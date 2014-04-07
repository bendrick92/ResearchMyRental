using RateMyRental.Entities;
using RateMyRental.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RateMyRental.DAL
{
    public class DbRepository : IDbRepository
    {
        //DbContext object
        EfDbContext dbc = new EfDbContext();

        #region Account
        /// <summary>
        /// Adds new User to the database
        /// </summary>
        /// <param name="user">User object to be added to database</param>
        public void AddUser(User user)
        {
            //user.ID is not auto-iterating?!  Is 0?ll
            dbc.Entry(user).State = System.Data.Entity.EntityState.Added;
            Save();
        }

        /// <summary>
        /// Deletes User from database
        /// </summary>
        /// <param name="userID">ID of user to be deleted</param>
        public void DeleteUser(int userID)
        {
            User user = GetUserByID(userID);
            dbc.Entry(user).State = System.Data.Entity.EntityState.Deleted;
            Save();
        }

        /// <summary>
        /// Update existing User object
        /// </summary>
        /// <param name="user">User object to be updated</param>
        public void UpdateUser(User user)
        {
            dbc.Entry(user).State = EntityState.Modified;
            Save();
        }

        /// <summary>
        /// Gets User from database
        /// </summary>
        /// <param name="userID">ID of user to be returned</param>
        /// <returns>User object</returns>
        public User GetUserByID(int userID)
        {
            var v = from u in dbc.Users where u.ID == userID select u;
            return v.FirstOrDefault();
        }

        /// <summary>
        /// Gets User from database based on e-mail (username)
        /// </summary>
        /// <param name="username">E-mail of user to be returned</param>
        /// <returns>User object</returns>
        public User GetUserByUsername(string username)
        {
            var v = from u in dbc.Users where u.email == username select u;
            return v.FirstOrDefault();
        }

        /// <summary>
        /// Returns all Users from database
        /// </summary>
        /// <returns>IEnumerable of all Users from database</returns>
        public IEnumerable<User> GetAllUsers()
        {
            var v = from u in dbc.Users select u;
            return v;
        }

        /// <summary>
        /// Sets the isActive field for the given userID to true
        /// </summary>
        /// <param name="userID">ID of User to set as active</param>
        public void ActivateUser(int userID)
        {
            User user = GetUserByID(userID);
            user.isActive = true;
            UpdateUser(user);
        }

        /// <summary>
        /// Checks to see if the User has activated account yet
        /// </summary>
        /// <param name="userID">ID of user to check active</param>
        /// <returns>If user is active</returns>
        public bool CheckIfUserIsActive(int userID)
        {
            User user = GetUserByID(userID);
            if (user.isActive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds Registration to database
        /// </summary>
        /// <param name="registration">Registration object to be added</param>
        public void AddRegistration(Registration registration)
        {
            dbc.Entry(registration).State = System.Data.Entity.EntityState.Added;
            Save();
        }

        /// <summary>
        /// Deletes Registration from database
        /// </summary>
        /// <param name="registrationID">ID of Registration object to be deleted</param>
        public void DeleteRegistration(int registrationID)
        {
            Registration registration = GetRegistrationByID(registrationID);
            dbc.Entry(registration).State = System.Data.Entity.EntityState.Deleted;
            Save();
        }

        /// <summary>
        /// Get Registration from database 
        /// </summary>
        /// <param name="registrationID">ID of Registration object to be returned</param>
        /// <returns>Registration object</returns>
        public Registration GetRegistrationByID(int registrationID)
        {
            var v = from r in dbc.Registrations where r.ID == registrationID select r;
            return v.FirstOrDefault();
        }

        /// <summary>
        /// Get Registration object from database based on random token
        /// </summary>
        /// <param name="token">Token of Registration to be retrieved</param>
        /// <returns>Registration object matching unique token</returns>
        public Registration GetRegistrationByToken(string token)
        {
            var v = from r in dbc.Registrations where r.token == token select r;
            return v.FirstOrDefault();
        }

        /// <summary>
        /// Returns all Registration objects from database
        /// </summary>
        /// <returns>IEnumerable of all Registration objects from database</returns>
        public IEnumerable<Registration> GetAllRegistrations()
        {
            var v = from r in dbc.Registrations select r;
            return v;
        }

        /// <summary>
        /// Checks to see if given e-mail is already registered with system
        /// </summary>
        /// <param name="email">E-mail address to check</param>
        /// <returns>If e-mail is in use</returns>
        public bool CheckIfEmailInUse(string email)
        {
            var v = from u in dbc.Users where u.email.ToLower() == email.ToLower() select u;
            if (v.Count() > 0 && v != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds new Domain object to database
        /// </summary>
        /// <param name="domain">Domain object to be added to database</param>
        public void AddDomain(Domain domain)
        {
            dbc.Entry(domain).State = System.Data.Entity.EntityState.Added;
            Save();
        }

        /// <summary>
        /// Deletes Domain object from database
        /// </summary>
        /// <param name="domainID">ID of Domain object to be deleted</param>
        public void DeleteDomain(int domainID)
        {
            Domain domain = GetDomainByID(domainID);
            dbc.Entry(domain).State = System.Data.Entity.EntityState.Deleted;
            Save();
        }

        /// <summary>
        /// Get Domain from database
        /// </summary>
        /// <param name="domainID">ID of Domain object to be returned</param>
        /// <returns>Domain object</returns>
        public Domain GetDomainByID(int domainID)
        {
            var v = from d in dbc.Domains where d.ID == domainID select d;
            return v.FirstOrDefault();
        }

        /// <summary>
        /// Get all Domain objects from database
        /// </summary>
        /// <returns>IEnumerable of all Domain objects from database</returns>
        public IEnumerable<Domain> GetAllDomains()
        {
            var v = from d in dbc.Domains select d;
            return v;
        }
        #endregion

        /// <summary>
        /// Get all DomainNames from database
        /// </summary>
        /// <returns>List of DomainNames</returns>
        public List<string> GetAllDomainNames()
        {
            var v = from d in dbc.Domains select d.DomainName;
            return v.ToList();
        }

        /// <summary>
        /// Adds new PasswordResetRequest object to database
        /// </summary>
        /// <param name="prr">PasswordResetRequest to be added</param>
        public void AddPasswordResetRequest(PasswordResetRequest prr)
        {
            dbc.Entry(prr).State = EntityState.Added;
            Save();
        }

        /// <summary>
        /// Deletes PasswordResetRequest object from database
        /// </summary>
        /// <param name="prrID">ID of PasswordResetRequest object to be deleted</param>
        public void DeletePasswordResetRequest(int prrID)
        {
            PasswordResetRequest prr = GetPasswordResetRequestByID(prrID);
            dbc.Entry(prr).State = EntityState.Deleted;
            Save();
        }

        public void DeletePasswordResetRequestsForUser(int userID)
        {
            var v = from p in dbc.PasswordResetRequests where p.UserID == userID select p;
            foreach (var rr in v)
            {
                dbc.Entry(rr).State = EntityState.Deleted;
            }
            Save();
        }

        /// <summary>
        /// Gets PasswordResetRequest object from database
        /// </summary>
        /// <param name="prrID">ID of PasswordResetRequest object</param>
        /// <returns>PasswordResetRequest object</returns>
        public PasswordResetRequest GetPasswordResetRequestByID(int prrID)
        {
            var v = from p in dbc.PasswordResetRequests where p.ID == prrID select p;
            return v.FirstOrDefault();
        }

        /// <summary>
        /// Gets PasswordResetRequest object from database
        /// </summary>
        /// <param name="token">Token of PasswordResetRequest object</param>
        /// <returns>PasswordResetRequest object</returns>
        public PasswordResetRequest GetPasswordResetRequestByToken(string token)
        {
            var v = from p in dbc.PasswordResetRequests where p.Token == token select p;
            return v.FirstOrDefault();
        }

        #region Misc
        /// <summary>
        /// Saves changes made to EfDbContext
        /// </summary>
        public void Save()
        {
            dbc.SaveChanges();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}