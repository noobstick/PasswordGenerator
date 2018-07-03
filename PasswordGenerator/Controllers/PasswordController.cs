using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasswordGenerator.Models;

namespace PasswordGenerator.Controllers
{
    [Route("api/password")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly PasswordContext _context;

        private const string alphabets = "abcdefghijklmnopqrstuvwxyz";
        private const string numbers = "0123456789";
        private const string symbols = "!@#$%^&*()_+{}|:<>?,.";
        private const int multiplePass = 10;

        private List<Password> passList = new List<Password>();

        public PasswordController(PasswordContext context)
        {
            _context = context;
        }

        [HttpPost("generate")]
        public IActionResult Generate(PasswordRequirement passwordReq)
        {
            string criteria = GetPasswordCriteria(passwordReq);

            if (passwordReq.Length > 0 && passwordReq.MultiplePasswords)
            {
                for (int i = 0; i < multiplePass; i++)
                {
                    passList.Add(GeneratePassword(criteria, passwordReq.Length));
                }
            }
            else
            {
                passList.Add(GeneratePassword(criteria, passwordReq.Length));
            }

            return new JsonResult(passList);
        }

        private Password GeneratePassword(string criteria, int passwordLength = 10)
        {
            Random rng = new Random();
            Password password = new Password();
            for (int i = 0; i < passwordLength; i++)
            {
                char rngChar = criteria[rng.Next(0, criteria.Length)];
                password.PasswordText += rngChar;
            }

            return password;
        }

        private string GetPasswordCriteria(PasswordRequirement passwordReq)
        {
            string criteria = "";

            if (passwordReq.IncludeLowercase)
            {
                criteria += alphabets;
            }

            if (passwordReq.IncludeUpperCase)
            {
                criteria += alphabets.ToUpper();
            }

            if (passwordReq.IncludeNumbers)
            {
                criteria += numbers;
            }

            if (passwordReq.IncludeSymbols)
            {
                criteria += symbols;
            }

            return criteria;
        }

    }
}