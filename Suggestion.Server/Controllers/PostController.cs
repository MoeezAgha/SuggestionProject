using Microsoft.AspNetCore.Mvc;
using Suggestion.BL.Model;
using Suggestion.DAL.Context;
using System.Text;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace Suggestion.Server.Controllers
{



    public class PostController : Controller
    {

        private readonly ApplicationDbContext applicationDbContext;

        public PostController(ApplicationDbContext ApplicationDbContext)
        {
            applicationDbContext = ApplicationDbContext;
        }
        [HttpGet("api/fetchPost")]
        public List<Tweet> FetchPost()
        {

            var result = applicationDbContext.Tweet.ToList();

            return result;
        }

        [HttpGet("api/face")]
        public List<Tweet> FakeDataSeeding()
        {

            for (int i = 0; i < 100; i++)
            {
                string[] words = { "anemone", "wagstaff", "man", "the", "for",
        "and", "a", "with", "bird", "fox" };
                RandomText text = new RandomText(words);
                text.AddContentParagraphs(2, 2, 4, 5, 12);
                //   var usr = applicationDbContext.User.ToList();

                var z = new Tweet();
                z.UserTweet = text.Content;
                z.UserId = 1;

                applicationDbContext.Add(z);
                applicationDbContext.SaveChanges();

            }



            var result = applicationDbContext.Tweet.ToList();

            return result;
        }


    }

    public class RandomText
    {
        static Random _random = new Random();
        StringBuilder _builder;
        string[] _words;

        public RandomText(string[] words)
        {
            _builder = new StringBuilder();
            _words = words;
        }

        public void AddContentParagraphs(int numberParagraphs, int minSentences,
        int maxSentences, int minWords, int maxWords)
        {
            for (int i = 0; i < numberParagraphs; i++)
            {
                AddParagraph(_random.Next(minSentences, maxSentences + 1),
                     minWords, maxWords);
                _builder.Append("\n\n");
            }
        }

        void AddParagraph(int numberSentences, int minWords, int maxWords)
        {
            for (int i = 0; i < numberSentences; i++)
            {
                int count = _random.Next(minWords, maxWords + 1);
                AddSentence(count);
            }
        }

        void AddSentence(int numberWords)
        {
            StringBuilder b = new StringBuilder();
            // Add n words together.
            for (int i = 0; i < numberWords; i++) // Number of words
            {
                b.Append(_words[_random.Next(_words.Length)]).Append(" ");
            }
            string sentence = b.ToString().Trim() + ". ";
            // Uppercase sentence
            sentence = char.ToUpper(sentence[0]) + sentence.Substring(1);
            // Add this sentence to the class
            _builder.Append(sentence);
        }

        public string Content
        {
            get
            {
                return _builder.ToString();
            }
        }
    }
}
