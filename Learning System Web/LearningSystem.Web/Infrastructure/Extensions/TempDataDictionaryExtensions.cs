using LearningSystem.Common.Constants;

namespace LearningSystem.Web.Infrastructure.Extensions
{

    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using static WebConstants;

    public static class TempDataDictionaryExtensions
    {
        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[TemDataSuccessMesageKey] = message;
        }

        public static void AddErrorMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[TemDataErrorMesageKey] = message;
        }
    }
}
