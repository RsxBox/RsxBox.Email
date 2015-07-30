using System.Collections.Generic;
using RsxBox.Email.Core.Models;
using RsxBox.Email.Core.Interface;
using Microsoft.AspNet.Mvc;

namespace RsxBox.Email.Owin.WebApi.Controllers
{
    [Route("[controller]")]
    public class RsxBoxEmailController : Controller
    {
        private IEmailManager<EmailTemplate, int> manager;

        public RsxBoxEmailController(IEmailManager<EmailTemplate, int> manager)
        {
            this.manager = manager;
        }


        [HttpGet]
        public IEnumerable<EmailTemplate> GetAll()
        {
            return manager.GetAllTemplates(0,10);
        }

        [HttpGet("{id:int}", Name = "GetByIdRoute")]
        public IActionResult GetById(int id)
        {
            var item = manager.GetTemplate(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        public void CreateTodoItem([FromBody] EmailTemplate item)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
            }
            else
            {
                 manager.CreateTemplate(item);

                string url = Url.RouteUrl("GetByIdRoute", new { id = item.PK },
                    Request.Scheme, Request.Host.ToUriComponent());

                ///TODO: Persistence Module with Module and Application to get URL. 
                Context.Response.StatusCode = 201;
                Context.Response.Headers["Location"] = url;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            var item = manager.GetTemplate(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            manager.DeleteTemplate(id);
            return new HttpStatusCodeResult(204); // 201 No Content
        }
    }
}
