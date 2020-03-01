using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace cw1
{
    class Program
    {
        public static async Task Main(string[] args)
        {

            if(args.Length == 0)
            {
                throw new ArgumentNullException();
            }

            foreach (var a in args)
            {
                Console.WriteLine(a);
            }

            var emails = await GetEmails(args[0]);

            foreach (var email in emails)
            {
                Console.WriteLine(email);
            }

        }

        static async Task<IList<string>> GetEmails(string url)
        {
            Uri uriResult;
            bool isUrl = Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uriResult);

            if (isUrl)
            {
                throw new ArgumentException();
            }

            var listOfEmails = new List<string>();
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            httpClient.Dispose();

            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            MatchCollection emailMatches = emailRegex.Matches(response.Content.ReadAsStringAsync().Result);

        

            foreach(var emailMatch in emailMatches)
            {
                listOfEmails.Add(emailMatch.ToString());
            }
            return listOfEmails;
        }
    }
}
