using System.Collections.Generic;
using System.Linq;
using DesafioSouthSystem.Shared.Models;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace DesafioSouthSystem.Shared.CommandQuery
{
    public class CommandQueryResult
    {
        private readonly IList<Error> _errors;
        private readonly AppSettings _appSettings;
        public CommandQueryResult(IOptions<AppSettings> options)
        {
            Valid = true;
            _appSettings = options.Value;
            _errors = new List<Error>();
        }
        public bool Valid { get; private set; }
        public object Data { get; private set; }
        public IReadOnlyCollection<Error> Errors { get { return _errors.ToArray(); } }

        public void AddError(Error error)
        {
            Valid = false;
            _errors.Add(error);
        }

        public void AddError(int errorCode)
        {
            Valid = false;
             string msg = _appSettings.Errors.FirstOrDefault(f => f.Key == errorCode.ToString()).Value;
            _errors.Add(new Error(errorCode, msg));
        }

        public void SetData(object data)
        {
            Data = data;
        }

        public void Validate<T>(T entity, AbstractValidator<T> validator)
        {
            var val = validator.Validate(entity);
            if (!val.IsValid)
            {
                val.Errors.ToList().ForEach(e =>
                {
                    int code = int.Parse(e.ErrorCode);
                    string msg = _appSettings.Errors.FirstOrDefault(f => f.Key == e.ErrorCode).Value;
                    AddError(new Error(code, msg));
                });
            }
        }

        public bool HasError(int errorCode)
        {
            return Errors.Any(f => f.Code == errorCode);
        }
    }
}
