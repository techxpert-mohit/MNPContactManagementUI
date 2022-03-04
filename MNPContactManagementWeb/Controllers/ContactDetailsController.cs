using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using MNPContactManagementWeb.Models;
using System.Dynamic;

namespace MNPContactManagementWeb.Controllers
{
    public class ContactDetailsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7040/api");
        HttpClient client;
        public ContactDetailsController()
        {
            
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            var contactsList = new List<ContactDetails>();
            contactsList = GetContactDetails();
            return View(contactsList);
        }

        public List<ContactDetails> GetContactDetails()
        {
            var contactsList = new List<ContactDetails>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/ContactDetails").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                contactsList = JsonConvert.DeserializeObject<List<ContactDetails>>(data);
            }
            return contactsList;
        }

        public IActionResult Company()
        {
            var companylist = new List<Company>();
            companylist = GetCompanies();
            return View(companylist);
        }

        public List<Company> GetCompanies()
        {
            var companyList = new List<Company>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Company").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                companyList = JsonConvert.DeserializeObject<List<Company>>(data);
            }
            return companyList;
        }

        public IActionResult Create()
        {
            ViewBag.Companies = GetCompanies();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContactDetails contactDetails)
        {
            
            if(ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(contactDetails);
                StringContent content = new StringContent(data,Encoding.UTF8,"application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/ContactDetails", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Contact Added Successfully";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            ViewBag.Companies = GetCompanies();
            var contact = new ContactDetails();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/ContactDetails/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                contact = JsonConvert.DeserializeObject<ContactDetails>(data);
            }
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ContactDetails contactDetails)
        {

            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(contactDetails);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/ContactDetails/", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Contact Updated Successfully";
                    return RedirectToAction("Index");
                }
                
            }
            return View();
        }
    }
}
