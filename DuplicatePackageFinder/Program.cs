using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

namespace DuplicatePackageFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            Console.WriteLine("Finding Duplicate Package References");

            //Configure the path and search pattern
            var path = config["folderPath"];
            var searchPattern = config["searchFilePattern"];
            Console.WriteLine($"Looking for files with pattern: {searchPattern} in path: {path}");

            //Collect all files matching the search pattern in the folder path provided
            var files = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);
            Console.WriteLine($"{files.Count()} files found...");

            //Configure the regex
            Regex regex = GetRegex();
            string tempLineValue;
            foreach (var file in files)
            {
                var referenceLines = new List<string>();
                using (StreamReader inputReader = new StreamReader(file))
                {
                    while (null != (tempLineValue = inputReader.ReadLine()))
                    {
                        //Get all package lines
                        if (regex.Match(tempLineValue).Success)
                        {
                            //store the line without the version number
                            var output = Regex.Replace(tempLineValue, @"[\d-]", string.Empty);

                            referenceLines.Add(output);
                        }
                    }
                }

                // Group by packages that have more than one of the same reference
                var query = referenceLines.GroupBy(x => x)
                              .Where(g => g.Count() > 1)
                              .Select(y => new { Element = y.Key, Counter = y.Count() })
                              .ToList();


                if (query.Any())
                {
                    //Output duplicate lines to console
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(file);
                    Console.ForegroundColor = ConsoleColor.Green;
                    foreach (var duplicate in query)
                    {
                        Console.WriteLine($"{duplicate}");
                    }
                    Console.WriteLine();
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static Regex GetRegex()
        {
            string re3 = "(PackageReference)";

            Regex r = new Regex(re3, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return r;
        }
    }
}

