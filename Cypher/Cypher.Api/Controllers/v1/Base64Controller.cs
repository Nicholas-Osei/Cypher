using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreHero.EntityFrameworkCore.AuditTrail;
using Cypher.Application.Interfaces.Contexts;
using Cypher.Domain.Entities.Catalog;
using Cypher.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace Cypher.Api.Controllers.v1
{
    [Route("api/v1/Login")]
    public class Base64Controller:Controller
    {
        private readonly ApplicationDbContext context;
        public Base64Controller()
        {
        }

        [HttpGet]
        public List<Credentials> GetAllCredentials()
        {
            return context.Creds.ToList();
        }
        [HttpPost]
        public IActionResult CreateCredential([FromBody] Credentials newcredentials)
        {

            context.Creds.Add(newcredentials);
            context.SaveChanges();
            return StatusCode(201, newcredentials);

        }
    }
}
