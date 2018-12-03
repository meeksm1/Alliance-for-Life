using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace Alliance_for_Life.ViewModels

{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dueDate;

            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                "mm/dd/yyyy",CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out dueDate);

            return (isValid && dueDate > DateTime.Now);
        }
    }

    public class BirthDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dob;

            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                "mm/dd/yyyy", CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out dob);

            return (isValid && dob < DateTime.Now);
        }
    }

    public class WordDocumentAttribute : ActionFilterAttribute
    {
        public string DefaultFilename { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var result = filterContext.Result as ViewResult;

            if (result != null)
                result.MasterName = "~/Views/Shared/_LayoutWord.cshtml";

            filterContext.Controller.ViewBag.WordDocumentMode = true;

            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var filename = filterContext.Controller.ViewBag.WordDocumentFilename;
            filename = filename ?? DefaultFilename ?? "Print";

            filterContext.HttpContext.Response.AppendHeader("Content-Disposition", string.Format("filename={0}.doc", filename));
            filterContext.HttpContext.Response.ContentType = "application/msword";

            base.OnResultExecuted(filterContext);
        }
    }
}