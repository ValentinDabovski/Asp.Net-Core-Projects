﻿namespace LearningSystem.Services.Html
{
    using Ganss.XSS;

    public class HtmlService : IHtmlService
    {
        private readonly HtmlSanitizer htmlSanitizer;

        public HtmlService()
        {
            this.htmlSanitizer = new HtmlSanitizer();
            this.htmlSanitizer.AllowedAttributes.Add("class");
        }

        public string Sanitize(string htmlContent)
        {
            return this.htmlSanitizer.Sanitize(htmlContent);
        }
    }
}
