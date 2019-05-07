using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using demo.Models;
using demo.Models.Data_Transfer_Objects;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace demo.Controllers.ApiControllers
{
    public class AccountsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<IHttpActionResult> registerUser(UserDto userDto)
        {
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            ApplicationUser newUser = new ApplicationUser { UserName = userDto.Email, Email = userDto.Email, Name = userDto.Name };

            var result = await userManager.CreateAsync(newUser,userDto.Password);
       
            if(result.Succeeded)
            {
                string roleName = "User";
                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                if(roleManager.RoleExists(roleName))
                {
                    await userManager.AddToRoleAsync(newUser.Id, roleName);
                }else
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                    await userManager.AddToRoleAsync(newUser.Id, roleName);
                }
                var user = Mapper.Map<ApplicationUser, UserDto>(newUser);
                return Ok(user);
            }else
                return BadRequest();
        }
        public IHttpActionResult GetUserClaim()
        {
            var identityClaim = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaim.Claims;
            UserDto userDto = new UserDto()
            {
                Name = identityClaim.FindFirst("Name").Value,
                Email=identityClaim.FindFirst("Email").Value,
            };
            return Ok(userDto);
        }

        /*// GET: api/Accounts
        public IQueryable<ApplicationUser> GetApplicationUsers()
        {
            return db.ApplicationUsers;
        }

        // GET: api/Accounts/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult GetApplicationUser(string id)
        {
            ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }

        // PUT: api/Accounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutApplicationUser(string id, ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationUser.Id)
            {
                return BadRequest();
            }

            db.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Accounts
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult PostApplicationUser(ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ApplicationUsers.Add(applicationUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ApplicationUserExists(applicationUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = applicationUser.Id }, applicationUser);
        }

        // DELETE: api/Accounts/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult DeleteApplicationUser(string id)
        {
            ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            db.ApplicationUsers.Remove(applicationUser);
            db.SaveChanges();

            return Ok(applicationUser);
        }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            //Here we are disposing.
            base.Dispose(disposing);
        }

        private bool ApplicationUserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}
