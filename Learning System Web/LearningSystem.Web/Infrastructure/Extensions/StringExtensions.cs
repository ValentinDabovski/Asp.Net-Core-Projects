﻿namespace LearningSystem.Web.Infrastructure.Extensions
{
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string ToFirendlyUrl(this string text)
        {
            if (text.Contains("#"))
            {
                text = Regex.Replace(text, "sharp", "-");
            }

            text = Regex.Replace(text, @"[^A-Za-z0-9_\\.~]+", "-").ToLower();
            return text;

        }

        public static string ToCountStudentsString(this int count)
        {
            if (count <= 0)
            {
                return "No students enroled";
            }

            if (count == 1)
            {
                return $"{count} Student";
            }

            return $"{count} Students";
        }
    }
}
