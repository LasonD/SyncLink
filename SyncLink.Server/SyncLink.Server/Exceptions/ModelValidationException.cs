using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SyncLink.Server.Exceptions;

internal class ModelValidationException : Exception
{
    public ModelValidationException(ModelStateDictionary modelState) : base("Invalid data")
    {
        ModelState = modelState;
    }

    public ModelStateDictionary ModelState { get; }
}