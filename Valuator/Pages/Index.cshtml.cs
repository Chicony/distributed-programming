using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Valuator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IStorage _storage;

        public IndexModel(ILogger<IndexModel> logger, IStorage storage)
        {
            _logger = logger;
            _storage = storage;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost(string text)
        {
            _logger.LogDebug(text);

            string id = Guid.NewGuid().ToString();

            string textKey = "TEXT-" + id;
            //TODO: сохранить в БД text по ключу textKey
            _storage.SetValue(textKey, text);

            string rankKey = "RANK-" + id;
            //TODO: посчитать rank и сохранить в БД по ключу rankKey
            _storage.SetValue(rankKey, CalculateRank(text).ToString());

            string similarityKey = "SIMILARITY-" + id;
            //TODO: посчитать similarity и сохранить в БД по ключу similarityKey
            _storage.SetValue(similarityKey, CalculateSimilarity(text, id).ToString());

            return Redirect($"summary?id={id}");
        }

        private double CalculateRank (string text)
        {
            double length = text.Length;
            int counter = 0;
            
            for (int j = 0; j < length; ++j)
            {
                if (!Char.IsLetter(text[j]))
                {
                    ++counter;
                }
            }    

            return Math.Round(counter / length, 2);
        }

        private double CalculateSimilarity (string text, string id) 
        {
            id = "TEXT-" + id;
            var values = _storage.GetValues("TEXT-");

            foreach (var val in values)
            {
                if (val != id && _storage.GetValue(val) == text)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
