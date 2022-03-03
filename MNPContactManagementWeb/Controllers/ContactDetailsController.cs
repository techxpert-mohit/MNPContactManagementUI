using Microsoft.AspNetCore.Mvc;
using ContactDetails = MNPContactManagementWeb.Models.ContactDetails;
using Newtonsoft.Json;
using System.Text;

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
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/ContactDetails").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                contactsList = JsonConvert.DeserializeObject<List<ContactDetails>>(data);
            }
            return View(contactsList);
        }

        public IActionResult Create()
        {
            
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
