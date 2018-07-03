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
        private const string alphabets = "abcdefghijklmnopqrstuvwxyz";
        private const string numbers = "0123456789";
        private const string symbols = "!@#$%^&*()_+{}|:<>?,.";
        private const int multiplePass = 10;

        private List<Password> passList = new List<Password>();
        Random rng = new Random();

        public PasswordController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            string criteria = GetPasswordCriteria();
            passList.Add(GeneratePassword(criteria, rng.Next(10, 50)));

            return new JsonResult(passList);
        }

        [HttpGet("criteria")]
        public IActionResult GetWithCriteria([FromQuery]int length, [FromQuery]bool lowerCase, [FromQuery]bool upperCase, [FromQuery]bool numbers, [FromQuery]bool symbols, [FromQuery]bool multiPass)
        {
            PasswordRequirement passwordReq = new PasswordRequirement
            {
                Length = length,
                IncludeLowercase = lowerCase,
                IncludeUpperCase = upperCase,
                IncludeNumbers = numbers,
                IncludeSymbols = symbols,
                MultiplePasswords = multiPass
            };
            string criteria = GetPasswordCriteria(passwordReq);

            if (passwordReq.Length > 0 && passwordReq.MultiplePasswords)
            {
                for (int i = 0; i < multiplePass; i++)
                {
                    passList.Add(GeneratePassword(criteria, passwordReq.Length));
                }
            }
            else if (passwordReq.Length > 0)
            {
                passList.Add(GeneratePassword(criteria, passwordReq.Length));
            }

            return new JsonResult(passList);
        }

        private Password GeneratePassword(string criteria, int passwordLength = 10)
        {
            Password password = new Password();
            for (int i = 0; i < passwordLength; i++)
            {
                char rngChar = criteria[rng.Next(0, criteria.Length)];
                password.PasswordText += rngChar;
            }

            return password;
        }

        private string GetPasswordCriteria(PasswordRequirement passwordReq = null)
        {
            string criteria = "";

            if (passwordReq != null)
            {
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
            }
            else
            {
                criteria += alphabets + alphabets.ToUpper() + numbers + symbols;
            }

            return criteria;
        }

    }
}