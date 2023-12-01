using System;
using ConvertionToWordsApp.Data;
using ConvertionToWordsApp.Models;
using ConvertionToWordsApp.Models.DTO;
using ConvertionToWordsApp.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ConvertionToWordsApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ConvertionController : ControllerBase
    {
        private readonly IinputRepository _inputRepository;

        public ConvertionController(IinputRepository inputRepository)
        {
            _inputRepository = inputRepository;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(decimal inputNumber)
        {
            //Console.Write(inputNumber);

            var input = new Input
            {
                amounts = inputNumber
            };

           await _inputRepository.CreateAsync(input);
            
            var response = new InputDto
            {
                Id = input.Id,
                amounts = input.amounts
            };

            return Ok(response);
        }
    

        [HttpGet]
        public async Task<ActionResult> GetAmount()
        {
           
            var inputItems = await _inputRepository.GetAllAsync();
            var response = new List<getOutputDto>();
            var data = inputItems.OrderByDescending(e => e.Id).FirstOrDefault();
            {
                decimal d = data.amounts;

                string result = NumberConvertToWords(new InputRequestDto { amounts = d });
                response.Add(new getOutputDto
                {
                    id = data.Id,
                    amounts = result
                });
                return Ok(response);
                
            }
                  
        }

        private static string NumberConvertToWords(InputRequestDto value)
        {
            string amountString = value.amounts.ToString().Replace(",", "");

            if (!decimal.TryParse(amountString, out decimal amount))
            {
                return "Invalid input";
            }

            long dollars = (long)Math.Floor(amount);
            int cents = (int)((amount - dollars) * 100);

            if (dollars == 0 && cents == 0)
            {
                return "Zero dollars";
            }
            if (dollars == 1)
            {
                return "One dollar";
            }

            string dollarsInWords = ConvertToWords(dollars);
            string centsInWords = ConvertToWords(cents);

            if (dollars != 0 && cents != 0)
            {
                return $"{dollarsInWords} dollars and {centsInWords} cents";
            }
            else
            {
                return dollars != 0 ? $"{dollarsInWords} dollars" : $"{centsInWords} cents";
            }
        }

        private static string ConvertToWords(long number)
        {
            if (number == 0)
            {
                return "Zero";
            }

            int groupIndex = 0;
            string words = "";

            do
            {
                int group = (int)(number % 1000);
                number /= 1000;

                if (group > 0)
                {
                    string groupWords = ConvertGroupToWords(group);

                    words = groupWords + Units(groupIndex) + " " + words;
                }

                groupIndex++;
            } while (number > 0);

            return words.Trim();
        }

        private static string ConvertGroupToWords(int group)
        {
            string groupWords = "";

            // Convert hundreds place
            if (group >= 100)
            {
                groupWords += Ones(group / 100) + " Hundred ";
                group %= 100;
            }

            // Convert tens and ones places
            if (group >= 11 && group <= 19)
            {
                groupWords += Teens(group - 11) + " ";
            }
            else
            {
                groupWords += Tens(group / 10) + " ";
                group %= 10;

                if (group > 0)
                {
                    groupWords += Ones(group) + " ";
                }
            }

            return groupWords;
        }

        private static string Units(int groupIndex)
        {
            switch (groupIndex)
            {
                case 0: return "";
                case 1: return "Thousand";
                case 2: return "Million";
                case 3: return "Billion";
                case 4: return "Trillion";
                default: return "";
            }
        }

        private static string Ones(int number)
        {
            switch (number)
            {
                case 1: return "One";
                case 2: return "Two";
                case 3: return "Three";
                case 4: return "Four";
                case 5: return "Five";
                case 6: return "Six";
                case 7: return "Seven";
                case 8: return "Eight";
                case 9: return "Nine";
                default: return "";
            }
        }

        private static string Teens(int number)
        {
            switch (number)
            {
                case 0: return "Ten";
                case 1: return "Eleven";
                case 2: return "Twelve";
                case 3: return "Thirteen";
                case 4: return "Fourteen";
                case 5: return "Fifteen";
                case 6: return "Sixteen";
                case 7: return "Seventeen";
                case 8: return "Eighteen";
                case 9: return "Nineteen";
                default: return "";
            }
        }

        private static string Tens(int number)
        {
            switch (number)
            {
                case 2: return "Twenty";
                case 3: return "Thirty";
                case 4: return "Forty";
                case 5: return "Fifty";
                case 6: return "Sixty";
                case 7: return "Seventy";
                case 8: return "Eighty";
                case 9: return "Ninety";
                default: return "";
            }
        }


    }

}


