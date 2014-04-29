using RateMyRental.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateMyRental.Controllers
{
    public class ResourcesController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            Resources_IndexViewModel model = new Resources_IndexViewModel();
            model.resourceHeadings = repo.GetAllResourceHeadings();
            model.resources = repo.GetAllResources();
            return View(model);
        }

        public ActionResult AddResource()
        {
            AddResourceViewModel model = new AddResourceViewModel();
            model.resource = new Entities.Resource();
            model.resourceHeadingsList = repo.GetResourceHeadingList();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddResource(AddResourceViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Should there be a file?
                if (!(model.isURL))
                {
                    model.resource.IsURL = false;

                    //Is there a file to be uploaded?
                    if (Request.Files != null && Request.Files.Count > 0)
                    {
                        //Fetch the file from the HttpPost request
                        foreach (string requestFile in Request.Files)
                        {
                            HttpPostedFileBase file = Request.Files[requestFile];
                            //File actually has content
                            if (file.ContentLength > 0)
                            {
                                FileInfo fi = new FileInfo(file.FileName);
                                string extension = fi.Extension;
                                List<string> allowedExtensions = repo.GetAllowedExtensionsList();
                                //File type is allowed
                                if (allowedExtensions.Contains(extension))
                                {
                                    //fi.Name includes extension (i.e. .doc, .pdf)
                                    string fileName = fi.Name;
                                    model.resource.FileName = fileName;
                                    var path = Path.Combine(Server.MapPath("~/Content/Resources"), fileName);
                                    file.SaveAs(path);
                                    model.resource.UploadDate = DateTime.Now;
                                    repo.AddResource(model.resource);

                                    //Confirm to user that Resource was added
                                    TempData["popupMessage"] = "Resource successfully added!";
                                    return RedirectToAction("Index");
                                }
                                //File type not allowed
                                else
                                {
                                    ModelState.AddModelError("isURL", "File type not allowed.");
                                }
                            }
                        }
                    }
                    //There is no file to be uploaded (Error)
                    else
                    {
                        //Tell user to add file
                        ModelState.AddModelError("isURL", "Please add a file.");
                    }
                }
                //Resource is a link, no file to be uploaded
                else
                {
                    model.resource.IsURL = true;
                    model.resource.UploadDate = DateTime.Now;
                    repo.AddResource(model.resource);

                    //Confirm to user that Resource was added
                    TempData["popupMessage"] = "Resource successfully added!";
                    return RedirectToAction("Index");
                }
            }
            model.resourceHeadingsList = repo.GetResourceHeadingList();
            return View(model);
        }

        public ActionResult EditResource(int ID)
        {
            EditResourceViewModel model = new EditResourceViewModel();
            model.resource = repo.GetResourceByID(ID);
            model.resourceHeadingsList = repo.GetResourceHeadingList();
            //If there is a file to be overwritten/deleted
            if (!(model.resource.IsURL))
            {
                model.isURL = false;
                //Get the name of the old file so it can be deleted
                model.oldFileName = model.resource.FileName;
            }
            else
            {
                model.isURL = true;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditResource(EditResourceViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Resource is not a URL (i.e. there should be a file associated with it)
                if (model.isURL == false)
                {
                    //Was a new file uploaded?
                    if (Request.Files != null && Request.Files.Count > 0)
                    {
                        //Fetch the new file from the HttpPost request
                        foreach (string requestFile in Request.Files)
                        {
                            HttpPostedFileBase file = Request.Files[requestFile];
                            //Does the file have content?
                            if (file.ContentLength > 0)
                            {
                                //File has content
                                //Is the file type valid?
                                FileInfo fi = new FileInfo(file.FileName);
                                string extension = fi.Extension;
                                List<string> allowedExtensions = repo.GetAllowedExtensionsList();
                                //Is the file type allowed?
                                if (allowedExtensions.Contains(extension) == true)
                                {
                                    var path = "";
                                    //File type is valid
                                    //Is there an existing file associated with this Resource?
                                    if (model.oldFileName != "" && model.oldFileName != null)
                                    {
                                        //There is an existing file
                                        //Delete old file from folder
                                        path = Path.Combine(Server.MapPath("~/Content/Resources"), model.oldFileName);
                                        System.IO.File.Delete(path);
                                    }
                                    //Add new file and update database
                                    string fileName = fi.Name;
                                    model.resource.FileName = fileName;
                                    path = Path.Combine(Server.MapPath("~/Content/Resources"), fileName);
                                    file.SaveAs(path);
                                    model.isURL = false;
                                    model.resource.UploadDate = DateTime.Now;
                                    repo.UpdateResource(model.resource);
                                    //Confirm to user that Resource was updated
                                    TempData["popupMessage"] = "Resource successfully updated!";
                                    return RedirectToAction("Index");
                                }
                                //File type not allowed
                                else
                                {
                                    ModelState.AddModelError("isURL", "File type not allowed.");
                                }
                            }
                            //File has no content - no new file
                            else
                            {
                                //Is there an existing file associated with this Resource?
                                if (model.oldFileName != "" && model.oldFileName != null)
                                {
                                    //There is an existing file - use it
                                    //Update all information besides resource.FileName
                                    model.resource.UploadDate = DateTime.Now;
                                    repo.UpdateResource(model.resource);

                                    //Confirm to user that Resource was updated
                                    TempData["popupMessage"] = "Resource successfully updated!";
                                    return RedirectToAction("Index");
                                }
                                //There is no existing file either
                                else
                                {
                                    ModelState.AddModelError("isURL", "Please add a file.");
                                }
                            }
                        }
                    }
                    //No new file uploaded, use old file
                    else
                    {
                        //Update all information besides resource.FileName
                        model.resource.UploadDate = DateTime.Now;
                        repo.UpdateResource(model.resource);

                        //Confirm to user that Resource was updated
                        TempData["popupMessage"] = "Resource successfully updated!";
                        return RedirectToAction("Index");
                    }
                }
                //Resource is a URL
                else
                {
                    //Is there an existing file associated with this Resource?
                    if (model.oldFileName != "" && model.oldFileName != null)
                    {
                        //Delete old file (is now a URL)
                        var path = Path.Combine(Server.MapPath("~/Content/Resources"), model.oldFileName);
                        System.IO.File.Delete(path);
                    }

                    model.resource.IsURL = true;
                    model.resource.UploadDate = DateTime.Now;
                    repo.UpdateResource(model.resource);

                    //Confirm to user that Resource was updated
                    TempData["popupMessage"] = "Resource successfully updated!";
                    return RedirectToAction("Index");
                }
            }
            model.resourceHeadingsList = repo.GetResourceHeadingList();
            return View(model);
        }

        public ActionResult DeleteResource(int ID)
        {
            DeleteResourceViewModel model = new DeleteResourceViewModel();
            model.resource = repo.GetResourceByID(ID);
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteResource(DeleteResourceViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }

        public ActionResult AddResourceHeading()
        {
            AddResourceHeadingViewModel model = new AddResourceHeadingViewModel();
            model.currentResourceHeadingsList = repo.GetCurrentResourceHeadingsList();
            model.resourceHeading = new Entities.ResourceHeading();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddResourceHeading(AddResourceHeadingViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Check to see if heading already exists
                if (repo.CheckForResourceHeading(model.newResourceHeadingTitle) == true)
                {
                    //ResourceHeading already exists in database
                    ModelState.AddModelError("newResourceHeadingTitle", "This heading already exists.  Please enter a new heading.");
                }
                //ResourceHeading does not exist yet
                else
                {
                    //Assign variable to model
                    model.resourceHeading.headingText = model.newResourceHeadingTitle;
                    //Add new ResourceHeading to database
                    repo.AddResourceHeading(model.resourceHeading);
                    TempData["popupMessage"] = "New heading successfully added!";
                    return RedirectToAction("Index");
                }
            }
            model.currentResourceHeadingsList = repo.GetCurrentResourceHeadingsList();
            return View(model);
        }

        public ActionResult EditResourceHeading(int ID)
        {
            EditResourceHeadingViewModel model = new EditResourceHeadingViewModel();
            model.currentResourceHeadingsList = repo.GetCurrentResourceHeadingsList();
            model.resourceHeading = repo.GetResourceHeadingByID(ID);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditResourceHeading(EditResourceHeadingViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Check to make sure heading does not already exist in database
                if (repo.CheckForResourceHeading(model.resourceHeading.headingText) == true)
                {
                    //Heading is already in database (If Administrator is trying overwrite, this will be hit)
                    ModelState.AddModelError("resourceHeadingTitle", "This heading already exists.  Please enter a new heading or click 'Cancel'.");
                }
                //Heading is not already in database
                else
                {
                    //Update ResourceHeading object in database
                    repo.UpdateResourceHeading(model.resourceHeading);
                    TempData["popupMessage"] = "Heading successfully updated!";
                    return RedirectToAction("Index");
                }
            }
            model.currentResourceHeadingsList = repo.GetCurrentResourceHeadingsList();
            return View(model);
        }

        public ActionResult DeleteResourceHeading(int ID)
        {
            DeleteResourceHeadingViewModel model = new DeleteResourceHeadingViewModel();
            model.resourceHeading = repo.GetResourceHeadingByID(ID);
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteResourceHeading(DeleteResourceHeadingViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Check to make sure there aren't any Resources that rely on this ResourceHeading
                if (repo.CheckForResourceDependencies(model.resourceHeading.ID) == false)
                {
                    //There are no Resources that rely on this ResourceHeading
                    //Delete ResourceHeading from database
                    repo.DeleteResourceHeading(model.resourceHeading.ID);
                    TempData["popupMessage"] = "Heading successfully deleted!";
                    return RedirectToAction("Index");
                }
                //There are Resources that rely on this ResourceHeading
                else
                {
                    ModelState.AddModelError("resourceHeading", "There are Resources that need this heading.  Please delete these Resources and try again.");
                }
            }
            return View(model);
        }
    }
}