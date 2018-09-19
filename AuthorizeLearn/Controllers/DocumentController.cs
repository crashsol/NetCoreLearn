using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizeLearn.Data;
using AuthorizeLearn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizeLearn.Controllers
{
    public class DocumentController : Controller
    {

        private readonly IAuthorizationService _authorizationService;
        private readonly IDocumentRepository _documentRepository;

        public DocumentController(IAuthorizationService authorizationService,
                                  IDocumentRepository documentRepository)
        {
            _authorizationService = authorizationService;
            _documentRepository = documentRepository;
        }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            var document = _documentRepository.Find(id);

            if (document == null)
            {
                return new NotFoundResult();            }

            //判断用户是否具有这个资源的访问权限
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, document, "DocumentPolicy");

            if (authorizationResult.Succeeded)
            {
                return View(document);
            }
            else if (User.Identity.IsAuthenticated)
            {
                return new ForbidResult();
            }
            else
            {
                return new ChallengeResult();
            }
        }
    }
}