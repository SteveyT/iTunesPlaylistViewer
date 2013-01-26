using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Schema;
using iTunesPlaylistViewer.Models;

namespace iTunesPlaylistViewer.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Home/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            var output = new StringBuilder();
            if (ModelState.IsValid)
            {
                var settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                settings.IgnoreWhitespace = true;
                settings.IgnoreComments = true;
                settings.ValidationType = ValidationType.None;
                settings.DtdProcessing = DtdProcessing.Ignore;

                using (var reader = XmlReader.Create(file.InputStream, settings))
                {
                    var ws = new XmlWriterSettings {Indent = true};

                    using (var writer = XmlWriter.Create(output, ws))
                    {
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    writer.WriteStartElement(reader.Name);
                                    break;
                                case XmlNodeType.Text:
                                    writer.WriteString(reader.Value);
                                    break;
                                case XmlNodeType.XmlDeclaration:
                                case XmlNodeType.ProcessingInstruction:
                                    writer.WriteProcessingInstruction(reader.Name, reader.Value);
                                    break;
                                case XmlNodeType.Comment:
                                    writer.WriteComment(reader.Value);
                                    break;
                                case XmlNodeType.EndElement:
                                    writer.WriteFullEndElement();
                                    break;
                            }
                        }
                    }
                }
            }

            var xmldoc = new XmlDocument();
            xmldoc.LoadXml(output.ToString());

            return View();
        }
        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                throw new XmlSchemaException("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
            
            throw new XmlSchemaValidationException("\tValidation error: " + args.Message);
        }
    }
}
