//using Microsoft.Reporting.NETCore;
//using System.IO;
//using System.Text;

//namespace Halda.Utilities.Report
//{
//    public interface IReportService
//    {
//        byte[] GenerateReportAsync(string reportName, string reportType, string datasetname, object dataset);
//    }

//    public class ReportService : IReportService
//    {

//        public byte[] GenerateReportAsync(string rdlcpath, string reportType, string datasetname, object dataset)
//        {
//            string mimeType = "application/pdf";
//            string encoding;
//            string fileNameExtension = "test";
//            Warning[] warnings;
//            string[] streams;
//            //byte[] renderedBytes;
//            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
//            Encoding.GetEncoding("utf-8");
//            LocalReport report = new LocalReport();
//            report.ReportPath = rdlcpath;
//            report.ReleaseSandboxAppDomain();
//            report.EnableExternalImages = true;
//            ReportDataSource rd = new ReportDataSource();
//            rd.Name = datasetname;
//            rd.Value = dataset;
//            report.DataSources.Add(rd);

//            ReportPageSettings aPageSettings = report.GetDefaultPageSettings();
//            int width = aPageSettings.PaperSize.Width;
//            int height = aPageSettings.PaperSize.Height;
//            int margintop = aPageSettings.Margins.Top;
//            int marginbottom = aPageSettings.Margins.Bottom;
//            int marginleft = aPageSettings.Margins.Left;
//            int marginright = aPageSettings.Margins.Right;

//            string deviceInfo =

//                "<DeviceInfo>" +
//                "  <OutputFormat>" + "PDF" + "</OutputFormat>" +
//                "  <PageWidth>" + width + "</PageWidth>" +
//                "  <PageHeight>" + height + "</PageHeight>" +
//                "  <MarginTop>" + margintop + "</MarginTop>" +
//                "  <MarginLeft>" + marginleft + "</MarginLeft>" +
//                "  <MarginRight>" + marginright + "</MarginRight>" +
//                "  <MarginBottom>" + marginbottom + "</MarginBottom>" +
//                "</DeviceInfo>";
//            Dictionary<string, string> parameters = new Dictionary<string, string>();
//            //Warning[] warnings;
//            //string[] streams;
//            //byte[] renderedBytes;
//            var result = report.Render(reportType,
//                    deviceInfo,
//                    out mimeType,
//                    out encoding,
//                    out fileNameExtension,
//                    out streams,
//                    out warnings);

//            return result;
//        }

//        //private RenderType GetRenderType(string reportType)
//        //{
//        //    var renderType = RenderType.Pdf;

//        //    switch (reportType.ToUpper())
//        //    {
//        //        default:
//        //        case "PDF":
//        //            renderType = RenderType.Pdf;
//        //            break;
//        //        case "XLS":
//        //            renderType = RenderType.Excel;
//        //            break;
//        //        case "WORD":
//        //            renderType = RenderType.Word;
//        //            break;
//        //    }

//        //    return renderType;
//        //}

//    }

//}

