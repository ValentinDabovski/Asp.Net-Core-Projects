namespace CameraBazaar.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Html;
    using CameraBazaar.Models.ViewModels.Camera;



    public static class CameraViewExtensions
    {
        private static readonly HtmlString successText = new HtmlString("<p class=\"text-success\">In stock</p>");
        private static readonly HtmlString errorText = new HtmlString("<p class=\"text-danger\">Out of stock</p>");

        public static HtmlString InStockDisplay(this DetailCameraView camera)
        {
            
            if (camera.IsInStock)
            {
                return successText;
            }

            return errorText;
        }

        public static HtmlString InStockDisplay(this AllCamerasViewModel camera)
        {
            if (camera.IsInStock)
            {
                return successText;
            }

            return errorText;
        }
    }
}
