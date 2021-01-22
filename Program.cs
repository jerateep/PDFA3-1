using System;
using System.IO;
using System.Xml.Serialization;

namespace PDFA3
{
    class Program
    {
        static void Main(string[] args)
        {
            string pdfPath = @"C:\Users\jerateeps\Desktop\A3TEST.PDF";
            byte[] array = File.ReadAllBytes(pdfPath);
            //var LstFile = ListEmbeddedFileNames(pdfPath);
            var LstFile2 = GetEmbeddedFileData(pdfPath, "A3TEST");
        }

        public static string[] ListEmbeddedFileNames(string pdfFileName)
        {
            string[] fileNames = new string[0];
            var reader = new iTextSharp.text.pdf.PdfReader(pdfFileName);
            if (reader != null)
            {
                var root = reader.Catalog;
                if (root != null)
                {
                    var names = root.GetAsDict(iTextSharp.text.pdf.PdfName.NAMES);
                    if (names != null)
                    {
                        var embeddedFiles = names.GetAsDict(iTextSharp.text.pdf.PdfName.EMBEDDEDFILES);
                        if (embeddedFiles != null)
                        {
                            var namesArray = embeddedFiles.GetAsArray(iTextSharp.text.pdf.PdfName.NAMES);

                            if (namesArray != null)
                            {
                                int n = namesArray.Size / 2; // I don't understand why I have to divide by 2
                                fileNames = new string[n];
                                for (int i = 0; i < n; i++) 
                                    fileNames[i] = namesArray[2 * i].ToString();
                            }
                        }
                    }
                }
                reader.Close();
            }
            return fileNames;
        }
        public static byte[] GetEmbeddedFileData(string pdfFileName, string embeddedFileName)
        {
            byte[] attachedFileBytes = null;
            var reader = new iTextSharp.text.pdf.PdfReader(pdfFileName);
            if (reader != null)
            {
                var root = reader.Catalog;
                if (root != null)
                {
                    var names = root.GetAsDict(iTextSharp.text.pdf.PdfName.NAMES);
                    if (names != null)
                    {
                        var embeddedFiles = names.GetAsDict(iTextSharp.text.pdf.PdfName.EMBEDDEDFILES);
                        if (embeddedFiles != null)
                        {
                            var namesArray = embeddedFiles.GetAsArray(iTextSharp.text.pdf.PdfName.NAMES);
                            if (namesArray != null)
                            {
                                int n = namesArray.Size;
                                for (int i = 0; i < n; i++)
                                {
                                    i++;
                                    var fileArray = namesArray.GetAsDict(i);
                                    var file = fileArray.GetAsDict(iTextSharp.text.pdf.PdfName.EF);
                                    foreach (iTextSharp.text.pdf.PdfName key in file.Keys)
                                    {
                                        string attachedFileName = fileArray.GetAsString(key).ToString();
                                        //if (attachedFileName == embeddedFileName)
                                        //{
                                        var stream = (iTextSharp.text.pdf.PRStream)iTextSharp.text.pdf.PdfReader.GetPdfObject(file.GetAsIndirectObject(key));
                                        attachedFileBytes = iTextSharp.text.pdf.PdfReader.GetStreamBytes(stream);
                                        string text = System.Text.Encoding.UTF8.GetString(attachedFileBytes);
                                        XmlSerializer serializer = new XmlSerializer(typeof(FBWiFi_XMLBody));
                                        using (TextReader txtXML = new StringReader(text))
                                        {
                                            FBWiFi_XMLBody result = (FBWiFi_XMLBody)serializer.Deserialize(txtXML);
                                        }
                                        break;
                                        //}
                                    }
                                    if (attachedFileBytes != null) 
                                        break;
                                }
                            }
                        }
                    }
                }
                reader.Close();
            }
            return attachedFileBytes;
        }
    }
}
