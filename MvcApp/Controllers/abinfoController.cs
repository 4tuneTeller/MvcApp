using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class AbinfoController : Controller // контроллер для формирования xml по записи из таблицы
    {
        // GET: abinfo
        public ActionResult Index()
        {
            string param = Request.QueryString["abid"]; // берем параметр из uri
            int elementId;
            XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes")); // создаем xml-документ

            try
            {
                if (int.TryParse(param, out elementId)) // проверяем, что в параметре находится int-значение и парсим его
                {
                    using (AddressBookEntities context = new AddressBookEntities())
                    {
                        // запрос в базу данных:
                        var query = from c in context.AddressBook
                                    where c.ID == elementId
                                    select c;

                        if (query.Count() > 0)
                        {
                            // если запись найдена, формируем xml
                            AddressBook dbElement = query.First();
                            XElement[] xmlSubElements = { new XElement("FullName", dbElement.FullName),
                                                      new XElement("PhoneNumber", dbElement.PhoneNumber),
                                                      new XElement("EMail", dbElement.EMail),
                                                      new XElement("Department", dbElement.Department),
                                                      new XElement("Position", dbElement.Position) };
                            XElement xml = new XElement("AddressBook", new XElement("Contact", new XAttribute("ID", elementId), xmlSubElements));
                            xmlDoc.Add(xml);

                            return Content(xmlDoc.ToString(), "text/xml");
                        }
                        else
                        {
                            // иначе - выводим ошибку в виде xml
                            return errorResult(xmlDoc, "Запись с указанным id не найдена в базе данных");
                        }
                    }
                }
                else
                {
                    return errorResult(xmlDoc, "Неверно указан id");
                }
            }
            catch (Exception ex)
            {
                return errorResult(xmlDoc, "Ошибка получения данных: " + ex.Message);
            }
        }

        private ContentResult errorResult(XDocument xmlDoc, string message)
        {
            xmlDoc.Add(new XElement("Error", message));
            return Content(xmlDoc.ToString(), "text/xml");
        }
    }
}