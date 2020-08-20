using DesafioSouthSystem.Domain.Commands;
using DesafioSouthSystem.Domain.Entities;
using DesafioSouthSystem.Domain.Handlers.Entities;
using DesafioSouthSystem.Shared.CommandQuery;
using DesafioSouthSystem.Shared.Models;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioSouthSystem.Domain.Handlers.Commands
{
    public class FileReadingCommandHandler : CommandQueryHandler, IRequestHandler<FileReadingCommand, CommandQueryResult>
    {
        private readonly string _separator = "ç";
        public FileReadingCommandHandler(IOptions<AppSettings> optionsAppSettings):base(optionsAppSettings)
        {
        }

        public async Task<CommandQueryResult> Handle(FileReadingCommand fileReadingCommand, CancellationToken cancelationToken)
        {
            var commandResult = new CommandQueryResult(_optionsAppSettings);

            string[] lines = await File.ReadAllLinesAsync(fileReadingCommand.InputPath, Encoding.UTF8);
            if (lines.Length == 0)
            {
                commandResult.AddError(1001);
            }
            else
            {
                var salesmen = new List<Salesman>();
                var customers = new List<Customer>();
                var sales = new List<Sale>();
                Console.WriteLine("INPUT:");
                ForEachLine(lines, commandResult, salesmen, customers, sales);

                if (!commandResult.Valid)
                    return commandResult;

                string[] outputLines = CollectOutputLines(salesmen, customers, sales);
                commandResult.SetData(outputLines);
                await WriteOutputFile(fileReadingCommand.OutputPath, outputLines);
            }
            return commandResult;
        }

        private void ForEachLine(string[] lines, CommandQueryResult commandResult, List<Salesman> salesmen, List<Customer> customers, List<Sale> sales)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                Console.WriteLine($"Line #{i}: {line}");
                if (!line.Contains(_separator))
                {
                    commandResult.AddError(1015);
                    return;
                }
                else
                {
                    string[] fields = line.Split(_separator);
                    string code = fields[0];
                    SwitchCaseCode(commandResult, salesmen, customers, sales, fields, code);
                    if (!commandResult.Valid)
                        return;
                }
            }
        }

        private void SwitchCaseCode(CommandQueryResult commandResult, List<Salesman> salesmen, List<Customer> customers, List<Sale> sales, string[] fields, string code)
        {
            switch (code)
            {
                case "001":
                    var sm = new SalesmanHandler().Collect(fields, commandResult);
                    if (commandResult.Valid)
                        salesmen.Add(sm);
                    break;
                case "002":
                    var c = new CustomerHandler().Collect(fields, commandResult);
                    if (commandResult.Valid)
                        customers.Add(c);
                    break;
                case "003":
                    var s = new SaleHandler().Collect(fields, commandResult);
                    if (commandResult.Valid)
                        sales.Add(s);
                    break;
                default:
                    break;
            }
        }

        private string[] CollectOutputLines(List<Salesman> salesmen, List<Customer> customers, List<Sale> sales)
        {
            int totalCustomers = customers.Count;
            int totalSalesmen = salesmen.Count;
            var mostExpensiveSales = sales.OrderByDescending(f => f.SaleItems.Sum(m => m.Price * m.Quatity));
            int mostExpensiveSaleID = mostExpensiveSales.First().SaleID;
            string worstSalesmanName = mostExpensiveSales.Last().SalesmanName;

            string[] lines = new string[4] {
                totalCustomers.ToString(), totalSalesmen.ToString(),
                mostExpensiveSaleID.ToString(), worstSalesmanName.ToString()
            };
            return lines;
        }

        private async Task WriteOutputFile(string outputPath, string[] lines)
        {
            if (File.Exists(outputPath))
                File.Delete(outputPath);
            File.Create(outputPath).Close();

            await File.WriteAllLinesAsync(outputPath, lines, Encoding.UTF8);
        }
    }
}
