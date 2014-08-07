using RateMyRental.Entities;
using RateMyRental.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

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

        /// <summary>
        /// Gets a list of all Administrators e-mails
        /// </summary>
        /// <returns>List of e-mails</returns>
        public List<string> GetAdminEmailsList()
        {
            List<string> emails = new List<string>();
            var v = GetAllUsers().Where(m => m.isAdmin == true);
            foreach (var user in v)
            {
                emails.Add(user.email);
            }
            return emails;
        }
        #endregion

        #region Resources
        /// <summary>
        /// Get ResourceHeading object by ID
        /// </summary>
        /// <param name="ID">ID of ResourceHeading</param>
        public ResourceHeading GetResourceHeadingByID(int ID)
        {
            var v = from rh in dbc.ResourceHeadings where rh.ID == ID select rh;
            return v.FirstOrDefault();
        }

        /// <summary>
        /// Add a new ResourceHeading to the database
        /// </summary>
        /// <param name="rh">ResourceHeading object to be added</param>
        public void AddResourceHeading(ResourceHeading rh)
        {
            dbc.Entry(rh).State = EntityState.Added;
            Save();
        }

        /// <summary>
        /// Delete ResourceHeading object
        /// </summary>
        /// <param name="ID">ID of ResourceHeading</param>
        public void DeleteResourceHeading(int ID)
        {
            ResourceHeading rh = GetResourceHeadingByID(ID);
            dbc.Entry(rh).State = EntityState.Deleted;
            Save();
        }

        /// <summary>
        /// Update ResourceHeading object
        /// </summary>
        /// <param name="rh">ResourceHeading object to be updated</param>
        public void UpdateResourceHeading(ResourceHeading rh)
        {
            dbc.Entry(rh).State = EntityState.Modified;
            Save();
        }

        /// <summary>
        /// Gets all ResourceHeadings from database
        /// </summary>
        /// <returns>IEnumerable of ResourceHeadings</returns>
        public IEnumerable<ResourceHeading> GetAllResourceHeadings()
        {
            var v = from rh in dbc.ResourceHeadings select rh;
            return v;
        }

        /// <summary>
        /// Get a SelectList of all ResourceHeadings
        /// </summary>
        /// <returns>SelectList of all ResourceHeadings; Text = headingText, Value = ID</returns>
        public List<SelectListItem> GetResourceHeadingList()
        {
            var v = GetAllResourceHeadings();
            List<SelectListItem> resourceHeadingsList = new List<SelectListItem>();
            foreach (var rh in v)
            {
                SelectListItem newItem = new SelectListItem { Text = rh.headingText, Value = rh.ID.ToString() };
                resourceHeadingsList.Add(newItem);
            }
            return resourceHeadingsList;
        }

        /// <summary>
        /// Gets a string list of all current ResourceHeadings
        /// </summary>
        /// <returns>List<string> of all ResourceHeadings</string></returns>
        public List<string> GetCurrentResourceHeadingsList()
        {
            var v = GetAllResourceHeadings();
            List<string> currentResourceHeadingsList = new List<string>();
            foreach (var rh in v)
            {
                string s = rh.headingText;
                currentResourceHeadingsList.Add(s);
            }
            return currentResourceHeadingsList;
        }

        /// <summary>
        /// Get Resource object from database by ID
        /// </summary>
        /// <param name="resourceID">ID of Resource</param>
        /// <returns>Resource object</returns>
        public Resource GetResourceByID(int resourceID)
        {
            var v = from r in dbc.Resources where r.ID == resourceID select r;
            foreach (var resource in v)
            {
                resource.ResourceHeading = GetResourceHeadingByID(resource.ResourceType);
            }
            return v.FirstOrDefault();
        }

        /// <summary>
        /// Gets all Resource objects from the database
        /// </summary>
        /// <returns>IEnumerable of all Resources</returns>
        public IEnumerable<Resource> GetAllResources()
        {
            var v = from r in dbc.Resources select r;
            return v;
        }

        /// <summary>
        /// Add new Resource object to database
        /// </summary>
        /// <param name="resource">Resource object to be added</param>
        public void AddResource(Resource resource)
        {
            resource.FileName = resource.FileName.Replace("http://", "");
            resource.FileName = resource.FileName.Replace("https://", "");
            dbc.Entry(resource).State = EntityState.Added;
            Save();
        }

        /// <summary>
        /// Delete Resource object from database
        /// </summary>
        /// <param name="resourceID">ID of Resource</param>
        public void DeleteResource(int resourceID)
        {
            Resource resource = GetResourceByID(resourceID);
            dbc.Entry(resource).State = EntityState.Deleted;
            Save();
        }

        /// <summary>
        /// Updates Resource object in database
        /// </summary>
        /// <param name="resource">Updated Resource</param>
        public void UpdateResource(Resource resource)
        {
            resource.UploadDate = DateTime.Now;
            resource.FileName = resource.FileName.Replace("http://", "");
            resource.FileName = resource.FileName.Replace("https://", "");
            dbc.Entry(resource).State = EntityState.Modified;
            Save();
        }

        /// <summary>
        /// Gets a list of all allowed file extensions
        /// </summary>
        /// <returns>List of allowed extension strings</returns>
        public List<string> GetAllowedExtensionsList()
        {
            List<string> allowedExtensions = new List<string>();
            //Image types
            allowedExtensions.Add(".jpg");
            allowedExtensions.Add(".png");
            allowedExtensions.Add(".gif");
            //Document types
            allowedExtensions.Add(".pdf");
            allowedExtensions.Add(".doc");
            allowedExtensions.Add(".docx");
            allowedExtensions.Add(".xls");
            allowedExtensions.Add(".xlsx");
            allowedExtensions.Add(".odf");
            allowedExtensions.Add(".txt");
            return allowedExtensions;
        }

        /// <summary>
        /// Checks whether or not a specified ResourceHeading exists or not
        /// </summary>
        /// <param name="resourceHeadingTitle">ResourceHeading title</param>
        /// <returns>True = ResourceHeading exists, False = ResourceHeading does not exist</returns>
        public bool CheckForResourceHeading(string resourceHeadingTitle)
        {
            var v = from rh in dbc.ResourceHeadings where rh.headingText.ToLower().Trim() == resourceHeadingTitle.ToLower().Trim() select rh;
            if (v != null && v.Count() > 0)
            {
                //ResourceHeading already exists
                return true;
            }
            else
            {
                //ResourceHeading does not exist
                return false;
            }
        }

        /// <summary>
        /// Check the Resources table for references to a given ResourceHeading
        /// </summary>
        /// <param name="resourceHeadingID">ID of ResourceHeading</param>
        /// <returns>True = There are some Resource dependencies, False = There are no Resource dependencies</returns>
        public bool CheckForResourceDependencies(int resourceHeadingID)
        {
            var v = from r in dbc.Resources where r.ResourceType == resourceHeadingID select r;
            //There are Resources that rely on this ResourceHeading
            if (v != null && v.Count() > 0)
            {
                return true;
            }
            //There are no Resources that rely on this ResourceHeading
            else
            {
                return false;
            }
        }
        #endregion

        #region Reviews
        /// <summary>
        /// Gets all State objects
        /// </summary>
        /// <returns>IEnumerable of all States</returns>
        public IEnumerable<State> GetAllStates()
        {
            var v = from s in dbc.States select s;
            return v;
        }

        /// <summary>
        /// Gets State object by ID
        /// </summary>
        /// <param name="ID">ID of State to get</param>
        /// <returns>State object</returns>
        public State GetStateByID(int ID)
        {
            var v = from s in dbc.States where s.ID == ID select s;
            return v.FirstOrDefault();
        }

        /// <summary>
        /// Gets List of States
        /// </summary>
        /// <returns>List of States</returns>
        public List<SelectListItem> GetStatesList()
        {
            var v = GetAllStates();
            List<SelectListItem> statesList = new List<SelectListItem>();
            foreach (var state in v)
            {
                SelectListItem newItem = new SelectListItem { Text = state.StateName, Value = state.ID.ToString() };
                statesList.Add(newItem);
            }
            return statesList;
        }

        /// <summary>
        /// Gets all City objects
        /// </summary>
        /// <returns>IEnumerable of all Cities</returns>
        public IEnumerable<City> GetAllCities()
        {
            var v = from c in dbc.Cities select c;
            return v;
        }

        /// <summary>
        /// Gets City object by ID
        /// </summary>
        /// <param name="ID">ID of City to get</param>
        /// <returns>City object</returns>
        public City GetCityByID(int ID)
        {
            var v = from c in dbc.Cities where c.ID == ID select c;
            return v.FirstOrDefault();
        } 

        /// <summary>
        /// Gets a List of all City objects
        /// </summary>
        /// <returns>List of Cities</returns>
        public List<SelectListItem> GetCitiesList()
        {
            var v = GetAllCities();
            List<SelectListItem> citiesList = new List<SelectListItem>();
            foreach (var city in v)
            {
                SelectListItem newItem = new SelectListItem { Text = city.CityName, Value = city.ID.ToString() };
                citiesList.Add(newItem);
            }
            return citiesList;
        }

        /// <summary>
        /// Gets all Properties
        /// </summary>
        /// <returns>IEnumerable of all Properties</returns>
        public IEnumerable<Property> GetAllProperties()
        {
            var v = from p in dbc.Properties select p;
            //Assign City and State objects
            foreach (var property in v)
            {
                property.State = GetStateByID(property.StateID);
                property.City = GetCityByID(property.CityID);
            }
            return v;
        }

        /// <summary>
        /// Adds new Property object to database
        /// </summary>
        /// <param name="property">Property to add</param>
        public void AddProperty(Property property)
        {
            dbc.Entry(property).State = EntityState.Added;
            Save();
        }

        /// <summary>
        /// Deletes Property object from database
        /// </summary>
        /// <param name="ID">ID of Property to delete</param>
        public void DeleteProperty(int ID)
        {
            Property property = GetPropertyByID(ID);
            dbc.Entry(property).State = EntityState.Deleted;
            Save();
        }

        /// <summary>
        /// Updates Property object
        /// </summary>
        /// <param name="property">Updated Property object</param>
        public void UpdateProperty(Property property)
        {
            dbc.Entry(property).State = EntityState.Modified;
            Save();
        }

        /// <summary>
        /// Gets Property object by ID
        /// </summary>
        /// <param name="ID">ID of Property object</param>
        /// <returns>Property object</returns>
        public Property GetPropertyByID(int ID)
        {
            var v = GetAllProperties().Where(p => p.ID == ID);
            return v.FirstOrDefault();
        }
        #endregion

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