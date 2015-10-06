using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Thumb : System.Web.UI.Page
{

    private void Page_Load(object sender, System.EventArgs e)
    {
        // get the file name -- fall800.jpg
        string file = Request.QueryString["file"];

        // create an image object, using the filename we just retrieved
        System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(file));

        // create the actual thumbnail image
        System.Drawing.Image thumbnailImage = image.GetThumbnailImage(100, 100, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);

        // make a memory stream to work with the image bytes
        MemoryStream imageStream = new MemoryStream();

        // put the image into the memory stream
        thumbnailImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        // make byte array the same size as the image
        byte[] imageContent = new Byte[imageStream.Length];

        // rewind the memory stream
        imageStream.Position = 0;

        // load the byte array with the image
        imageStream.Read(imageContent, 0, (int)imageStream.Length);

        // return byte array to caller with image type
        Response.ContentType = "image/jpeg";
        Response.BinaryWrite(imageContent);
    }
    public bool ThumbnailCallback()
    {
        return true;
    }
}
