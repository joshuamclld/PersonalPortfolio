// My Personal Portfolio Management Controller
// This file handles all the logic for the admin dashboard, where I can update my portfolio content.
// I've organized it by section (Profile, About, etc.) to keep things clean.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalProjectPortfolio.Data;
using FinalProjectPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectPortfolio.Controllers
{
    [Authorize] // I've secured this entire controller, so only logged-in users can access these actions.
    public class AdminController : Controller
    {
        // I need the database context to get and save data, and the host environment to handle file uploads.
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // This is the main entry point for my admin dashboard.
        public IActionResult Index()
        {
            return View();
        }

        #region Profile Actions
        // --- My Profile Actions ---
        // This action gets my profile data and shows the edit page.
        public async Task<IActionResult> EditProfile()
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync();
            return View(profile ?? new Profile()); // If no profile exists, I'll create a new one.
        }

        // This action saves the changes I make to my profile.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(Profile profile, IFormFile? profilePicture, IFormFile? cvFile)
        {
            if (ModelState.IsValid)
            {
                // Handle the profile picture upload.
                if (profilePicture != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(profilePicture.FileName);
                    string extension = Path.GetExtension(profilePicture.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension; // Unique filename to avoid conflicts.
                    string path = Path.Combine(wwwRootPath + "/uploads/profile/", fileName);
                    
                    Directory.CreateDirectory(Path.Combine(wwwRootPath, "uploads/profile"));

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await profilePicture.CopyToAsync(fileStream);
                    }
                    
                    // If I'm uploading a new picture, I should delete the old one.
                    if (!string.IsNullOrEmpty(profile.ProfilePictureUrl))
                    {
                        var oldPath = Path.Combine(wwwRootPath, "uploads/profile", profile.ProfilePictureUrl);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    profile.ProfilePictureUrl = fileName;
                }
                else
                {
                    // If I don't upload a new picture, I'll keep the existing one.
                    var existingProfile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.Id == profile.Id);
                    if (existingProfile != null)
                    {
                        profile.ProfilePictureUrl = existingProfile.ProfilePictureUrl;
                    }
                }

                // Handle the CV/resume file upload.
                if (cvFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = "resume" + Path.GetExtension(cvFile.FileName); // I'll keep a consistent name for my resume.
                    string path = Path.Combine(wwwRootPath + "/uploads/cv/", fileName);
                    
                    Directory.CreateDirectory(Path.Combine(wwwRootPath, "uploads/cv"));

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await cvFile.CopyToAsync(fileStream);
                    }
                    profile.CVUrl = fileName;
                }
                else
                {
                    // If I don't upload a new CV, I'll keep the existing one.
                    var existingProfile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.Id == profile.Id);
                    if (existingProfile != null)
                    {
                        profile.CVUrl = existingProfile.CVUrl;
                    }
                }

                // If the profile is new, I'll add it. Otherwise, I'll update the existing one.
                if (profile.Id == 0)
                {
                    _context.Add(profile);
                }
                else
                {
                    _context.Update(profile);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profile);
        }
        #endregion

        #region About Actions
        // --- My About Section Actions ---
        // This gets the content for my "About Me" page.
        public async Task<IActionResult> EditAbout()
        {
            var about = await _context.Abouts.FirstOrDefaultAsync();
            return View(about ?? new About());
        }

        // This saves the changes to my "About Me" section.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAbout(About about)
        {
            if (ModelState.IsValid)
            {
                if (about.Id == 0) _context.Add(about);
                else _context.Update(about);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(about);
        }
        #endregion
        
        #region Contact Actions
        // --- My Contact Info Actions ---
        // This gets my contact details for the edit page.
        public async Task<IActionResult> EditContact()
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync();
            return View(contact ?? new Contact());
        }

        // This saves my updated contact information.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                if (contact.Id == 0) _context.Add(contact);
                else _context.Update(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }
        #endregion

        #region Experience Actions
        // --- My Work Experience Actions ---
        // This shows a list of all my past jobs.
        public async Task<IActionResult> ManageExperience()
        {
            return View(await _context.Experiences.OrderByDescending(e => e.StartDate).ToListAsync());
        }

        // This shows the page where I can add a new job.
        public IActionResult CreateExperience()
        {
            return View();
        }

        // This saves the new job to my database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExperience(Experience experience)
        {
            if (ModelState.IsValid)
            {
                _context.Add(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageExperience));
            }
            return View(experience);
        }

        // This shows the page to edit an existing job.
        public async Task<IActionResult> EditExperience(int? id)
        {
            if (id == null) return NotFound();
            var experience = await _context.Experiences.FindAsync(id);
            if (experience == null) return NotFound();
            return View(experience);
        }

        // This saves the changes to an existing job.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExperience(int id, Experience experience)
        {
            if (id != experience.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageExperience));
            }
            return View(experience);
        }

        // This shows the confirmation page before I delete a job entry.
        public async Task<IActionResult> DeleteExperience(int? id)
        {
            if (id == null) return NotFound();
            var experience = await _context.Experiences.FirstOrDefaultAsync(m => m.Id == id);
            if (experience == null) return NotFound();
            return View(experience);
        }

        // This actually deletes the job entry after I confirm.
        [HttpPost, ActionName("DeleteExperience")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExperienceConfirmed(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            if (experience != null)
            {
                _context.Experiences.Remove(experience);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ManageExperience));
        }
        #endregion

        #region Project Actions
        // --- My Portfolio Project Actions ---
        // This shows a list of all my projects.
        public async Task<IActionResult> ManageProjects()
        {
            return View(await _context.Projects.ToListAsync());
        }

        // This shows the page to add a new project.
        public IActionResult CreateProject()
        {
            return View();
        }

        // This saves a new project, including its image.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProject(Project project, IFormFile? projectImage)
        {
            if (ModelState.IsValid)
            {
                if (projectImage != null)
                {
                    project.ImageUrl = await UploadFile(projectImage, "projects");
                }
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageProjects));
            }
            return View(project);
        }

        // This shows the page to edit an existing project.
        public async Task<IActionResult> EditProject(int? id)
        {
            if (id == null) return NotFound();
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();
            return View(project);
        }

        // This saves changes to a project, including updating the image if I upload a new one.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProject(int id, Project project, IFormFile? projectImage)
        {
            if (id != project.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (projectImage != null)
                {
                    if (!string.IsNullOrEmpty(project.ImageUrl))
                    {
                        DeleteFile(project.ImageUrl, "projects");
                    }
                    project.ImageUrl = await UploadFile(projectImage, "projects");
                }
                else
                {
                    var existingProject = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                    if (existingProject != null)
                    {
                        project.ImageUrl = existingProject.ImageUrl;
                    }
                }
                
                _context.Update(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageProjects));
            }
            return View(project);
        }

        // This shows the confirmation page before I delete a project.
        public async Task<IActionResult> DeleteProject(int? id)
        {
            if (id == null) return NotFound();
            var project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
            if (project == null) return NotFound();
            return View(project);
        }

        // This deletes the project and its image from my server.
        [HttpPost, ActionName("DeleteProject")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProjectConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                if (!string.IsNullOrEmpty(project.ImageUrl))
                {
                    DeleteFile(project.ImageUrl, "projects");
                }
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ManageProjects));
        }
        #endregion

        #region Skill Actions
        // --- My Skills Actions ---
        public async Task<IActionResult> ManageSkills()
        {
            return View(await _context.Skills.ToListAsync());
        }

        public IActionResult CreateSkill()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSkill(Skill skill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageSkills));
            }
            return View(skill);
        }

        public async Task<IActionResult> EditSkill(int? id)
        {
            if (id == null) return NotFound();
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) return NotFound();
            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSkill(int id, Skill skill)
        {
            if (id != skill.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageSkills));
            }
            return View(skill);
        }

        public async Task<IActionResult> DeleteSkill(int? id)
        {
            if (id == null) return NotFound();
            var skill = await _context.Skills.FirstOrDefaultAsync(m => m.Id == id);
            if (skill == null) return NotFound();
            return View(skill);
        }

        [HttpPost, ActionName("DeleteSkill")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSkillConfirmed(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill != null)
            {
                _context.Skills.Remove(skill);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ManageSkills));
        }
        #endregion
        
        #region Social Link Actions
        // --- My Social Media Link Actions ---
        public async Task<IActionResult> ManageSocialLinks()
        {
            return View(await _context.SocialLinks.ToListAsync());
        }

        public IActionResult CreateSocialLink()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSocialLink(SocialLink socialLink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socialLink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageSocialLinks));
            }
            return View(socialLink);
        }

        public async Task<IActionResult> EditSocialLink(int? id)
        {
            if (id == null) return NotFound();
            var socialLink = await _context.SocialLinks.FindAsync(id);
            if (socialLink == null) return NotFound();
            return View(socialLink);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSocialLink(int id, SocialLink socialLink)
        {
            if (id != socialLink.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(socialLink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageSocialLinks));
            }
            return View(socialLink);
        }

        public async Task<IActionResult> DeleteSocialLink(int? id)
        {
            if (id == null) return NotFound();
            var socialLink = await _context.SocialLinks.FirstOrDefaultAsync(m => m.Id == id);
            if (socialLink == null) return NotFound();
            return View(socialLink);
        }

        [HttpPost, ActionName("DeleteSocialLink")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSocialLinkConfirmed(int id)
        {
            var socialLink = await _context.SocialLinks.FindAsync(id);
            if (socialLink != null)
            {
                _context.SocialLinks.Remove(socialLink);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ManageSocialLinks));
        }
        #endregion
        
        #region Education Actions
        // --- My Education History Actions ---
        public async Task<IActionResult> ManageEducation()
        {
            return View(await _context.Educations.OrderByDescending(e => e.StartDate).ToListAsync());
        }

        public IActionResult CreateEducation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEducation(Education education)
        {
            if (ModelState.IsValid)
            {
                _context.Add(education);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageEducation));
            }
            return View(education);
        }

        public async Task<IActionResult> EditEducation(int? id)
        {
            if (id == null) return NotFound();
            var education = await _context.Educations.FindAsync(id);
            if (education == null) return NotFound();
            return View(education);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEducation(int id, Education education)
        {
            if (id != education.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(education);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageEducation));
            }
            return View(education);
        }

        public async Task<IActionResult> DeleteEducation(int? id)
        {
            if (id == null) return NotFound();
            var education = await _context.Educations.FirstOrDefaultAsync(m => m.Id == id);
            if (education == null) return NotFound();
            return View(education);
        }

        [HttpPost, ActionName("DeleteEducation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEducationConfirmed(int id)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education != null)
            {
                _context.Educations.Remove(education);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ManageEducation));
        }
        #endregion
        
        #region Service Actions
        // --- My Offered Services Actions ---
        public async Task<IActionResult> ManageServices()
        {
            return View(await _context.Services.ToListAsync());
        }

        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateService(Service service, IFormFile? serviceImage)
        {
            if (ModelState.IsValid)
            {
                if (serviceImage != null)
                {
                    service.ImageUrl = await UploadFile(serviceImage, "services");
                }
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageServices));
            }
            return View(service);
        }

        public async Task<IActionResult> EditService(int? id)
        {
            if (id == null) return NotFound();
            var service = await _context.Services.FindAsync(id);
            if (service == null) return NotFound();
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(int id, Service service, IFormFile? serviceImage)
        {
            if (id != service.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (serviceImage != null)
                {
                    if (!string.IsNullOrEmpty(service.ImageUrl))
                    {
                        DeleteFile(service.ImageUrl, "services");
                    }
                    service.ImageUrl = await UploadFile(serviceImage, "services");
                }
                else
                {
                    var existingService = await _context.Services.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                    if (existingService != null)
                    {
                        service.ImageUrl = existingService.ImageUrl;
                    }
                }
                
                _context.Update(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageServices));
            }
            return View(service);
        }

        public async Task<IActionResult> DeleteService(int? id)
        {
            if (id == null) return NotFound();
            var service = await _context.Services.FirstOrDefaultAsync(m => m.Id == id);
            if (service == null) return NotFound();
            return View(service);
        }

        [HttpPost, ActionName("DeleteService")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteServiceConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                if (!string.IsNullOrEmpty(service.ImageUrl))
                {
                    DeleteFile(service.ImageUrl, "services");
                }
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ManageServices));
        }
        #endregion

        // --- Helper Methods ---
        // A private helper method to handle file uploads, which keeps my code DRY.
        private async Task<string> UploadFile(IFormFile file, string subfolder)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath, "uploads", subfolder, fileName);

            Directory.CreateDirectory(Path.Combine(wwwRootPath, "uploads", subfolder));

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }

        // A private helper method to delete old files.
        private void DeleteFile(string fileName, string subfolder)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            var filePath = Path.Combine(wwwRootPath, "uploads", subfolder, fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        
        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
