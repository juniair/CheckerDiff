// See https://aka.ms/new-console-template for more information


using CheckerDiff.Models;
using CsvHelper.Configuration;
using System.Globalization;
using CheckerDiff.Services;





var service = new CheckerDiffService();
service.Diff($@"C:\Users\Alice\Downloads\Windows");
//service.Replace($@"C:\Users\Alice\Downloads\Windows", oldHeader, newHeader);
Console.WriteLine("Hello, World!");

